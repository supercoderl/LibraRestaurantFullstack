import { Text, View } from 'react-native'

import OrderEmpty from '../svgs/order-empty'
import { useTranslation } from 'react-i18next'

export default function EmptyCommentsList() {
  //? Assets
  const { t } = useTranslation();
  return (
    <View className="py-20">
      <OrderEmpty className="mx-auto h-52 w-52" />

      <Text className="text-center">{t('empty-comment-list')}</Text>
    </View>
  )
}
