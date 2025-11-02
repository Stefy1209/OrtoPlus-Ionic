import { clinicApi } from "../api/clinic.api";

const PENDING_KEY = 'pendingReviews';
export interface PendingReview {
  id: string;
  clinicId: string;
  rating: number;
  comment: string;
  timestamp: number;
}
export const offlineStorage = {
  // Add pending review
  add(clinicId: string, rating: number, comment: string): PendingReview {
    const pending = this.getAll();
    const review: PendingReview = {
      id: `${clinicId}_${Date.now()}`,
      clinicId,
      rating,
      comment,
      timestamp: Date.now()
    };
    pending.push(review);
    localStorage.setItem(PENDING_KEY, JSON.stringify(pending));
    console.log('OfflineStorage: Added pending review', review);
    console.log('OfflineStorage: Total pending:', pending.length);
    return review;
  },

  // Get all pending reviews
  getAll(): PendingReview[] {
    const data = localStorage.getItem(PENDING_KEY);
    return data ? JSON.parse(data) : [];
  },

  // Remove review after sync
  remove(id: string) {
    const pending = this.getAll().filter(r => r.id !== id);
    localStorage.setItem(PENDING_KEY, JSON.stringify(pending));
  },

  // Get count
  count(): number {
    return this.getAll().length;
  },

  // Clear all
  clear() {
    localStorage.removeItem(PENDING_KEY);
  },

  async syncAll() {
    const pending = this.getAll();
    if (pending.length === 0) return;
    console.log(`Syncing ${pending.length} pending reviews...`);
    for (const review of pending) {
      try {
        await clinicApi.addReview(review.clinicId, {
          reviewId: '00000000-0000-0000-0000-000000000000',
          rating: review.rating,
          comment: review.comment,
          date: new Date().toISOString(),
          userAccountId: '00000000-0000-0000-0000-000000000000'
        });
        this.remove(review.id);
        console.log(`Synced review ${review.id}`);
      } catch (err) {
        console.error(`Failed to sync review ${review.id}:`, err);
      }
    }
  }
};