import { Text, View } from 'react-native'

import { useAppSelector } from '@/hooks'
import { useTranslation } from 'react-i18next'

const CartInfo = props => {
  //? States
  const { t } = useTranslation()

  //? Store
  const { totalItems, totalPrice } = useAppSelector(state => state.cart)

  //? Render(s)
  return (
    <View className="px-4 py-2 mt-2 space-y-5 lg:mt-0 lg:h-fit lg:py-4">
      {/* total cart price */}
      <View className="pb-2 border-b border-gray-200 flex flex-row justify-between">
        <Text className="text-sm">
          {t('subtotal')} ({t('${quantity}-dishes', { quantity: totalItems })})
        </Text>
        <View className="flex-center flex-row">
          <Text className="">{totalPrice}</Text>
          <Text className="ml-1">₫</Text>
        </View>
      </View>

      {/* total cart items */}
      <View className="flex flex-row justify-between">
        <Text>{t('total')}</Text>
        <View className="flex-center flex-row">
          <Text className="text-sm">{totalPrice}</Text>
          <Text className="ml-1">₫</Text>
        </View>
      </View>

      <Text className="inline-block w-full pb-2 border-b border-gray-200 italic">
        "{t('cart-text')}"
      </Text>

      {/* total cart profit */}
      <View className="flex flex-row justify-between">
        <Text className="text-red-500">{t('member-point')}:</Text>
        <View className="flex-center flex-row gap-x-1">
          <Text className="text-red-500 text-sm">+ 10%</Text>
          <Text className="text-red-500">{10000}</Text>
          <Text className="ml-1 text-red-500">₫</Text>
        </View>
      </View>
    </View>
  )
}

export default CartInfo
