import { useTranslation } from 'react-i18next'
import { Image, Text, View } from 'react-native'

export default function EmptyCustomList() {
  //? Assets
  const { t } = useTranslation();

  return (
    <View className="flex items-center justify-center h-full space-y-6 bg-white">
      <Image
        source={require('@/assets/images/list-empty-v2.png')}
        className="w-[60vw] h-[60vw] aspect-square"
      />
      <View className="px-4 space-y-2 flex items-center justify-center">
        <Text className="text-sm text-[5vw]">{t('empty-data')}</Text>
      </View>
    </View>
  )
}
