import { Link, Stack, useLocalSearchParams, useRouter } from 'expo-router'
import { View, Text, ScrollView, Pressable, RefreshControl } from 'react-native'
import { useSafeAreaInsets } from 'react-native-safe-area-context'

import {
  AddToCartOperation,
  Description,
  Icons,
  ImageGallery,
  Info,
  InitialStore,
  OutOfStock,
  Reviews,
  ShowWrapper,
} from '@/components'
import { useAppSelector } from '@/hooks'
import { useGetSingleProductDetailQuery } from '@/services'
import { formatNumber } from '@/utils'
import { useAppDispatch } from 'hooks'
import { useState } from 'react'
import Alert from 'components/Alert'
import { addToCart } from 'store'
import Toast from 'react-native-toast-message'
import { useTranslation } from 'react-i18next'

export default function SingleProductScreen() {
  //? Assets
  const router = useRouter()
  const { id } = useLocalSearchParams()
  const insets = useSafeAreaInsets()
  const dispatch = useAppDispatch()
  const { t } = useTranslation()

  //? State
  const [showAlert, setShowAlert] = useState(false)

  //? Store
  const { totalItems } = useAppSelector(state => state.cart)
  const { reservationId } = useAppSelector(state => ({
    reservationId: state.reservation.id,
  }))

  //? Get Feeds Query
  const { product, isLoading, isSuccess, isFetching, error, isError, refetch } =
    useGetSingleProductDetailQuery(
      { slug: id },
      {
        selectFromResult: ({ data, ...args }) => ({
          product: data?.data || {},
          ...args,
        }),
      }
    )

  //? handlers
  const handleAddItem = () => {
    if (product?.quantity === 0)
      return Toast.show({
        type: 'error',
        text2: 'Món ăn đã bán hết',
      })
    else if (!id) return setShowAlert(true)
    else if (!reservationId) {
      Toast.show({
        text1: 'Thêm món vào giỏ hàng',
        text2: t('you-have-not-reservation'),
        type: 'error',
      })
      return
    }
    dispatch(
      addToCart({
        itemId: product?.itemId,
        title: product?.title,
        quantity: product?.quantity,
        price: product?.price,
        picture: product?.picture,
        recipe: product?.recipe,
        quantityInCart: 1,
      })
    )

    Toast.show({
      text1: "Thêm món",
      text2: t('added', { name: product?.title }),
      type: "success"
    })
  }

  const handleSubmit = () => {
    router.navigate('scan')
    setShowAlert(false)
  }

  return (
    <>
      <Stack.Screen
        options={{
          headerRight: () => (
            <>
              <Link href="/cart" asChild className="relative">
                <Pressable>
                  <Icons.AntDesign
                    name="shoppingcart"
                    size={24}
                    color="#1F2937"
                    className="px-2 py-1"
                  />
                  {formatNumber(totalItems) && (
                    <Pressable className="absolute outline outline-2 -top-1 -right-0 bg-red-500 rounded-sm w-4 h-4 p-0.5">
                      <Text className=" text-center text-[8px] text-white">
                        {formatNumber(totalItems)}
                      </Text>
                    </Pressable>
                  )}
                </Pressable>
              </Link>

              <Icons.Feather name="heart" size={20} color="#1F2937" className="px-2 py-1" />
            </>
          ),
          title: product?.title || '',
          headerBackTitleVisible: false,
        }}
      />
      <ShowWrapper
        error={error}
        isError={isError}
        refetch={refetch}
        isFetching={isFetching}
        isSuccess={isSuccess}
        type="detail"
      >
        <View className="h-full bg-white relative">
          <ScrollView
            className=""
            refreshControl={<RefreshControl refreshing={isFetching} onRefresh={refetch} />}
          >
            <View className="py-4 flex gap-y-4 ">
              <View className="h-fit">
                <InitialStore product={product} />
                <ImageGallery images={[product?.picture]} productName={product?.title} />
                <View className="lg:col-span-4 ">
                  {/* title */}
                  <Text className="p-4 text-base font-semibold leading-8 tracking-wide text-black/80 ">
                    {product?.title}
                  </Text>

                  <View className="section-divide-y h-2 bg-gray-100" />

                  {/* {product?.quantity > 0 && (
                    <SelectColor colors={product.colors} />
                  )} */}

                  {/* {product?.quantity > 0 && (
                    <SelectSize sizes={product.sizes} />
                  )} */}
                  {product?.quantity === 0 && <OutOfStock />}

                  <Info
                    sku={product?.sku}
                    quantity={product?.quantity}
                    instruction={product?.instruction}
                    recipe={product?.recipe}
                  />
                </View>
              </View>
              <View>
                {product?.summary?.length > 0 && <Description description={product?.summary} />}
              </View>
              {/* <SmilarProductsSlider smilarProducts={[]} /> */}

              <View className="section-divide-y h-2 bg-gray-100" />

              <Reviews numReviews={0} prdouctID={product?.itemId} productTitle={product?.title} />
            </View>
          </ScrollView>
          {product?.quantity > 0 && (
            <View className="fixed left-0 right-0 z-20" style={{ bottom: insets.bottom }}>
              <AddToCartOperation product={product} handleAddItem={handleAddItem} />
            </View>
          )}
        </View>
      </ShowWrapper>

      <Alert
        title="Chưa có bàn"
        description="Chúng tôi nhận thấy rằng bạn chưa đặt chỗ sẵn, hãy quét mã QR trên bàn để có thể gọi món."
        icon="alert-rhombus-outline"
        open={showAlert}
        handleClose={() => setShowAlert(false)}
        handleSubmit={handleSubmit}
      />
    </>
  )
}
