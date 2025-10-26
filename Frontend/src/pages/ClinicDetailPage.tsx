// src/pages/ClinicDetailPage.tsx
import React from 'react';
import { useParams, useHistory } from 'react-router-dom';
import {
  IonContent,
  IonHeader,
  IonPage,
  IonTitle,
  IonToolbar,
  IonButtons,
  IonBackButton,
  IonCard,
  IonCardHeader,
  IonCardTitle,
  IonCardContent,
  IonItem,
  IonLabel,
  IonButton,
  IonSkeletonText,
  IonText,
  IonIcon,
  IonBadge,
  IonList,
} from '@ionic/react';
import { star, location } from 'ionicons/icons';
import { useIonViewWillEnter } from '@ionic/react';
import { useClinic } from '../hooks/clinicHooks';

const ClinicDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const history = useHistory();
  const { data: clinic, isLoading, error, refetch } = useClinic(id);

  useIonViewWillEnter(() => {
    refetch();
  });

  if (isLoading) {
    return (
      <IonPage>
        <IonHeader>
          <IonToolbar>
            <IonButtons slot="start">
              <IonBackButton />
            </IonButtons>
            <IonTitle>
              <IonSkeletonText animated style={{ width: '50%' }} />
            </IonTitle>
          </IonToolbar>
        </IonHeader>
        <IonContent>
          <IonCard>
            <IonCardContent>
              <IonSkeletonText animated style={{ width: '80%', height: '20px' }} />
              <IonSkeletonText animated style={{ width: '60%', height: '20px' }} />
              <IonSkeletonText animated style={{ width: '70%', height: '20px' }} />
            </IonCardContent>
          </IonCard>
        </IonContent>
      </IonPage>
    );
  }

  if (error) {
    return (
      <IonPage>
        <IonHeader>
          <IonToolbar>
            <IonButtons slot="start">
              <IonBackButton />
            </IonButtons>
            <IonTitle>Error</IonTitle>
          </IonToolbar>
        </IonHeader>
        <IonContent>
          <div style={{ padding: '20px', textAlign: 'center' }}>
            <IonText color="danger">
              <h2>Error Loading Clinic</h2>
              <p>{(error as any).message}</p>
            </IonText>
            <IonButton onClick={() => refetch()}>Try Again</IonButton>
            <IonButton fill="clear" onClick={() => history.goBack()}>
              Go Back
            </IonButton>
          </div>
        </IonContent>
      </IonPage>
    );
  }

  if (!clinic) {
    return null;
  }

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonButtons slot="start">
            <IonBackButton />
          </IonButtons>
          <IonTitle>{clinic.name}</IonTitle>
        </IonToolbar>
      </IonHeader>
      <IonContent>
        <IonCard>
          <IonCardHeader>
            <IonCardTitle>Clinic Information</IonCardTitle>
          </IonCardHeader>
          <IonCardContent>
            <IonItem lines="none">
              <IonIcon icon={star} slot="start" color="warning" />
              <IonLabel>
                <h3>Rating</h3>
                <p>
                  <IonBadge color="primary">{clinic.rating.toFixed(1)}</IonBadge>
                  <span style={{ marginLeft: '8px', color: '#666' }}>
                    ({clinic.reviews.length} {clinic.reviews.length === 1 ? 'review' : 'reviews'})
                  </span>
                </p>
              </IonLabel>
            </IonItem>

            <IonItem lines="none">
              <IonIcon icon={location} slot="start" color="danger" />
              <IonLabel>
                <h3>Address</h3>
                <p>{clinic.address.street}</p>
                <p>
                  {clinic.address.city}, {clinic.address.state} {clinic.address.zipCode}
                </p>
                <p>{clinic.address.country}</p>
              </IonLabel>
            </IonItem>
          </IonCardContent>
        </IonCard>

        <IonCard>
          <IonCardHeader>
            <IonCardTitle>Reviews ({clinic.reviews.length})</IonCardTitle>
          </IonCardHeader>
          <IonCardContent>
            {clinic.reviews.length === 0 ? (
              <IonText color="medium">
                <p>No reviews yet</p>
              </IonText>
            ) : (
              <IonList>
                {clinic.reviews.map((review) => (
                  <IonItem key={review.reviewId} lines="full">
                    <IonLabel>
                      <div style={{ display: 'flex', justifyContent: 'space-between', marginBottom: '4px' }}>
                        <IonBadge color="primary">
                          <IonIcon icon={star} style={{ fontSize: '10px', marginRight: '2px' }} />
                          {review.rating.toFixed(1)}
                        </IonBadge>
                      </div>
                      <p>{review.comment}</p>
                      <IonText color="medium" style={{ fontSize: '12px' }}>
                        {new Date(review.date).toLocaleDateString()}
                      </IonText>
                    </IonLabel>
                  </IonItem>
                ))}
              </IonList>
            )}
          </IonCardContent>
        </IonCard>
      </IonContent>
    </IonPage>
  );
};

export default ClinicDetailPage;