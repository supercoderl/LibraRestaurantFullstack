export interface OrderLine {
    orderLineId: number;
    orderId: string;
    itemId: number;
    foodName?: string | null;
    quantity: number;
    isCanceled: boolean;
    canceledTime?: Date | null;
    canceledReason?: string | null;
    customerReview?: string | null;
    customerLike: number;
    foodPrice?: number | null;
}