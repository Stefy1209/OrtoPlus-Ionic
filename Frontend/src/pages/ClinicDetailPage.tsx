// src/pages/ClinicDetailPage.tsx
import React, { useEffect, useRef, useState } from 'react';
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
  IonModal,
  IonGrid,
  IonRow,
  IonCol,
  IonInput,
  IonSelect,
  IonSelectOption,
  IonToast,
} from '@ionic/react';
import { star, location, add } from 'ionicons/icons';
import { useIonViewWillEnter } from '@ionic/react';
import { useAddReview, useClinic } from '../hooks/clinicHooks';
import { Review } from '../types/review';
import { Network } from '@capacitor/network';

const ClinicDetailPage: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const history = useHistory();
  const { data: clinic, isLoading, error, refetch } = useClinic(id);
  const [comment, setComment] = useState('');
  const [rating, setRating] = useState(5);
  const [isConnected, setIsConnected] = useState(false);
  const [isOpen, setIsOpen] = useState(false);
  const [showErrorToast, setShowErrorToast] = useState(false);
  const { addReview, error: addReviewError } = useAddReview(id, refetch);

  useIonViewWillEnter(() => {
    refetch();
  });

  useEffect(() => {
    const checkStatus = async () => {
      const status = await Network.getStatus();
      setIsConnected(status.connected);
    };

    checkStatus();

    let removeListener: (() => void);
    
    Network.addListener('networkStatusChange', status => {
      setIsConnected(status.connected);
      if (status.connected) {
        refetch();
      }
    }).then(handler => {
      removeListener = () => handler.remove();
    });

    if (addReviewError) {
      setShowErrorToast(true);
    }

    return () => {
      if (removeListener) {
        removeListener();
      }
    };
  }, [addReviewError]);

  const handleAddReview = async () => {
    if (!comment.trim()) {
      return; // Don't submit empty reviews
    }

    const review: Review = {
      reviewId: '00000000-0000-0000-0000-000000000000', // Empty Guid
      comment: comment.trim(),
      rating: rating,
      date: new Date().toISOString(),
      userAccountId: '00000000-0000-0000-0000-000000000000' // Backend will override this
    };

    try {
      await addReview(review);
    } catch(err) {

    } finally {
      setComment('');
      setRating(5);
      setIsOpen(false);
    }

  };

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
          <IonButtons slot='start'>
            <IonBackButton />
          </IonButtons>
          <IonTitle>{clinic.name}</IonTitle>
          <IonButtons slot='end'>
            <div
              style={{
                width: '12px',
                height: '12px',
                borderRadius: '50%',
                backgroundColor: isConnected ? '#10b981' : '#ef4444',
                marginRight: '16px',
                boxShadow: `0 0 8px ${isConnected ? '#10b98180' : '#ef444480'}`
              }}
            />
          </IonButtons>
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

          <IonToolbar>
            <IonButtons slot='end'>
              <IonButton id='open-add-review-modal' onClick={() => setIsOpen(true)}>
                <IonIcon slot='icon-only' icon={add}></IonIcon>
              </IonButton>
              <IonModal isOpen={isOpen} trigger='open-add-review-modal'>
                <IonHeader>
                  <IonToolbar>
                    <IonTitle>Add Review</IonTitle>
                    <IonButtons slot='end'>
                      <IonButton strong={true} onClick={handleAddReview}>Confirm</IonButton>
                    </IonButtons>
                  </IonToolbar>
                </IonHeader>
                <IonContent className='ion-padding'>
                  <IonGrid>
                    <IonRow>
                      <IonCol>
                        <IonInput label='Enter Comment' labelPlacement='stacked' type='text' value={comment} onIonInput={(e) => setComment(e.detail.value!)}/>
                      </IonCol>
                    </IonRow>
                    <IonRow>
                      <IonSelect label='Rating' value={rating} onIonChange={(e) => setRating(e.detail.value)}>
                        <IonSelectOption value={1}>1</IonSelectOption>
                        <IonSelectOption value={2}>2</IonSelectOption>
                        <IonSelectOption value={3}>3</IonSelectOption>
                        <IonSelectOption value={4}>4</IonSelectOption>
                        <IonSelectOption value={5}>5</IonSelectOption>
                      </IonSelect>
                    </IonRow>
                  </IonGrid>
                </IonContent>
              </IonModal>
            </IonButtons>
          </IonToolbar>
        </IonCard>
        <IonToast 
          isOpen={showErrorToast} 
          onDidDismiss={() => setShowErrorToast(false)} 
          message={addReviewError?.message || 'Failed to add review'}
          duration={3000}
          color='danger'
          position='bottom'
        />
      </IonContent>
    </IonPage>
  );
};

export default ClinicDetailPage;