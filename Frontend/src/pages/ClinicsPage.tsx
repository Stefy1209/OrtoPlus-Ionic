import { IonBadge, IonButton, IonContent, IonHeader, IonIcon, IonItem, IonLabel, IonList, IonPage, IonRefresher, IonRefresherContent, IonSkeletonText, IonText, IonTitle, IonToolbar, RefresherEventDetail, useIonViewWillEnter } from '@ionic/react';
import { location, star } from 'ionicons/icons';
import { useClinics } from '../hooks/clinicHooks';


const ClinicsPage: React.FC = () => {
  const { data: clinics, isLoading, error, refetch } = useClinics();

  useIonViewWillEnter(() => {
    refetch();
  });

  const handleRefresh = async (event: CustomEvent<RefresherEventDetail>) => {
    await refetch();
    event.detail.complete();
  };

  const renderContent = () => {
    if (isLoading) {
      return (
        <IonList inset={true}>
          {[1, 2, 3, 4, 5].map((i) => (
            <IonItem key={i}>
              <IonLabel>
                <h1>
                  <IonSkeletonText animated style={{ width: '60%' }} />
                </h1>
                <p>
                  <IonSkeletonText animated style={{ width: '80%' }} />
                </p>
                <p>
                  <IonSkeletonText animated style={{ width: '40%' }} />
                </p>
              </IonLabel>
            </IonItem>
          ))}
        </IonList>
      );
    }

    if (error) {
      return (
        <div style={{ padding: '20px', textAlign: 'center' }}>
          <IonText color="danger">
            <h2>Error Loading Clinics</h2>
            <p>{(error as any).message}</p>
          </IonText>
          <IonButton onClick={() => refetch()}>Try Again</IonButton>
        </div>
      );
    }

    if (!clinics || clinics.length === 0) {
      return (
        <div style={{ padding: '20px', textAlign: 'center' }}>
          <IonText color="medium">
            <h2>No Clinics Found</h2>
            <p>No clinics available at the moment</p>
          </IonText>
        </div>
      );
    }

    return (
      <IonList inset={true}>
        {clinics.map((clinic) => (
          <IonItem key={clinic.clinicId} button routerLink={`/clinics/${clinic.clinicId}`}>
            <IonLabel>
              <h1>{clinic.name}</h1>
              <p>
                <IonIcon icon={location} style={{ fontSize: '14px', marginRight: '4px' }} />
                {clinic.address.street}, {clinic.address.city}
              </p>
              <div style={{ display: 'flex', alignItems: 'center', gap: '8px', marginTop: '4px' }}>
                <IonBadge color="primary">
                  <IonIcon icon={star} style={{ fontSize: '12px', marginRight: '2px' }} />
                  {clinic.rating.toFixed(1)}
                </IonBadge>
                <IonText color="medium" style={{ fontSize: '12px' }}>
                  {clinic.reviews.length} {clinic.reviews.length === 1 ? 'review' : 'reviews'}
                </IonText>
              </div>
            </IonLabel>
          </IonItem>
        ))}
      </IonList>
    );
  };

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>OrtoPlus</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent fullscreen>
        <IonRefresher slot="fixed" onIonRefresh={handleRefresh}>
          <IonRefresherContent />
        </IonRefresher>

        <IonHeader collapse="condense">
          <IonToolbar>
            <IonTitle size="large">Clinics</IonTitle>
          </IonToolbar>
        </IonHeader>

        {renderContent()}
      </IonContent>
    </IonPage>
  );
};

export default ClinicsPage;
