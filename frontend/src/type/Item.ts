export default interface Item {
    itemId: number;
    title: string;
    slug: string;
    summary?: string | null;
    sku: string;
    picture: string | null;
    price: number;
    quantity: number;
    recipe?: string | null;
    instruction?: string | null;
    createdAt: Date;
    lastUpdatedAt?: Date | null;
    categoryIds: number[];
    discount: {
        discountId: number;
        isPercentage: boolean;
        discountValue: number;
        discountTypeId: number;
    };
    discountStatus: number;
    ratingScore: number;
}