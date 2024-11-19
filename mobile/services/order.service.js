import apiSlice from './api'

export const orderApiSlice = apiSlice.injectEndpoints({
  endpoints: builder => ({
    getOrdersList: builder.query({
      query: (pageSize, page, phone) => ({
        url: `/api/order/list`,
        method: 'GET',
        params: { pageSize, page, phone },
      }),

      providesTags: (result, error, arg) =>
        result
          ? [
              ...result.data.orders.map(({ _id }) => ({
                type: 'Order',
                id: _id,
              })),
              'Order',
            ]
          : ['Order'],
    }),

    getOrders: builder.query({
      query: ({ page, pageSize, phone, includeDeleted }) => ({
        url: `Order`,
        method: 'GET',
        params: {
          page: page || 1,
          pageSize: pageSize || 10,
          phone,
          includeDeleted: includeDeleted || false,
        },
      }),
      serializeQueryArgs: ({ queryArgs, ...rest }) => {
        const newQueryArgs = { ...queryArgs }
        if (newQueryArgs.page) {
          delete newQueryArgs.page
        }
        return newQueryArgs
      },
    }),

    getOrdersByPhone: builder.query({
      query: ({ page, pageSize, phone, includeDeleted }) => ({
        url: `Order/customer`,
        method: 'GET',
        params: {
          page: page || 1,
          pageSize: pageSize || 10,
          phone,
          includeDeleted: includeDeleted || false,
        },
      }),
      serializeQueryArgs: ({ queryArgs, ...rest }) => {
        const newQueryArgs = { ...queryArgs }
        if (newQueryArgs.page) {
          delete newQueryArgs.page
        }
        return newQueryArgs
      },
    }),

    getSingleOrder: builder.query({
      query: ({ id }) => ({
        url: `Order/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, arg) => [{ type: 'Order', id: arg.orderId }],
    }),

    updateOrder: builder.mutation({
      query: ({ body }) => ({
        url: `Order`,
        method: 'PUT',
        body,
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'Order', id: arg }],
    }),

    createOrder: builder.mutation({
      query: ({ body }) => ({
        url: 'Order',
        method: 'POST',
        body,
      }),
      invalidatesTags: ['Order'],
    }),
  }),
})

export const {
  useGetOrdersQuery,
  useGetSingleOrderQuery,
  useLazyGetSingleOrderQuery,
  useUpdateOrderMutation,
  useCreateOrderMutation,
  useGetOrdersListQuery,
  useLazyGetOrdersQuery,
  useLazyGetOrdersByPhoneQuery,
} = orderApiSlice
