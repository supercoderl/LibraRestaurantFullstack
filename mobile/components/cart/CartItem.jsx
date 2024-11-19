import { Link } from 'expo-router'
import { Pressable, StyleSheet, Text, View } from 'react-native'

import CartButtons from './CartButtons'
import DiscountCartItem from './DiscountCartItem'
import Icons from '../common/Icons'
import ResponsiveImage from '../common/ResponsiveImage'

import { formatNumber } from '@/utils'
import { useAppDispatch } from 'hooks'
import { removeFromCart } from 'store'

const CartItem = props => {
  //? Props
  const { item, loading } = props

  //? Assets
  const dispatch = useAppDispatch();

  //? Render(s)
  return (
    <View className="flex flex-row m-2 p-3 space-x-4 bg-white rounded-md" style={styles.shadow}>
      {/* image & cartButtons */}
      <View className="space-y-4">
        <ResponsiveImage
          dimensions="w-24 h-24"
          imageStyles="w-24 h-24"
          source={item?.picture}
          alt={item?.title}
        />
      </View>

      {/* name */}
      <View className="flex-auto">
        <View className="mb-1 flex flex-row items-center justify-between">
          <Text className="text-sm">
            <Link href={`/products/${item?.slug}`}>{item?.title}</Link>
          </Text>

          {item?.quantityInCart === 1 && item?.hasMore &&
            <Pressable type="button" onPress={() => dispatch(removeFromCart(item?.itemId))}>
              <Icons.MaterialCommunityIcons name="close" size={16} className="icon text-gray-500" />
            </Pressable>}
        </View>

        {/* info */}
        <View className="mb-1">
          <View className="flex flex-row items-center gap-x-2">
            <Text numberOfLines={2} className="font-light text-gray-400 truncate">{item?.recipe}</Text>
          </View>
        </View>

        {/* action */}
        <View className="flex-row items-center mt-auto justify-between ">
          {item.discount > 0 ? (
            <View>
              <DiscountCartItem discount={item?.discount || 0} price={item?.price} />
            </View>
          ) : (
            <View className="flex flex-row items-center gap-x-2">
              <Text className="text-[15px] font-bold text-red-500">{formatNumber(item?.price)} ₫</Text>
            </View>
          )}
          <CartButtons item={item} loading={loading} />
        </View>
      </View>
    </View>
  )
}

export default CartItem

const styles = StyleSheet.create({
  shadow: {
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2,
    },
    shadowOpacity: 0.25,
    shadowRadius: 3.84,
    elevation: 3
  }
})
