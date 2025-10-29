import { Redirect, Route } from 'react-router-dom';
import { IonApp, IonRouterOutlet, setupIonicReact } from '@ionic/react';
import { IonReactRouter } from '@ionic/react-router';

/* Core CSS required for Ionic components to work properly */
import '@ionic/react/css/core.css';

/* Basic CSS for apps built with Ionic */
import '@ionic/react/css/normalize.css';
import '@ionic/react/css/structure.css';
import '@ionic/react/css/typography.css';

/* Optional CSS utils that can be commented out */
import '@ionic/react/css/padding.css';
import '@ionic/react/css/float-elements.css';
import '@ionic/react/css/text-alignment.css';
import '@ionic/react/css/text-transformation.css';
import '@ionic/react/css/flex-utils.css';
import '@ionic/react/css/display.css';

/**
 * Ionic Dark Mode
 * -----------------------------------------------------
 * For more info, please see:
 * https://ionicframework.com/docs/theming/dark-mode
 */

/* import '@ionic/react/css/palettes/dark.always.css'; */
/* import '@ionic/react/css/palettes/dark.class.css'; */
import '@ionic/react/css/palettes/dark.system.css';

/* Theme variables */
import './theme/variables.css';
import ClinicsPage from './pages/ClinicsPage';
import ClinicDetailPage from './pages/ClinicDetailPage';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { NotificationProvider } from './providers/NotificationProvider';
import AuthenticationPage from './pages/AuthenticationPage';
import authService from './services/auth.service';
import ProtectedRoute from './components/ProtectedRoute';

setupIonicReact();

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
});

const App: React.FC = () => {
  const isAuthenticated = authService.isAuthenticated();

  return (
    <QueryClientProvider client={queryClient}>
      <IonApp>
          <IonReactRouter>
            <IonRouterOutlet>
              <Route exact path="/authentication">
                {isAuthenticated ? <Redirect to="/clinics" /> : <AuthenticationPage />}
              </Route>
              <ProtectedRoute exact path="/clinics" component={ClinicsPage} />
              <NotificationProvider>
                <ProtectedRoute path="/clinics/:id" component={ClinicDetailPage} />
              </NotificationProvider>
              <Route exact path="/">
                <Redirect to={isAuthenticated ? "/clinics" : "/authentication"}/>
              </Route>
            </IonRouterOutlet>
          </IonReactRouter>
      </IonApp>
    </QueryClientProvider>
  );
}

export default App;
