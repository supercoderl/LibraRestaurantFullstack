import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import apiSlice from 'services/api'

const initialState = {
  items: [],
  item: null,
}

const itemSlice = createSlice({
  name: 'item',
  initialState,
  reducers: {},
  extraReducers: builder => {
    builder.addMatcher(apiSlice.endpoints.getItems.matchFulfilled, (state, action) => {
      state.items = action.payload?.data?.items
    })
  },
})

export default itemSlice.reducer
