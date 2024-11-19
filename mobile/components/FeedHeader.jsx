import { router } from 'expo-router'
import { Pressable, Text, TouchableOpacity, View } from 'react-native'
import { useSafeAreaInsets } from 'react-native-safe-area-context'

import Search from './Search'
import Icons from './common/Icons'
import Logo from './svgs/logo.svg'

import { useAppSelector } from '@/hooks'
import { formatNumber } from '@/utils'
import { _get } from 'utils/storage'
import { CurvedNumberIcon } from './icons/CurvedNumber'

export default function FeedHeader() {
  //? Assets
  const insets = useSafeAreaInsets()

  //? Store
  const { totalItems } = useAppSelector(state => state.cart);
  const { tableNumber } = useAppSelector(state => state.reservation);

  //? Handlers
  const handleIconClick = path => {
    router.push(path)
  }

  //? Render(s)
  return (
    <View style={{ paddingTop: insets.top }} className="p-3 bg-white shadow-sm">
      <View className="flex flex-row items-center justify-between">
        <Logo width={200} height={50} />
        <View className="flex flex-row space-x-3 pr-1">
          {
            tableNumber && tableNumber !== -1 ?
              <TouchableOpacity
                onPress={() => handleIconClick('/scan')}
                className="flex items-center flex-row"
              >
                <CurvedNumberIcon number={tableNumber} size={24} />
              </TouchableOpacity>
              :
              <TouchableOpacity
                onPress={() => {
                  handleIconClick('/scan')
                }}
              >
                <Icons.MaterialCommunityIcons name="qrcode-scan" size={24} color="black" />
              </TouchableOpacity>
          }

          <Pressable
            onPress={() => {
              handleIconClick('/cart')
            }}
            className="relative"
          >
            <Icons.Feather name="shopping-bag" size={24} color="#1F2937" />
            {formatNumber(totalItems) && (
              <View className="absolute outline outline-2 -top-2 -right-2 bg-red-500 rounded-sm w-4 h-4 p-0.5">
                <Text className=" text-center text-[8px] text-white">{formatNumber(totalItems)}</Text>
              </View>
            )}
          </Pressable>
        </View>
      </View>
      <Search />
    </View>
  )
}
