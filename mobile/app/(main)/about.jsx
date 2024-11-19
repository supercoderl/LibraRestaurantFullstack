import { Icons, ResponsiveImage } from 'components'
import { AboutInfo } from 'components/about/info'
import { Stack, useLocalSearchParams, useRouter } from 'expo-router'
import { View, Text, StatusBar, ScrollView } from 'react-native'
import { useSafeAreaInsets } from 'react-native-safe-area-context'

export default function AboutScreen() {
  const router = useRouter()
  const insets = useSafeAreaInsets()

  return (
    <ScrollView>
      <Stack.Screen
        options={{
          headerShown: false
        }}
      />
      <StatusBar barStyle="light-content" />
      <View className="flex-1 bg-white">
        <Icons.Ionicons
          className="absolute z-10 text-white"
          name='arrow-back-outline'
          size={24}
          style={{
            top: insets.top + 20,
            left: insets.left + 20
          }}
          onPress={() => router.back()}
        />
        <ResponsiveImage
          source={{ uri: "https://thumbs.dreamstime.com/b/exotic-view-wooden-restaurant-stilts-background-azure-water-sunny-sky-exotic-view-wooden-restaurant-113614967.jpg" }}
          alt="background"
          dimensions="w-full h-20"
          imageStyles="w-screen h-[90vw] rounded-b-[10vw] mb-10"
        />
        <AboutInfo />
      </View>
    </ScrollView>
  )
}
