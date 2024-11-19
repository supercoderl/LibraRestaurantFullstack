import Item from "@/type/Item";
import { OrderLine } from "@/type/OrderLine";

export const hasItem = (obj: any): obj is { item: Item, quantityOrder: number } => {
    return obj && obj.item !== undefined;
}

export const mergeItemsAndOrderLines = (
    itemsInCart: { item: Item, quantityOrder: number }[],
    orderLines: OrderLine[] | undefined,
    items: Item[]
): ({ item: Item, quantityOrder: number, hasMore: boolean })[] => {
    const mergedItems: ({ item: Item, quantityOrder: number, hasMore: boolean })[] = [];

    // Thêm các order line vào mergedItems
    if (orderLines) {
        orderLines.forEach(orderLine => {
            const item = items.find(x => x.itemId === orderLine.itemId);
            if (item) {
                mergedItems.push({ item, quantityOrder: orderLine.quantity, hasMore: false });
            }
        });
    }

    // Thêm các item trong giỏ hàng vào mergedItems
    if (itemsInCart.length > 0) {
        itemsInCart.forEach(item => {
            const existingItem = mergedItems.find(x => x.item.itemId === item.item.itemId);

            if (!existingItem) {
                mergedItems.push({ ...item, hasMore: true }); // Thêm nếu chưa tồn tại
            } else {
                // Nếu có, kiểm tra quantity
                if (item.quantityOrder > existingItem.quantityOrder) {
                    existingItem.quantityOrder = item.quantityOrder;
                    existingItem.hasMore = true;
                }
            }
        });
    }

    return mergedItems;
};