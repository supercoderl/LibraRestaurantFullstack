import { FlashList } from '@shopify/flash-list'
import { Stack, useLocalSearchParams } from 'expo-router'
import { View, Text } from 'react-native'

import { Filter, ProductCard, ProductSkeleton, Sort, SubCategories } from '@/components'
import { useChangeRoute } from '@/hooks'
import { useGetCategoriesQuery, useGetProductsQuery } from '@/services'
import ProductCardV2 from 'components/product/ProductCardV2'
import { EmptyCustomList } from 'components'
import { useTranslation } from 'react-i18next'

export default function ProductsScreen() {
  //? Assets
  const params = useLocalSearchParams()
  const { t } = useTranslation();

  const categoryId = params?.categoryId ?? -1
  const title = params?.title ?? ''
  const page = params?.page?.toString() ?? 1

  //? Querirs
  //*    Get Products Data

  const {
    products,
    hasNextPage,
    isFetching: isFetchingProduct,
  } = useGetProductsQuery(
    {
      categoryId,
    },
    {
      selectFromResult: ({ data, ...args }) => ({
        products: data?.data?.items ?? [],
        ...args,
      }),
    }
  )

  //? Handlers
  const changeRoute = useChangeRoute()

  const onEndReachedThreshold = () => {
    if (!hasNextPage) return
    changeRoute({
      page: Number(page) + 1,
    })
  }

  const handleChangeRoute = newQueries => {
    changeRoute({
      ...params,
      page: 1,
      ...newQueries,
    })
  }

  //*    Get childCategories Data
  const {
    isLoading: isLoadingCategories,
    childCategories,
    currentCategory,
  } = useGetCategoriesQuery(undefined, {
    selectFromResult: ({ isLoading, data }) => {
      const currentCategory = []
      const childCategories = []
      return { childCategories, isLoading, currentCategory }
    },
  })

  return (
    <>
      <Stack.Screen
        options={{
          title: title,
        }}
      />
      <View className="bg-white h-full flex">
        <SubCategories
          childCategories={childCategories}
          name={currentCategory?.name}
          isLoading={isLoadingCategories}
        />
        <View className="px-1 pb-3 flex-1">
          <View id="_products" className="w-full h-[100%] flex  mt-2">
            {/* Filters & Sort */}
            <View className="divide-y-2 divide-neutral-200">
              <View className="flex flex-row py-2 gap-x-1">
                <Filter
                  mainMaxPrice={0}
                  mainMinPrice={0}
                  handleChangeRoute={handleChangeRoute}
                />
                <Sort handleChangeRoute={handleChangeRoute} />
              </View>

              <View className="flex flex-row justify-between p-2">
                <Text className="text-base text-neutral-600">{t('all')}</Text>

                <Text className="text-base text-neutral-600">
                  {products?.length ?? 0} {t('${quantity}-dishes', { quantity: totalItems })}
                </Text>
              </View>
            </View>
            {/* Products */}
            {isFetchingProduct && <ProductSkeleton />}
            {products && products.length > 0 ? (
              <FlashList
                data={products}
                renderItem={({ item, index }) => <ProductCardV2 product={item} key={item.itemId} />}
                onEndReached={onEndReachedThreshold}
                onEndReachedThreshold={0}
                estimatedItemSize={200}
                showsVerticalScrollIndicator={false}
              />
            ) : (
              <EmptyCustomList />
            )}
          </View>
        </View>
      </View>
    </>
  )
}
