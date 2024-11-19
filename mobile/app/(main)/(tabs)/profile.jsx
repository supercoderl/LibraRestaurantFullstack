import { Stack } from 'expo-router'
import { View, Text, ScrollView, Platform, Pressable, Linking } from 'react-native'
import { useSafeAreaInsets } from 'react-native-safe-area-context'

import { BoxLink, Icons, Logout, Person } from '@/components'
import { useUserInfo } from '@/hooks'
import { v4 as uuid } from 'uuid';
import { useTranslation } from 'react-i18next'

export default function ProfileScreen() {
  //? Assets
  const insets = useSafeAreaInsets()
  const { userInfo, isLoading } = useUserInfo()
  const { t } = useTranslation();

  const profilePaths = [
    {
      name: t('favorite-food-title'),
      Icon: Icons.Feather,
      IconName: 'heart',
      path: '/profile/lists',
    },
    {
      name: t('my-comment'),
      Icon: Icons.FontAwesome5,
      IconName: 'comment',
      path: '/profile/reviews',
    },
    {
      name: t('check-order'),
      Icon: Icons.MaterialIcons,
      IconName: 'checklist-rtl',
      path: '/checkOrder',
    },
    {
      name: t('about'),
      Icon: Icons.MaterialIcons,
      IconName: 'storefront',
      path: '/about',
    },
    {
      name: t('privacy'),
      Icon: Icons.AntDesign,
      IconName: 'Safety',
      path: '/profile/personal-info',
    },
    {
      name: t('language'),
      Icon: Icons.AntDesign,
      IconName: 'earth',
      path: '/language',
    },
  ]

  //？Render(s)
  return (
    <>
      <Stack.Screen
        options={{
          title: t('profile')
        }}
      />
      <>
        <ScrollView className="bg-white">
          <View style={Platform.OS === "ios" && { paddingTop: insets.top + 60 }} className="flex bg-white">
            <View className="flex flex-row items-center px-4">
              <Person className="w-10 h-10 mr-2" />
              <View className="flex flex-col flex-1 gap-y-1">
                {isLoading ? (
                  <>
                    <View className="h-5 bg-red-200 rounded-md animate-pulse" />
                    <View className="w-32 h-5 bg-red-200 rounded-md animate-pulse" />
                  </>
                ) : (
                  <View className="gap-0">
                    <Text className=" text-lg font-bold">{t('customer')} #{uuid().split('-')[0]}</Text>
                    <Text className="text-xs text-gray-400">Hotline: 12090009</Text>
                  </View>
                )}
              </View>
              <Pressable onPress={() => Linking.openURL(`tel:12090009`)}>
                <Icons.Feather
                  name="phone-call"
                  size={20}
                  color="black"
                  className="icon text-gray-700  lg:mr-3"
                />
              </Pressable>
            </View>

            <View className="mt-2 px-4">
              {profilePaths.map((item, index) => (
                <BoxLink key={index} path={item.path} name={item.name}>
                  <item.Icon name={item.IconName} size={20} className="text-gray-700" />
                </BoxLink>
              ))}
              <Logout />
            </View>
          </View>
        </ScrollView>
      </>
    </>
  )
}
