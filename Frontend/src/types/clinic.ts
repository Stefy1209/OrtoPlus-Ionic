import { Address } from "./address";
import { Review } from "./review";

export interface Clinic {
    clinicId: string;
    name: string;
    rating: number;
    address: Address;
    reviews: Review[];
}