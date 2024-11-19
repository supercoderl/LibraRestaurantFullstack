import { Text, TouchableOpacity } from 'react-native'
import Toast from 'react-native-toast-message'

import Icons from './common/Icons'

import { useAppDispatch } from '@/hooks'
import { userLogout } from '@/store'
import { useTranslation } from 'react-i18next'

export default function Logout() {
  //? States
  const { t } = useTranslation();

  //? Assets
  const dispatch = useAppDispatch()

  //? Handlers
  const handleLogout = () => {
    dispatch(userLogout())
    Toast.show({
      type: 'success',
      text2: t('logout-success'),
    })
  }

  //? Render(s)
  return (
    <TouchableOpacity
      className="flex flex-row justify-between items-center transition-colors py-4 text-xs text-gray-700 w-full"
      onPress={handleLogout}
    >
      <Text className="text-red-500">{t('logout')}</Text>
      <Icons.MaterialIcons name="logout" size={24} className="text-red-500" />
    </TouchableOpacity>
  )
}
