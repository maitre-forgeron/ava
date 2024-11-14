import { Review } from "./review.model";

export interface Therapist {
    id: string;
    firstName: string;
    lastName: string;
    rating: number;
    summary: string;
    certificateId: string;
    reviews: Review[];
  }
