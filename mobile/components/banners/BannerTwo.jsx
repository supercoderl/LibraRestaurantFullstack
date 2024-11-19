import { FlashList } from '@shopify/flash-list'
import { View, Image } from 'react-native'

import FeedSectionContainer from '../common/FeedSectionContainer'

export default function BannerTwo(props) {
  //? Props
  const { data } = props

  //? Render(s)
  if (data.length === 0) return null

  return (
    <FeedSectionContainer title="Sự kiện đang diễn ra">
      <FlashList
        data={data}
        renderItem={({ item, index }) => (
          <View className="h-[40vw] w-[90vw] mr-4" key={index}>
            <Image
              key={index}
              source={item.source}
              className="w-full h-full rounded-lg"
            />
          </View>
        )}
        horizontal
        estimatedItemSize={200}
      />
    </FeedSectionContainer>
  )
}
