import { Link } from 'expo-router'
import { Pressable, StyleSheet, Text, TouchableOpacity, View } from 'react-native'
import Icons from '../common/Icons'
import ResponsiveImage from '../common/ResponsiveImage'

import { truncate } from '@/utils'
import { formatNumber } from 'utils'
import { DiscountTag } from 'components/tags/discountTag'
import screen from 'utils/screen'

const ProductCardV2 = props => {
    //? Props
    const { product } = props

    //? Render(s)
    return (
        <Link href={`/products/${product?.slug}`} asChild>
            <Pressable className="m-2 px-3 py-3.5 border-b border-gray-100 relative shadow bg-white rounded-md active:scale-95" style={styles.shadow}>
                <View className="flex flex-row items-center gap-3 space-x-3">
                    <ResponsiveImage
                        dimensions="h-[24vw] w-[24vw] mb-8"
                        imageStyles="h-[24vw] w-[24vw]"
                        source={{ uri: product?.picture }}
                        alt={product?.title}
                    />
                    <View className="flex-1 justify-between self-start">
                        <View className="gap-y-1">
                            <View className="flex flex-row items-center justify-between">
                                <Text className="text-lg leading-6 text-gray-800 break-all">
                                    {truncate(product?.title, screen.width * 0.05)}
                                </Text>
                                <TouchableOpacity>
                                    <Icons.MaterialCommunityIcons name="cards-heart-outline" size={20} />
                                </TouchableOpacity>
                            </View>
                            <View className="flex flex-row items-center gap-x-1">
                                <Icons.Octicons name="id-badge" size={16} className="text-gray-400" />
                                <Text className="text-neutral-500">{product?.sku}</Text>
                            </View>
                        </View>
                        <View className="flex flex-row justify-between mt-5">
                            <Text>{formatNumber(product?.price)} ₫</Text>
                            {/* <DiscountTag text="Khuyến mãi" /> */}
                        </View>
                    </View>
                </View>
            </Pressable>
        </Link>
    )
}

export default ProductCardV2

const styles = StyleSheet.create({
    shadow: {
        shadowColor: "#000",
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 3.84,
    }
})