import { router } from 'expo-router'
import { Image, Text, TouchableOpacity, View } from 'react-native'

export default function SigninPromoRenderer(props) {
  //? Props
  const { tips = 'Đăng nhập để tiếp tục sử dụng các tính năng' } = props
  //? Handers
  const handleJumpLogin = () => {
    router.push('/login')
  }
  return (
    <View className="flex items-center justify-center h-full space-y-4 bg-white">
      <Image source={require('@/assets/images/sign-in-promo.png')} className="w-[100vw] h-[58vw]" />
      <View className="px-4 space-y-2 flex items-center justify-center">
        <Text className="text-lg">Người dùng không xác định</Text>
        <Text className="text-sm">{tips}</Text>
      </View>
      <TouchableOpacity
        onPress={handleJumpLogin}
        className="py-2 px-8 flex-center bg-red-500 rounded-full"
      >
        <Text className="text-sm text-white" href="/login">
          Đi đến đăng nhập
        </Text>
      </TouchableOpacity>
    </View>
  )
}
