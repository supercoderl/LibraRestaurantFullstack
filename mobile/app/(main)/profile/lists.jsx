'use client'

import { Stack } from 'expo-router'
import { Text, View } from 'react-native'

import { FavoritesListEmpty } from '@/components'
import { useTranslation } from 'react-i18next'

const ListsScreen = () => {
  //? Assets
  const { t } = useTranslation()

  //? Render(s)
  return (
    <>
      <Stack.Screen
        options={{
          title: t('favorite-food-title'),
          headerBackTitleVisible: false,
        }}
      />
      <View className="py-20 bg-white h-full">
        <FavoritesListEmpty className="mx-auto h-52 w-52" />
        <Text className="text-center">{t('favorite-empty')}</Text>
        <Text className="block my-3 text-base text-center text-amber-500">（{t('coming-soon')}）</Text>
      </View>
    </>
  )
}

export default ListsScreen
