import { useTranslation } from 'react-i18next'
import { Text } from 'react-native'

export default function EmptyComment() {
  //? Assets
  const { t } = useTranslation();

  return <Text className="mt-6 text-red-800">{t('empty-comment')}</Text>
}
