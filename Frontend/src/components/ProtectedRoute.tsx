import { Redirect, Route, RouteProps } from "react-router";
import authService from "../services/auth.service";

interface ProtectedRouteProps extends RouteProps { 
    component: React.ComponentType<any>;
}

const ProtectedRoute: React.FC<ProtectedRouteProps>=  ({ component: Component, ...rest }) => { 
    return (
        <Route
            {...rest}
            render={(props) => authService.isAuthenticated() ? (<Component {...props} />) : (<Redirect to="/authentication" />)}
        />
    );
}

export default ProtectedRoute;