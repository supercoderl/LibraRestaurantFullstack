import apiSlice from './api'

export const paymentMethodApiSlice = apiSlice.injectEndpoints({
  endpoints: builder => ({
    getPaymentMethods: builder.query({
      query: ({ includeDeleted }) => {
        return {
          url: `PaymentMethod`,
          method: 'GET',
          params: { includeDeleted },
        }
      },
      providesTags: result =>
        result
          ? [
              ...result?.data?.items.map((_, index) => ({
                type: 'PaymentMethod',
                id: index,
              })),
              'PaymentMethod',
            ]
          : ['PaymentMethod'],
    }),
  }),
})

export const { useGetPaymentMethodsQuery } = paymentMethodApiSlice
