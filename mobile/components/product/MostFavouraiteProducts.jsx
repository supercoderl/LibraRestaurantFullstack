import { FontAwesome } from '@expo/vector-icons'
import { Link } from 'expo-router'
import { View, Text, TouchableOpacity, Image } from 'react-native'

import DiscountProduct from './DiscountProduct'
import ProductPrice from './ProductPrice'
import FeedSectionContainer from '../common/FeedSectionContainer'
import Skeleton from '../common/Skeleton'
import { useTranslation } from 'react-i18next'

export default function MostFavouraiteProducts(props) {
  //? Props
  const { products, isLoading } = props

  //? Assets
  const { t } = useTranslation()

  //? Render(s)
  return (
    <FeedSectionContainer title={t('favorite-food-title')}>
      <View className="w-full flex flex-row flex-wrap justify-between">
        {isLoading
          ? Array(10)
            .fill('_')
            .map((_, index) => (
              <Skeleton.Items
                key={index}
                className={`w-[49%] mr-[2%] mb-2 p-1 ${index % 2 === 1 ? 'mr-0' : ''}`}
              >
                <Skeleton.Item
                  height="h-32 md:h-36"
                  width="w-28 md:w-32"
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
            ))
          : products?.map((product, index) => (
            <Link
              href={{
                pathname: `/products/${product.slug}`,
              }}
              key={product.itemId}
              asChild
            >
              <TouchableOpacity
                key={product.itemId}
                className={`w-[48%] mb-4 py-1 px-2 transition rounded-lg ${index % 2 === 1 ? 'mr-0' : ''}`}
                style={{ backgroundColor: "rgba(0, 0, 0, 0.02)" }}
              >
                <Image
                  source={{
                    uri: product.picture,
                  }}
                  className="h-32 w-28 mx-auto"
                />
                <Text numberOfLines={1} className="text-[16px]">{product.title}</Text>
                <View className={`flex flex-row my-2 gap-x-2 justify-start`}>
                  <View className="flex flex-row items-center">
                    <FontAwesome name="star" size={14} color="rgb(251 191 36)" />
                    <Text className="text-xs text-gray-500"> {10} (10)</Text>
                  </View>
                  <View className="flex flex-row items-center">
                    <FontAwesome name="hourglass-1" size={14} color="rgb(251 191 36)" />
                    <Text className="text-xs text-gray-500"> 10 - 15 min</Text>
                  </View>
                </View>
                <ProductPrice
                  inStock={product.quantity}
                  discount={product.discount}
                  price={product.price}
                />
              </TouchableOpacity>
            </Link>
          ))}
      </View>
    </FeedSectionContainer>
  )
}
