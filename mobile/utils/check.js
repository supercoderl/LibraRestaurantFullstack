import { updateTotalItem } from 'store'

export const hasItem = obj => {
  return obj !== undefined
}

export const mergeItemsAndOrderLines = (cartItems, orderLines, items) => {
  const mergedItems = []

  // Thêm các order line vào mergedItems
  if (orderLines) {
    orderLines.forEach(orderLine => {
      const item = items.find(x => x.itemId === orderLine.itemId)
      if (item) {
        mergedItems.push({
          ...item,
          quantityInCart: orderLine.quantity,
          hasMore: false,
        })
      }
    })
  }

  // Thêm các item trong giỏ hàng vào mergedItems
  if (cartItems.length > 0) {
    cartItems.forEach(cart => {
      const existingItem = mergedItems.find(x => x.itemId === cart?.itemId)

      if (!existingItem) {
        mergedItems.push({ ...cart, hasMore: true }) // Thêm nếu chưa tồn tại
      } else {
        // Nếu có, kiểm tra quantity
        if (cart.quantityInCart > (existingItem?.quantityInCart || 0)) {
          existingItem.quantityInCart = cart.quantityInCart
          existingItem.hasMore = true
        }
      }
    })
  }

  return mergedItems
}
