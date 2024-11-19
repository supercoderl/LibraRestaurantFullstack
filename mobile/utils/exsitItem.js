export default function exsitItem(cartItems, itemId) {
  const result = cartItems.find(item => item.itemId === itemId)

  return result
}
