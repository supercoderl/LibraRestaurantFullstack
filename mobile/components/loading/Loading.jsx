import React from 'react'
import { View } from 'react-native'
import LottieView from 'lottie-react-native';
import screen from 'utils/screen';

export default function Loading(props) {
  //? Props
  const { style } = props

  return (
    <View className="relative inline-block flex items-center justify-center bg-transparent" style={style}>
      <LottieView
        source={require('../../assets/animations/loading.json')}
        autoPlay
        loop
        style={{ width: screen.width / 2 + 80, height: screen.width / 2 + 80 }}
      />
    </View>
  )
}
