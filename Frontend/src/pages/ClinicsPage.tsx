import { IonBadge, IonButton, IonButtons, IonCol, IonContent, IonGrid, IonHeader, IonIcon, IonInfiniteScroll, IonInfiniteScrollContent, IonItem, IonLabel, IonList, IonPage, IonRefresher, IonRefresherContent, IonRow, IonSearchbar, IonSelect, IonSelectOption, IonSkeletonText, IonText, IonTitle, IonToolbar, RefresherEventDetail, useIonRouter, useIonViewWillEnter } from '@ionic/react';
import { location, star, logOut, filter } from 'ionicons/icons';
import { useClinics } from '../hooks/clinicHooks';
import { useEffect, useState } from 'react';
import { Clinic } from '../types/clinic';
import authService from '../services/auth.service';
import { useQueryClient } from '@tanstack/react-query';

const ClinicsPage: React.FC = () => {
  // Clinics
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize] = useState(9);
  const [totalPages, setTotalPages] = useState<number | undefined>(undefined);
  const [filterName, setFilterName] = useState<string | undefined>(undefined);
  const [minRating, setMinRating] = useState<number | undefined>(undefined);
  const { data: pageMetadata, isLoading, error, refetch } = useClinics(pageNumber, pageSize, filterName, minRating);
  const [clinics, setClinics] = useState<Clinic[]>([]);

  useEffect(() => {
    if (pageMetadata) {
      setTotalPages(pageMetadata.totalPages);
      
      if (pageNumber === 1) {
        setClinics(pageMetadata.items);
      } else {
        setClinics(prev => [...prev, ...pageMetadata.items]);
      }
    }
  }, [pageMetadata, pageNumber, filterName, minRating]);

  useIonViewWillEnter(() => {
    refetch();
  });

  const handleRefresh = async (event: CustomEvent<RefresherEventDetail>) => {
    setPageNumber(1);
    setClinics([]);
    await refetch();
    event.detail.complete();
  };

  const loadMore = () => {
    if (totalPages && pageNumber < totalPages) {
      setPageNumber(prev => prev+1);
    }
  }

  const handleInfiniteScroll = async (event: CustomEvent) => {
    if (totalPages && pageNumber < totalPages && !isLoading) {
      loadMore();
      await new Promise(resolve => setTimeout(resolve, 100));
    }
    (event.target as any).complete();
  };

  const handleFilterName = (filterName: string) => {
    setPageNumber(1);
    setClinics([]);
    setFilterName(filterName);
  };

  const handleClearFilters = () => {
    setPageNumber(1);
    setClinics([]);
    setFilterName('');
    setMinRating(0);
  };

  const handleMinRating = (minRating: number) => {
    setPageNumber(1);
    setClinics([]);
    setMinRating(minRating);
  };

  const renderContent = () => {
    if (isLoading && clinics.length === 0) {
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

    if (clinics.length === 0) {
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
      <>
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
        <IonInfiniteScroll 
          onIonInfinite={handleInfiniteScroll}
          disabled={!totalPages || pageNumber >= totalPages}
        >
          <IonInfiniteScrollContent></IonInfiniteScrollContent>
        </IonInfiniteScroll>
      </>
    );
  };

  // Log Out
  const queryClient = useQueryClient();

  const handleLogout = () => {
    try {
      authService.logout();
      queryClient.clear();
      window.location.href='/'
    } catch (error) {
      console.error('Logout failed:', error);
    }
  };

  return (
    <IonPage>
      <IonHeader>
        <IonToolbar>
          <IonTitle>OrtoPlus</IonTitle>
          <IonButtons slot='end'>
            <IonButton color='danger' onClick={handleLogout}>
              <IonIcon slot='start' icon={logOut}></IonIcon>
              Log Out
            </IonButton>
          </IonButtons>
        </IonToolbar>
        <IonToolbar>
          <IonGrid>
            <IonRow>
              <IonCol>
                <IonSearchbar debounce={500} onIonInput={(e) => handleFilterName(e.detail.value!)}>
                </IonSearchbar>
              </IonCol>
              <IonCol size='1'>
                <IonSelect label='Rating' onIonChange={(e) => handleMinRating(e.detail.value!)}>
                  <IonSelectOption value={0}>0</IonSelectOption>
                  <IonSelectOption value={1}>1</IonSelectOption>
                  <IonSelectOption value={2}>2</IonSelectOption>
                  <IonSelectOption value={3}>3</IonSelectOption>
                  <IonSelectOption value={4}>4</IonSelectOption>
                  <IonSelectOption value={5}>5</IonSelectOption>
                </IonSelect>
              </IonCol>
            </IonRow>
          </IonGrid>
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
