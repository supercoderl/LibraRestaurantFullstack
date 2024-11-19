export interface PaymentMethod {
    paymentMethodId: number;
    name: string;
    description?: string | null;
    picture?: string | null;
    isActive: boolean;
}