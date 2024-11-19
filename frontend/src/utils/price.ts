import { DiscountStatus } from "@/enums";
import { DiscountType } from "@/type/DiscountType";
import Item from "@/type/Item";

export const calculateTotal = (
    itemsInCart: {
        item: Item;
        quantityOrder: number;
    }[],
    discount: DiscountType | null
) => {
    if (itemsInCart && itemsInCart.length > 0) {
        let totalPrice = itemsInCart.reduce((total, e) => {
            return total + (calculateDiscountPrice((e.item.price * e.quantityOrder), e.item?.discount?.discountValue, e.item?.discount?.isPercentage, e.item.discountStatus) ?? (e.item.price * e.quantityOrder));
        }, 0);

        if (discount) {
            if (discount.isPercentage) {
                totalPrice = totalPrice - totalPrice * discount.value / 100;
            }
            else {
                totalPrice = totalPrice - discount.value;
            }
        }

        return totalPrice;
    }
    return 0;
}

export const calculateDiscountPrice = (price: number, discountValue: number | null, isPercentage: boolean | null, status: number): number | undefined => {
    if ((DiscountStatus.Active !== status) || !discountValue) return;
    else if (isPercentage) {
        return price - price * discountValue / 100;
    }
    else {
        return price - discountValue;
    }
}