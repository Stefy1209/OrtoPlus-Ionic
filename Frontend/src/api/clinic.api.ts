import apiService from '../services/api.service';
import { Clinic } from '../types/clinic';
import { PageMetadata } from '../types/pageMetadata';
import { Review } from '../types/review';

export const clinicApi = {
  //Gel clinics paged
  getClinics: (pageNumber: number, pageSize: number, filterName: string | undefined, minRating: number | undefined) => {
      let baseUrl = `/clinics?pageNumber=${pageNumber}&pageSize=${pageSize}`;

      if (filterName) {
        baseUrl += `&filterName=${encodeURIComponent(filterName)}`;
      }

      if (minRating) {
        baseUrl += `&minRating=${minRating}`;
      }

      return apiService.get<PageMetadata<Clinic>>(baseUrl);
  },

  // Get single clinic
  getClinic: (id: string) => apiService.get<Clinic>(`/clinics/${id}`),

  // Add a review to a clinic
  addReview: (id: string, review: Review) => apiService.post<Clinic>(`/clinics/${id}/reviews`, review),
};