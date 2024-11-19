import { Text, View } from 'react-native'

import EmptySearch from '../svgs/empty-search'
import { useTranslation } from 'react-i18next'

export default function EmptySearchList() {
  //? Assets
  const { t } = useTranslation();

  return (
    <View className="py-20">
      <EmptySearch className="mx-auto h-60 w-60" />
      <View className="max-w-md p-2 mx-auto space-y-2 border border-neutral-300 rounded-md">
        <View className="flex items-center gap-x-2">
          <Text>{t("empty-result")}</Text>
        </View>
        <Text className="text-gray-500">{t('use')}</Text>
      </View>
    </View>
  )
}
