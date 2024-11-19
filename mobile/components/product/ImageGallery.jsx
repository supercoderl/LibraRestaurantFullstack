import { View } from 'react-native'
import Swiper from 'react-native-swiper'

import ResponsiveImage from '../common/ResponsiveImage'

const ImageGallery = props => {
  //? Porps
  const { images, productName } = props

  //? Render(s)
  return (
    <View className="mb-2">
      <Swiper className="h-[60vw]" showsPagination activeDotColor="#1D4ED8" dotColor="#E5E7EB">
        {images.map((image, index) => (
          <ResponsiveImage
            key={index}
            className="h-[60vw] w-full"
            imageStyles="h-[60vw] w-full"
            source={image}
            alt={productName}
          />
        ))}
      </Swiper>
    </View>
  )
}

export default ImageGallery
