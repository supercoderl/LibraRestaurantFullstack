import apiSlice from './api'

export const paymentApiSlice = apiSlice.injectEndpoints({
  endpoints: builder => ({
    pay: builder.mutation({
      query: ({ type, body }) => ({
        url: `Payment/${type}`,
        method: 'POST',
        body,
      }),
      invalidatesTags: ['Payment'],
    }),
  }),
})

export const { usePayMutation } = paymentApiSlice
