import { Stack, router } from 'expo-router'
import { useEffect } from 'react'
import { RefreshControl, View } from 'react-native'

import { Icons, ShowWrapper } from '@/components'
import { useGetCategoriesQuery } from '@/services'
import CategoryItem from 'components/category/categoryItem'
import { FlashList } from '@shopify/flash-list'
import { OrderSkeleton } from 'components'
import { useTranslation } from 'react-i18next'

export default function CategoryScreen() {
  //? States
  const { t } = useTranslation()

  //? Get data Query
  const { categories, isSuccess, isFetching, error, isError, refetch } = useGetCategoriesQuery(
    {},
    {
      selectFromResult: ({ data, ...args }) => ({
        categories: data?.data?.items || [],
        ...args,
      }),
    }
  )

  //? State

  //? Handlers

  const handleSearch = () => {
    router.push('/search')
  }

  //? Re-Renders
  useEffect(() => {}, [])

  //? Render(s)
  return (
    <>
      <Stack.Screen
        options={{
          title: t('category'),
          headerRight: () => (
            <>
              <Icons.EvilIcons
                name="search"
                size={30}
                color="#1F2937"
                className="px-2 py-1"
                onPress={handleSearch}
              />
            </>
          ),
        }}
      />
      <ShowWrapper
        error={error}
        isError={isError}
        refetch={refetch}
        isFetching={isFetching}
        isSuccess={isSuccess}
        type="list"
        dataLength={categories?.length ?? 0}
      >
        <View className="flex h-full flex-row bg-white">
          {isFetching && <OrderSkeleton />}
          {categories && categories.length > 0 ? (
            <FlashList
              data={categories}
              numColumns={2}
              renderItem={({ item, index }) => (
                <CategoryItem
                  title={item?.name}
                  picture={item?.picture}
                  id={item?.categoryId}
                  key={index}
                />
              )}
              onEndReachedThreshold={0}
              estimatedItemSize={200}
              refreshControl={
                <RefreshControl
                  refreshing={isFetching}
                  onRefresh={refetch}
                />
              }
            />
          ) : null}
        </View>
      </ShowWrapper>
    </>
  )
}
