import React, { createContext, useContext, useEffect, useState } from 'react';
import { useSignalR } from '../hooks/notificationHook';
import { useIonAlert } from '@ionic/react';

interface Notification {
  id: string;
  message: string;
  timestamp: Date;
  type?: 'info' | 'success' | 'warning' | 'error';
}

interface NotificationContextType {
  notifications: Notification[];
  isConnected: boolean;
  clearNotification: (id: string) => void;
  clearAll: () => void;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const NotificationProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [notifications, setNotifications] = useState<Notification[]>([]);
  const [presentAlert] = useIonAlert();
  
  const { isConnected, on, off } = useSignalR({
    hubUrl: `${import.meta.env.VITE_API_URL}/notificationHub`,
    autoConnect: true
  });

  useEffect(() => {
    // Listen for notifications from the server
    const handleNotification = (message: string, type?: string) => {
      const notification: Notification = {
        id: Date.now().toString(),
        message,
        timestamp: new Date(),
        type: (type as any) || 'info'
      };

      setNotifications(prev => [...prev, notification]);

      // Show Ionic alert
      presentAlert({
        header: 'Notification',
        subHeader: type ? type.charAt(0).toUpperCase() + type.slice(1) : 'Info',
        message,
        buttons: ['OK']
      });
    };

    on('ReceiveNotification', handleNotification);

    // Cleanup
    return () => {
      off('ReceiveNotification', handleNotification);
    };
  }, [on, off, presentAlert]);

  const clearNotification = (id: string) => {
    setNotifications(prev => prev.filter(n => n.id !== id));
  };

  const clearAll = () => {
    setNotifications([]);
  };

  return (
    <NotificationContext.Provider 
      value={{ 
        notifications, 
        isConnected, 
        clearNotification, 
        clearAll 
      }}
    >
      {children}
    </NotificationContext.Provider>
  );
};

export const useNotifications = () => {
  const context = useContext(NotificationContext);
  if (context === undefined) {
    throw new Error('useNotifications must be used within a NotificationProvider');
  }
  return context;
};