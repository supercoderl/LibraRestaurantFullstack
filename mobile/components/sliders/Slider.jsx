import { View, Image, StyleSheet } from 'react-native'
import Swiper from 'react-native-swiper'

export default function Slider(props) {
  //? Props
  const { data } = props

  //? Render(s)
  if (data?.length === 0) return null

  return (
    <View className="mt-3 rounded-lg overflow-hidden">
      <Swiper
        style={styles.wrapper}
        showsPagination={false}
        activeDotColor="#FF6600"
        dotColor="#E5E7EB"
        autoplay
      >
        {data
          .filter(item => item)
          .map((item, index) => (
            <Image
              key={index}
              source={{
                uri: process.env.EXPO_PUBLIC_CLOUDINARY_URL + item,
              }}
              className="w-full h-full"
            />
          ))}
      </Swiper>
    </View>
  )
}

const styles = StyleSheet.create({
  wrapper: {
    height: 200,
  },
})
