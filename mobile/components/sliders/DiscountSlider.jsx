import { FlashList } from '@shopify/flash-list'
import { Link, router } from 'expo-router'
import { View, Image, Pressable, TouchableOpacity, Text, ImageBackground, StyleSheet, FlatList } from 'react-native'

import FeedSectionContainer from '../common/FeedSectionContainer'
import Skeleton from '../common/Skeleton'
import DiscountProduct from '../product/DiscountProduct'
import ProductPrice from '../product/ProductPrice'
import { useTranslation } from 'react-i18next'

export default function DiscountSlider(props) {
  //? Props
  const { products, isLoading } = props

  //? handlers
  const handleJumptoMore = () => {
    router.push('/category')
  }

  //? Assets
  const { t } = useTranslation()

  //? Render(s)
  return (
    <FeedSectionContainer title={t('discount-foods')} showMore onJumptoMore={handleJumptoMore}>
      {isLoading ? (
        <FlashList
          data={Array(10).fill('_')}
          renderItem={({ item, index }) => (
            <Skeleton.Items className="mr-2" key={index}>
              <Skeleton.Item
                height=" h-32 lg:h-36"
                width="w-32 lg:w-36"
                animated="background"
                className="rounded-md mx-auto"
              />
              <Skeleton.Item
                height="h-5"
                width="w-32"
                animated="background"
                className="mt-4 mx-auto"
              />
              <Skeleton.Item
                height="h-5"
                width="w-20"
                animated="background"
                className="mt-4 mx-auto"
              />
            </Skeleton.Items>
          )}
          horizontal
          estimatedItemSize={200}
        />
      ) : !products.length ? null : (
        <FlatList
          data={products}
          keyExtractor={(_, index) => index.toString()}
          contentContainerStyle={{ gap: 10 }}
          showsHorizontalScrollIndicator={false}
          renderItem={({ item }) => (
            <Link
              href={{
                pathname: `/products/${item.slug}`,
              }}
              key={item.itemId}
              asChild
            >
              <TouchableOpacity className="w-40 h-28 overflow-hidden relative rounded-lg">
                <ImageBackground
                  source={{
                    uri: item?.picture,
                  }}
                  className="flex-1"
                  resizeMode="cover"
                >
                  <View className="flex-1 justify-end px-1.5 pb-1" style={{ backgroundColor: "rgba(0, 0, 0, 0.3)" }}>
                    <Text numberOfLines={1} className="text-white text-[16px]">{item?.title}</Text>
                  </View>
                </ImageBackground>
              </TouchableOpacity>
            </Link>
          )}
          horizontal
          estimatedItemSize={200}
        />
      )}
    </FeedSectionContainer>
  )
}
