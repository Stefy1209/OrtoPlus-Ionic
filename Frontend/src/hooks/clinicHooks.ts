import { useState, useEffect } from 'react';
import { clinicApi } from '../api/clinic.api';
import { Clinic } from '../types/clinic';
import { Review } from '../types/review';
import { PageMetadata } from '../types/pageMetadata';
import { ApiError } from '../services/api.service';
import { offlineStorage } from '../services/offlineStorage.service';
import { ConnectionStatus, Network } from '@capacitor/network';
import { PluginListenerHandle } from '@capacitor/core';

// Get all clinics
export const useClinics = (
  pageNumber: number, 
  pageSize: number, 
  filterName?: string,
  minRating?: number
) => {
  const [data, setData] = useState<PageMetadata<Clinic> | undefined>(undefined);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [refetchTrigger, setRefetchTrigger] = useState(0);

  useEffect(() => {
    let isMounted = true;
    
    const fetchClinics = async () => {
      try {
        setIsLoading(true);
        const result = await clinicApi.getClinics(pageNumber, pageSize, filterName, minRating);
        if (isMounted) {
          setData(result);
          setError(null);
        }
      } catch (err:any) {
        if (isMounted) {
          setError(err);
        }
      } finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    };

    fetchClinics();

    return () => {
      isMounted = false;
    };
  }, [pageNumber, pageSize, filterName, minRating, refetchTrigger]);

  const refetch = () => setRefetchTrigger(prev => prev + 1);

  return { data, isLoading, error, refetch };
};

// Get single clinic by ID
export const useClinic = (id: string) => {
  const [data, setData] = useState<Clinic | undefined>(undefined);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState(null);
  const [refetchTrigger, setRefetchTrigger] = useState(0);

  useEffect(() => {
    if (!id) {
      setIsLoading(false);
      return;
    }

    let isMounted = true;
    
    const fetchClinic = async () => {
      try {
        setIsLoading(true);
        const result = await clinicApi.getClinic(id);
        if (isMounted) {
          setData(result);
          setError(null);
        }
      } catch (err:any) {
        if (isMounted) {
          setError(err);
        }
      } finally {
        if (isMounted) {
          setIsLoading(false);
        }
      }
    };

    fetchClinic();

    return () => {
      isMounted = false;
    };
  }, [id, refetchTrigger]);

  const refetch = () => setRefetchTrigger(prev => prev + 1);

  return { data, isLoading, error, refetch };
};

export const useAddReview = (id: string, onSuccess?: () => void) => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<ApiError | undefined>(undefined);

  useEffect(() => {
    let listener: PluginListenerHandle | undefined;
    
    const setup = async () => {
      // Sync on mount if online
      const status = await Network.getStatus();
      if (status.connected) {
        await offlineStorage.syncAll();
      }
    
      // Listen for network changes
      listener = await Network.addListener('networkStatusChange', async (status: ConnectionStatus) => {
        if (status.connected) {
          await offlineStorage.syncAll();
        }
      });
    };
  
    setup();
  
    return () => {
      listener?.remove();
    };
  }, []);

  const addReview = async (review: Review) => {
    try {
      setIsLoading(true);
      setError(undefined);
      await clinicApi.addReview(id, review);
      if (onSuccess) {
        onSuccess();
      }
    } catch (err:any) {
      offlineStorage.add(id, review.rating, review.comment);
      setError(err);
      throw err;
    } finally {
      setIsLoading(false);
    }
  };

  return { addReview, isLoading, error };
};