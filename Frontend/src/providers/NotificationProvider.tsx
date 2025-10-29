import { useIonAlert } from "@ionic/react";
import { createContext, useContext, useEffect } from "react";
import { useSignalR } from "../hooks/notificationHook";

interface NotificationContextType {
  isConnected: boolean;
}

const NotificationContext = createContext<NotificationContextType | undefined>(undefined);

export const NotificationProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
  const [presentAlert] = useIonAlert();
  
  const { isConnected, on, off } = useSignalR({
    hubUrl: `${import.meta.env.VITE_API_URL}/notificationHub`,
    autoConnect: true
  });

  useEffect(() => {
    const handleNotification = (message: string, type?: string) => {
      presentAlert({
        header: 'Notification',
        subHeader: 'Info',
        message,
        buttons: ['OK']
      });
    };

    on('ReceiveNotification', handleNotification);
    
    return () => {
      off('ReceiveNotification', handleNotification);
    };
  }, [on, off, presentAlert]);

  return (
    <NotificationContext.Provider value={{ isConnected }}>
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