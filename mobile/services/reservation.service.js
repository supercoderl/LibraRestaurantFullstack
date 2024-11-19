import apiSlice from './api'

export const reservationApiSlice = apiSlice.injectEndpoints({
  endpoints: builder => ({
    getReservationStatus: builder.query({
      query: ({ tableNumber, storeId }) => ({
        url: `Reservation/${tableNumber}/${storeId}`,
        method: 'GET',
      }),
      providesTags: (result, error, arg) => [{ type: 'Reservation' }],
    }),
    updateReservationAsync: builder.mutation({
      query: body => ({
        url: `Reservation/customer`,
        method: 'PUT',
        body,
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'Reservation' }],
    }),
  }),
})

export const { useLazyGetReservationStatusQuery, useUpdateReservationAsyncMutation } =
  reservationApiSlice
