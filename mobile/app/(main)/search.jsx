import { FlashList } from '@shopify/flash-list'
import { Link, Stack } from 'expo-router'
import { useState } from 'react'
import { View, Text, Pressable, TextInput } from 'react-native'

import {
  DiscountProduct,
  EmptySearchList,
  Icons,
  ProductPrice,
  ResponsiveImage,
  ShowWrapper,
} from '@/components'
import { useDebounce } from '@/hooks'
import { useGetProductsQuery } from '@/services'
import { truncate } from '@/utils'
import { useTranslation } from 'react-i18next'
import { useGetItemsQuery } from 'services'
import ProductCardV2 from 'components/product/ProductCardV2'

export default function SerachScreen() {
  //? Props

  //? States
  const [search, setSearch] = useState('')
  const { t } = useTranslation();

  //? Assets
  const debouncedSearch = useDebounce(search, 1200)

  //? Search Products Query
  const { data, isSuccess, isFetching, error, isError, refetch, originalArgs } =
    useGetItemsQuery(
      {
        search,
      },
      {
        skip: !debouncedSearch || search !== debouncedSearch,
        selectFromResult: ({ data, ...args }) => {
          return {
            data,
            ...args,
          }
        },
      }
    )

  //? Handlers
  const handleChange = value => {
    setSearch(value)
  }

  const handleRemoveSearch = () => {
    setSearch('')
  }

  //? Render(s)
  return (
    <>
      <Stack.Screen
        options={{
          title: t('search'),
          headerBackTitleVisible: false,
        }}
      />
      <View className="flex flex-col h-full p-3 bg-white gap-y-3">
        <View className="flex flex-row items-center rounded-md bg-zinc-200/80">
          <View className="p-2">
            <Icons.EvilIcons name="search" size={24} color="#1F2937" />
          </View>
          <TextInput
            className="flex-grow p-1 text-left bg-transparent outline-none input focus:border-none"
            type="text"
            value={search}
            onChangeText={handleChange}
          />
          <Pressable type="button" className="p-2" onPress={handleRemoveSearch}>
            <Icons.AntDesign name="close" size={14} className="icon text-gray-500" />
          </Pressable>
        </View>
        <View className="flex-1 py-3">
          <ShowWrapper
            error={error}
            isError={isError}
            refetch={refetch}
            isFetching={isFetching}
            isSuccess={isSuccess}
            dataLength={data ? data?.data?.items?.length : 0}
            emptyComponent={<EmptySearchList />}
            type="list"
            originalArgs={originalArgs}
          >
            <View className="h-full divide-y divide-neutral-200 space-y-3">
              {data?.data?.items?.length && data?.data?.items.length > 0 && search.length > 0 && (
                <FlashList
                  data={data?.data?.items}
                  renderItem={({ item, index }) => <ProductCardV2 product={item} key={index} />}
                  onEndReachedThreshold={0}
                  estimatedItemSize={200}
                  showsVerticalScrollIndicator={false}
                />
              )}
            </View>
          </ShowWrapper>
        </View>
      </View>
    </>
  )
}
