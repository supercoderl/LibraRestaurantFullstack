import { createSlice, nanoid } from '@reduxjs/toolkit'
import Toast from 'react-native-toast-message'

import { exsitItem, getTotal } from '@/utils'

const initialState = {
  cartItems: [],
  totalItems: 0,
  totalPrice: 0,
  orderId: null,
  order: null,
}

const cartSlice = createSlice({
  name: 'cart',
  initialState,
  reducers: {
    addToCart: (state, action) => {
      const { itemId } = action.payload

      const isItemExist = exsitItem(state.cartItems, itemId)

      if (isItemExist) {
        isItemExist.quantityInCart += 1
        state.totalItems = getTotal(state.cartItems, 'quantity')
        state.totalPrice = getTotal(state.cartItems, 'price')
      } else {
        state.cartItems.push(action.payload)
        state.totalItems = getTotal(state.cartItems, 'quantity')
        state.totalPrice = getTotal(state.cartItems, 'price')
      }
    },

    removeFromCart: (state, action) => {
      const index = state.cartItems.findIndex(item => item.itemId === action.payload)

      if (index !== -1) {
        state.cartItems.splice(index, 1)
        state.totalItems = getTotal(state.cartItems, 'quantity')
        state.totalPrice = getTotal(state.cartItems, 'price')
      }
    },

    increase: (state, action) => {
      state.cartItems.forEach(cart => {
        if (cart.itemId === action.payload) {
          if (cart.quantityInCart < 99) {
            cart.quantityInCart += 1
          }
        }
      })
      state.totalItems = getTotal(state.cartItems, 'quantity')
      state.totalPrice = getTotal(state.cartItems, 'price')
    },

    decrease: (state, action) => {
      state.cartItems.forEach(cart => {
        if (cart.itemId === action.payload) {
          if (cart.quantityInCart > 1) {
            cart.quantityInCart -= 1
          }
        }
      })
      state.totalItems = getTotal(state.cartItems, 'quantity')
      state.totalPrice = getTotal(state.cartItems, 'price')
    },

    clearCart: state => {
      state.cartItems = []
      state.totalItems = 0
      state.totalPrice = 0
      state.order = null
    },

    removeOrderId: state => {
      state.orderId = null
      state.order = null
    },

    updateItemsInCart: (state, action) => {
      state.cartItems = action.payload
    },

    updateTotalItem: (state, action) => {
      state.totalItems = action.payload.totalItems
      state.totalPrice = action.payload.totalPrice
    },

    updateOrderId: (state, action) => {
      state.orderId = action.payload
    },

    updateOrder: (state, action) => {
      state.order = action.payload
    },
  },
})

export const {
  addToCart,
  removeFromCart,
  clearCart,
  decrease,
  increase,
  updateItemsInCart,
  updateTotalItem,
  updateOrderId,
  removeOrderId,
  updateOrder,
} = cartSlice.actions

export default cartSlice.reducer
