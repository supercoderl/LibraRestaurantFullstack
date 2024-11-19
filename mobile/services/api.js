import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'

const apiSlice = createApi({
  reducerPath: 'api',
  baseQuery: fetchBaseQuery({
    baseUrl: process.env.EXPO_PUBLIC_API_URL,
    prepareHeaders: (headers, { getState }) => {
      return headers
    },
  }),
  tagTypes: ['Category', 'Feed', 'Item', 'PaymentMethod', 'Reservation', 'Order', 'Payment'],
  endpoints: builder => ({}),
})

export default apiSlice
