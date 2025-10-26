import apiService from '../services/api.service';
import { Clinic } from '../types/clinic';

export const clinicApi = {
  // Get all users
  getClinics: () => apiService.get<Clinic[]>('/clinics'),

  // Get single user
  getClinic: (id: string) => apiService.get<Clinic>(`/clinics/${id}`),
};