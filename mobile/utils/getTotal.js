import { DiscountStatus } from 'enums'

export default function getTotal(items, attr) {
  const result = items.reduce((total, item) => {
    if (attr === 'price') {
      total += item.quantityInCart * item.price
    } else if (attr === 'quantity') {
      total = items.length
    }
    return total
  }, 0)

  return result
}

export const calculateTotal = (itemsInCart, discount) => {
  if (itemsInCart && itemsInCart.length > 0) {
    let totalPrice = itemsInCart.reduce((total, e) => {
      return (
        total +
        (calculateDiscountPrice(
          e.price * e.quantityInCart,
          e.discount?.discountValue,
          e.discount?.isPercentage,
          e.discountStatus
        ) ?? e.price * e.quantityInCart)
      )
    }, 0)

    if (discount) {
      if (discount.isPercentage) {
        totalPrice = totalPrice - (totalPrice * discount.value) / 100
      } else {
        totalPrice = totalPrice - discount.value
      }
    }

    return totalPrice
  }
  return 0
}

export const calculateDiscountPrice = (price, discountValue, isPercentage, status) => {
  if (DiscountStatus.Active !== status || !discountValue) return
  else if (isPercentage) {
    return price - (price * discountValue) / 100
  } else {
    return price - discountValue
  }
}
