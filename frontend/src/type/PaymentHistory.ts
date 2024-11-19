export interface PaymentHistory {
    paymentHistoryId: number;
    transactionId: string;
    orderId: string;
    paymentMethodId: number;
    amount: number;
    currencyId?: number | null;
    status: number;
    responseJSON?: string;
    callbackURL?: string;
    createdAt: Date;
    paymentMethodName?: string | null; 
}