import { FlashList } from '@shopify/flash-list'
import { Link } from 'expo-router'
import { View, Text, Image, Pressable } from 'react-native'

import FeedSectionContainer from '../common/FeedSectionContainer'
import Skeleton from '../common/Skeleton'

import { truncate } from '@/utils'
import { useTranslation } from 'react-i18next'

export default function BestSellsSlider(props) {
  //? Props
  const { products, isLoading } = props

  //? Assets
  const { t } = useTranslation()

  //? Render(s)

  return (
    <FeedSectionContainer title={t('best-sell')}>
      {isLoading ? (
        Array(2)
          .fill('_')
          .map((_, index) => (
            <Skeleton.Items key={index} className="flex flex-row p-2 space-x-4">
              <Skeleton.Item
                height="h-24"
                width="w-24"
                animated="background"
                className="rounded-md "
              />
              <Skeleton.Items className="flex items-start">
                <Skeleton.Item height="h-5" width="w-52" animated="background" className="mt-4" />
                <Skeleton.Item height="h-5" width="w-36" animated="background" className="mt-4" />
              </Skeleton.Items>
            </Skeleton.Items>
          ))
      ) : (
        <FlashList
          data={products}
          showsHorizontalScrollIndicator={false}
          renderItem={({ item, index }) => (
            <View className="mr-4">
              <Link
                href={{
                  pathname: `/products/${item.slug}`,
                }}
                key={item.itemId}
                asChild
                className="px-1 py-4 w-60"
              >
                <Pressable className="flex flex-row">
                  <Image
                    source={{
                      uri: item.picture,
                    }}
                    className="w-24 h-24 shrink-0 mr-2"
                  />
                  <View className="flex flex-auto flex-row items-center border-b border-gray-200">
                    <Text className="text-2xl text-sky-500 mx-2">{index + 1}</Text>
                    <Text className="flex-auto">{truncate(item.title, 15)}</Text>
                  </View>
                </Pressable>
              </Link>
            </View>
          )}
          horizontal
          estimatedItemSize={200}
        />
      )}
    </FeedSectionContainer>
  )
}
