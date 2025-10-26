import { useEffect, useRef, useState } from 'react';
import * as signalR from '@microsoft/signalr';

interface UseSignalROptions {
  hubUrl: string;
  autoConnect?: boolean;
}

export const useSignalR = (options: UseSignalROptions) => {
  const { hubUrl, autoConnect = true } = options;
  const [isConnected, setIsConnected] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const connectionRef = useRef<signalR.HubConnection | null>(null);

  useEffect(() => {
    // Build connection
    const connection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          // Exponential backoff: 0s, 2s, 10s, 30s, then 30s
          if (retryContext.previousRetryCount === 0) return 0;
          if (retryContext.previousRetryCount === 1) return 2000;
          if (retryContext.previousRetryCount === 2) return 10000;
          return 30000;
        }
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    connectionRef.current = connection;

    // Connection event handlers
    connection.onreconnecting(() => {
      setIsConnected(false);
      setError('Reconnecting...');
    });

    connection.onreconnected(() => {
      setIsConnected(true);
      setError(null);
    });

    connection.onclose((error) => {
      setIsConnected(false);
      setError(error?.message || 'Connection closed');
    });

    // Auto-connect if enabled
    if (autoConnect) {
      connection
        .start()
        .then(() => {
          setIsConnected(true);
          setError(null);
          console.log('SignalR Connected');
        })
        .catch((err) => {
          setError(err.message);
          console.error('SignalR Connection Error:', err);
        });
    }

    // Cleanup
    return () => {
      if (connection.state === signalR.HubConnectionState.Connected) {
        connection.stop();
      }
    };
  }, [hubUrl, autoConnect]);

  const on = (methodName: string, callback: (...args: any[]) => void) => {
    connectionRef.current?.on(methodName, callback);
  };

  const off = (methodName: string, callback: (...args: any[]) => void) => {
    connectionRef.current?.off(methodName, callback);
  };

  const invoke = async (methodName: string, ...args: any[]) => {
    if (connectionRef.current?.state === signalR.HubConnectionState.Connected) {
      try {
        return await connectionRef.current.invoke(methodName, ...args);
      } catch (err: any) {
        console.error(`Error invoking ${methodName}:`, err);
        throw err;
      }
    } else {
      throw new Error('SignalR connection not established');
    }
  };

  const send = async (methodName: string, ...args: any[]) => {
    if (connectionRef.current?.state === signalR.HubConnectionState.Connected) {
      try {
        await connectionRef.current.send(methodName, ...args);
      } catch (err: any) {
        console.error(`Error sending ${methodName}:`, err);
        throw err;
      }
    }
  };

  return {
    connection: connectionRef.current,
    isConnected,
    error,
    on,
    off,
    invoke,
    send
  };
};