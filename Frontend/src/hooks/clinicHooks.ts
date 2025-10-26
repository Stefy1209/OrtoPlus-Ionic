import { useQuery } from '@tanstack/react-query';
import { clinicApi } from '../api/clinic.api';

// Query Keys
export const clinicKeys = {
  all: ['clinics'] as const,
  lists: () => [...clinicKeys.all, 'list'] as const,
  details: () => [...clinicKeys.all, 'detail'] as const,
  detail: (id: string) => [...clinicKeys.details(), id] as const
};

// Get all clinics
export const useClinics = () => {
  return useQuery({
    queryKey: clinicKeys.lists(),
    queryFn: clinicApi.getClinics,
    staleTime: 5 * 60 * 1000, // 5 minutes
  });
};

// Get single clinic by ID
export const useClinic = (id: string) => {
  return useQuery({
    queryKey: clinicKeys.detail(id),
    queryFn: () => clinicApi.getClinic(id),
    enabled: !!id,
  });
};