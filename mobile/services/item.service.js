import apiSlice from './api'

export const itemApiSlice = apiSlice.injectEndpoints({
  endpoints: builder => ({
    getItems: builder.query({
      query: ({ search, categoryId, includeDeleted, pageSize }) => ({
        url: 'Item',
        method: 'GET',
        params: { searchTerm: search, categoryId, includeDeleted, pageSize },
      }),
      providesTags: (result, error, arg) =>
        result
          ? [
              ...result?.data?.items.map(({ _id }) => ({
                type: 'Item',
                id: _id,
              })),
              'Item',
            ]
          : ['Item'],
    }),

    getSingleItem: builder.query({
      query: ({ id }) => ({
        url: `Item/${id}`,
        method: 'GET',
      }),
      providesTags: (result, error, arg) => [{ type: 'Item', id: arg.id }],
    }),

    updateItem: builder.mutation({
      query: ({ id, body }) => ({
        url: `Item`,
        method: 'PUT',
        body,
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'Item', id: arg.id }],
    }),

    createItem: builder.mutation({
      query: ({ body }) => ({
        url: 'Item',
        method: 'POST',
        body,
      }),
      invalidatesTags: ['Item'],
    }),
  }),
})

export const {
  useCreateItemMutation,
  useGetItemsQuery,
  useGetSingleItemQuery,
  useUpdateItemMutation,
} = itemApiSlice
