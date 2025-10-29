import { useEffect, useRef, useState } from 'react';
import { HttpTransportType, HubConnection, HubConnectionBuilder, HubConnectionState, LogLevel }  from '@microsoft/signalr';
import { TOKEN_KEY } from '../services/auth.service';

interface UseSignalROptions {
  hubUrl: string;
  autoConnect?: boolean;
}

export const useSignalR = (options: UseSignalROptions) => {
  const { hubUrl, autoConnect = true } = options;
  const [isConnected, setIsConnected] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const connectionRef = useRef<HubConnection | null>(null);

  useEffect(() => {
    // Build connection
    const connection = new HubConnectionBuilder()
      .withUrl(hubUrl,{
        accessTokenFactory: () => localStorage.getItem(TOKEN_KEY) ?? '',
        transport: HttpTransportType.WebSockets,
        skipNegotiation: true,
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          // Exponential backoff: 0s, 2s, 10s, 30s, then 30s
          if (retryContext.previousRetryCount === 0) return 0;
          if (retryContext.previousRetryCount === 1) return 2000;
          if (retryContext.previousRetryCount === 2) return 10000;
          return 30000;
        }
      })
      .configureLogging(LogLevel.Information)
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
      if (connection.state === HubConnectionState.Connected) {
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

  return {
    connection: connectionRef.current,
    isConnected,
    error,
    on,
    off
  };
};