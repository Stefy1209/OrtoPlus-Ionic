import { IonButton, IonCard, IonCardContent, IonCardHeader, IonCardTitle, IonContent, IonHeader, IonInput, IonInputPasswordToggle, IonLoading, IonPage, IonText, IonTitle, IonToolbar } from "@ionic/react";
import { useState } from "react";
import { useHistory } from "react-router";
import authService from "../services/auth.service";

const AuthenticationPage: React.FC = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState("");
    const history = useHistory();

    const handleLogin = async () => {
        if (!email || !password) {
            setError("Please enter both email and password.");
            return;
        }

        setIsLoading(true);
        setError("");

        try {
            await authService.login({email, password});
            history.replace("/clinics");
        } catch (err: any) {
            const errorMessage = err.Message || "Login failed. Please check your credentials and try again.";
            setError(errorMessage);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <IonPage>
            <IonHeader>
                <IonToolbar>
                    <IonTitle>OrtoPlus</IonTitle>
                </IonToolbar>
            </IonHeader>
            <IonContent className="ion-padding">
                <IonLoading isOpen={isLoading} message="Please wait..." />
                <IonCard className="ion-padding">
                    <IonCardHeader>
                        <IonCardTitle>Authentication</IonCardTitle>
                    </IonCardHeader>
                    {error && (<IonText color="danger"><p>{error}</p></IonText>)}
                    <IonCardContent>
                        <IonInput 
                            label="Email" 
                            type="email" 
                            placeholder="email@domain.com" 
                            value={email} 
                            onIonInput={(e) => setEmail(e.detail.value!)}
                        >
                        </IonInput>
                        <IonInput 
                            label="Password" 
                            type="password" 
                            placeholder="1234567890 (please don't use this password...) "
                            value={password}
                            onIonInput={(p) => setPassword(p.detail.value!)}
                        >
                            <IonInputPasswordToggle slot="end"></IonInputPasswordToggle>
                        </IonInput>
                    </IonCardContent>
                    <IonButton onClick={handleLogin} disabled={isLoading}>Log In</IonButton>
                </IonCard>
            </IonContent>
        </IonPage>
    );
};

export default AuthenticationPage;