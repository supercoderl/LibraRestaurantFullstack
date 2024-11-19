import { ActivityIndicator, Pressable, Text, View } from 'react-native'
import { useDispatch } from 'react-redux'

import Icons from '../common/Icons'

import { decrease, increase } from '@/store'
import { formatNumber } from '@/utils'

const CartButtons = props => {
  //? Props
  const { item, loading } = props

  //? Assets
  const dispatch = useDispatch()

  //? Render(s)
  return (
    <View className="flex flex-row items-center py-2 text-sm rounded-md bg-white shadow justify-evenly">
      <Pressable
        onPress={() => dispatch(increase(item?.itemId))}
        className="active:scale-90 bg-red-500 p-1 rounded-full"
      >
        <Icons.AntDesign name="plus" size={14} className="text-white icon" />
      </Pressable>

      <Text className="text-sm font-semibold min-w-[22px] text-center">
        {formatNumber(item?.quantityInCart)}
      </Text>

      {loading ? (
        <ActivityIndicator size={22} />
      ) : (
        <Pressable
          disabled={!item.hasMore}
          onPress={() => dispatch(decrease(item?.itemId))}
          className={`active:scale-90 border-[1px] 
          ${!item.hasMore ? 'border-gray-300' : 'border-red-500'} 
          rounded-full p-[3px]`}
        >
          <Icons.AntDesign
            name="minus"
            size={14}
            className={`${!item.hasMore ? 'text-gray-300' : 'text-red-500'} icon`}
          />
        </Pressable>
      )}
    </View>
  )
}

export default CartButtons
