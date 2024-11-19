import { useTranslation } from 'react-i18next'
import { Text, View } from 'react-native'

const Info = props => {
  //? Props
  const { sku, quantity, instruction, recipe } = props

  //? Assets
  const { t } = useTranslation();

  //? Render(s)
  return (
    <View className="px-3 pb-2 border-b-4 border-gray-100">
      <Text className="py-3">{t('information')}</Text>
      <View className="ml-1 gap-y-2">
        <View className="flex flex-row gap-x-2 tracking-wide text-gray-500">
          <Text className="leading-6 text-gray-500">{t('sku')} :</Text>
          <Text className="text-gray-900 flex-1 leading-6">{sku}</Text>
        </View>
        <View className="flex flex-row gap-x-2 tracking-wide text-gray-500">
          <Text className="leading-6 text-gray-500">{t('remain')} :</Text>
          <Text className="text-gray-900 flex-1 leading-6">{quantity}</Text>
        </View>
        <View className="flex flex-row gap-x-2 tracking-wide text-gray-500">
          <Text className="leading-6 text-gray-500">{t('recipe')} :</Text>
          <Text className="text-gray-900 flex-1 leading-6">{recipe}</Text>
        </View>
        <View className="flex flex-row gap-x-2 tracking-wide text-gray-500">
          <Text className="leading-6 text-gray-500">{t('instruction')} :</Text>
          <Text className="text-gray-900 flex-1 leading-6">{instruction}</Text>
        </View>
      </View>
    </View>
  )
}

export default Info
