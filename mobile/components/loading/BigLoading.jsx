import { View } from 'react-native'

import Loading from './Loading'

export default function BigLoading() {
  return (
    <View className="flex items-center p-4 mx-auto space-y-4 text-center max-w-max ">
      <Loading />
    </View>
  )
}
