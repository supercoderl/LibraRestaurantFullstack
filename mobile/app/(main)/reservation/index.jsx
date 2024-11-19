import { FlashList } from '@shopify/flash-list';
import { ProductCard, ProductSkeleton } from 'components';
import { Stack, useLocalSearchParams } from 'expo-router'
import { View, Text } from 'react-native';
import { useGetProductsQuery } from 'services';

export default function ReservationScreen() {
    //? Assets
    const params = useLocalSearchParams();

    //? Handlers


    //*    Get Products
    const {
        products,
        isFetching,
    } = useGetProductsQuery(
        {},
        {
            selectFromResult: ({ data, ...args }) => ({
                products: data?.data?.items || [],
                ...args,
            }),
        }
    );

    const onEndReachedThreshold = () => {
        // if (!hasNextPage) return
        // changeRoute({
        //   page: Number(page) + 1,
        // })
    }

    return (
        <>
            <Stack.Screen
                options={{
                    title: "Đặt chỗ của bạn"
                }}
            />
            <View className="bg-white h-full flex" style={{ flex: 1 }}>
                <View className="px-1 flex-1" style={{ flex: 1 }}>
                    <View id="_products" className="w-full h-[100%] flex px-4 py-2 mt-2" style={{ flex: 1 }}>
                        {/* Products */}
                        {isFetching && <ProductSkeleton />}
                        {products && products.length > 0 ? (
                            <FlashList
                                data={products}
                                showsVerticalScrollIndicator={false}
                                renderItem={({ item, index }) => <ProductCard product={item} key={item.itemId} />}
                                onEndReached={onEndReachedThreshold}
                                onEndReachedThreshold={0}
                                estimatedItemSize={200}
                            />
                        )
                            :
                            <Text className="text-center text-red-500">Không tìm thấy món ăn</Text>
                        }
                    </View>
                </View>
            </View>
        </>
    )
}
