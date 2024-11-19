export interface DiscountType {
    discountTypeId: number;
    name: string;
    description?: string | null;
    isPercentage: boolean;
    value: number;
    createdAt: Date;
    startTime: Date;
    endTime?: Date | null;
    counponCode?: string | null;
    minOrderValue: number;
    minItemQuantity: number;
    maxDiscountValue: number;
}