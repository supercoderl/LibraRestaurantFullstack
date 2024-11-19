import { FlashList } from '@shopify/flash-list'
import { Icons, ProductSkeleton, ResponsiveImage, ShowWrapper } from 'components'
import ProductCardV2 from 'components/product/ProductCardV2'
import { Stack, useLocalSearchParams } from 'expo-router'
import { useTranslation } from 'react-i18next'
import { RefreshControl, Text, TouchableOpacity, View } from 'react-native'
import { useGetProductsQuery } from 'services'
import screen from 'utils/screen'

export default function SingleCategoryScreen() {
  //? Assets
  const params = useLocalSearchParams()
  const id = params?.id ?? -1
  const picture = params?.picture ?? ''
  const title = params?.title ?? ''
  const { t } = useTranslation()

  console.log(picture)

  //? Get data Query
  const { products, isSuccess, isFetching, error, isError, refetch } = useGetProductsQuery(
    { categoryId: id },
    {
      selectFromResult: ({ data, ...args }) => ({
        products: data?.data?.items || [],
        ...args,
      }),
    }
  )

  return (
    <>
      <Stack.Screen
        options={{
          title: `${t('menu')} / ${title}`,
        }}
      />
      <ShowWrapper
        error={error}
        isError={isError}
        refetch={refetch}
        isFetching={isFetching}
        isSuccess={isSuccess}
        type="list"
        dataLength={products?.length ?? 0}
      >
        <View className="bg-white relative" style={{ height: screen.height * 0.3 }}>
          <ResponsiveImage
            dimensions="w-full h-full"
            imageStyles="w-full h-full"
            source={picture}
            alt={title}
          />
          <TouchableOpacity className="absolute top-4 right-4">
            <Icons.MaterialCommunityIcons
              name="filter-outline"
              size={20}
              className="text-gray-500"
            />
          </TouchableOpacity>
          <Text className="text-black absolute bottom-4 left-4 font-semibold text-gray-500">
            {t('${quantity}-dishes', { quantity: products.length ?? 0 })}
          </Text>
        </View>
        <View className="p-2 bg-white h-full flex-1">
          {isFetching && <ProductSkeleton />}
          {products && products.length > 0 && (
            <FlashList
              data={products}
              renderItem={({ item, index }) => <ProductCardV2 product={item} key={item.itemId} />}
              onEndReachedThreshold={0}
              estimatedItemSize={200}
              showsVerticalScrollIndicator={false}
              refreshControl={<RefreshControl refreshing={isFetching} onRefresh={refetch} />}
            />
          )}
        </View>
      </ShowWrapper>
    </>
  )
}
