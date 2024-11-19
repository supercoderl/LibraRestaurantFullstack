export interface Discount {
    discountId: number;
    discountTypeId: number;
    categoryId?: number | null;
    itemId?: number | null;
    discountTargetType: number;
    orderId?: string | null;
    invoiceId?: string | null;
    comments?: string | null;
    discountTypeName?: string | null;
    categoryName?: string | null;
    foodName?: string | null;
}