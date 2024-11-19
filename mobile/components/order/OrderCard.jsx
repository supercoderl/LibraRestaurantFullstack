import moment from 'moment-jalaali'
import { Text, View } from 'react-native'

import Icons from '../common/Icons'

import { formatNumber } from '@/utils'
import { useTranslation } from 'react-i18next'

const OrderCard = props => {
  //? Assets
  const { t } = useTranslation()

  //? Props
  const { order } = props

  //? Render(s)
  return (
    <>
      <View className="py-4 space-y-3 border-b border-gray-200 lg:border lg:rounded-lg ">
        <View className="flex flex-row items-center justify-between lg:px-3">
          <View className="flex flex-row items-center gap-x-2 ">
            <Icons.FontAwesome name="phone" size={18} color="rgb(245, 158, 11)" />
            <Text className="text-sm text-black">{order.customerPhone}</Text>
          </View>
          <Text className="">{moment(order.latestStatusUpdate).format('YYYY-MM-DD HH:mm:ss')}</Text>
        </View>
        <View className="flex flex-row flex-wrap justify-between lg:px-3">
          <View className="flex flex-row">
            <Text>{t('bill-no')}:</Text>
            <Text className="ml-1 text-sm text-black">#{order.orderNo}</Text>
          </View>
          <View className="flex flex-row items-center gap-x-1">
            <Text className="text-black">
              {formatNumber(order.total)}
            </Text>
            <Text className="">₫</Text>
          </View>
        </View>
      </View>
    </>
  )
}

export default OrderCard
