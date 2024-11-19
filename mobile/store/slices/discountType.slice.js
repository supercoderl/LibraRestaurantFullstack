import { createSlice } from '@reduxjs/toolkit'

const initialState = {
  discountTypes: [],
  discountType: null,
  loading: false,
}

const discountTypeSlice = createSlice({
  name: 'discountType',
  initialState,
  reducers: {
    updateDiscount: (state, action) => {
      state.discountType = action.payload
    },
  },
})

export const { updateDiscount } = discountTypeSlice.actions

export default discountTypeSlice.reducer
