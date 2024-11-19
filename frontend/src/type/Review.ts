export interface Review {
    reviewId: number;
    itemId: number;
    customerName: string;
    customerEmail?: string | null;
    rating: number;
    comment: string;
    reviewDate: Date;
    picture?: string | null;
    likeCounts: number;
    isVerifiedPurchase: boolean;
    response?: string | null;
}