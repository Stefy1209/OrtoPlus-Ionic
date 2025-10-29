import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { clinicApi } from '../api/clinic.api';
import { Review } from '../types/review';

// Query Keys
export const clinicKeys = {
  all: ['clinics'] as const,
  lists: () => [...clinicKeys.all, 'list'] as const,
  list: (pageNumber: number, pageSize: number, filterName?: string, minRating?: number) => [...clinicKeys.lists(), { pageNumber, pageSize, filterName, minRating }] as const,
  details: () => [...clinicKeys.all, 'detail'] as const,
  detail: (id: string) => [...clinicKeys.details(), id] as const
};

// Get all clinics
export const useClinics = (
  pageNumber: number, 
  pageSize: number, 
  filterName?: string,
  minRating?: number
) => {
  return useQuery({
    queryKey: clinicKeys.list(pageNumber, pageSize, filterName, minRating),
    queryFn: () => clinicApi.getClinics(pageNumber, pageSize, filterName, minRating),
    staleTime: 5 * 60 * 1000 // 5 minutes
  });
};

// Get single clinic by ID
export const useClinic = (id: string) => {
  return useQuery({
    queryKey: clinicKeys.detail(id),
    queryFn: () => clinicApi.getClinic(id),
    enabled: !!id
  });
};