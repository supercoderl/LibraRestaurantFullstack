import { FlashList } from '@shopify/flash-list'
import { Link } from 'expo-router'
import React from 'react'
import { Image, Pressable, Text, View } from 'react-native'

import FeedSectionContainer from './common/FeedSectionContainer'
import { useTranslation } from 'react-i18next'

export default function Categories(props) {
  //? Props
  const { categories } = props

  //? Assets
  const { t } = useTranslation()

  //? Re-Renders
  if (categories.length > 0) {
    return (
      <FeedSectionContainer title={t('category')}>
        <FlashList
          showsHorizontalScrollIndicator={false}
          data={categories}
          horizontal
          renderItem={({ item, index }) => (
            <Link
              key={item.categoryId}
              href={{
                pathname: '/products',
                params: { categoryId: item?.categoryId, title: item?.name },
              }}
              asChild
            >
              <Pressable className="flex items-center mr-3 space-y-2">
                <View className="w-14 h-14 rounded-full border-solid border-2 border-slate-200 overflow-hidden">
                  <Image
                    key={index}
                    source={{
                      uri: item.picture,
                    }}
                    className="w-full h-full"
                  />
                </View>
                <Text className="text-gray-700">{item.name}</Text>
              </Pressable>
            </Link>
          )}
          estimatedItemSize={400}
        />
      </FeedSectionContainer>
    )
  }
  return null
}
