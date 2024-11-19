import apiSlice from './api'

export const categoryApiSlice = apiSlice.injectEndpoints({
  endpoints: builder => ({
    getCategories: builder.query({
      query: () => ({
        url: 'Category?includeDeleted=false',
        method: 'GET',
      }),
      providesTags: (result, error, arg) =>
        result
          ? [
              ...result?.data?.items.map((_, index) => ({
                type: 'Category',
                id: index,
              })),
              'Category',
            ]
          : ['Category'],
    }),

    getSingleCategory: builder.query({
      query: ({ categoryId }) => ({
        url: `Category/${categoryId}`,
        method: 'GET',
      }),
      providesTags: (result, error, arg) => [{ type: 'Category', id: arg.categoryId }],
    }),

    updateCategory: builder.mutation({
      query: ({ id, body }) => ({
        url: `/api/v1/Category`,
        method: 'PUT',
        body,
      }),
      invalidatesTags: (result, error, arg) => [{ type: 'Category', id: arg.id }],
    }),

    createCategory: builder.mutation({
      query: ({ body }) => ({
        url: '/api/v1/Category',
        method: 'POST',
        body,
      }),
      invalidatesTags: ['Category'],
    }),
  }),
})

export const {
  useCreateCategoryMutation,
  useGetCategoriesQuery,
  useGetSingleCategoryQuery,
  useUpdateCategoryMutation,
} = categoryApiSlice
