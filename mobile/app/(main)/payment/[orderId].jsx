import { Link, Stack, useLocalSearchParams, useRouter } from 'expo-router'
import { useEffect, useState } from 'react'
import { View, Text, ScrollView, Pressable, Image, RefreshControl } from 'react-native'
import { useSafeAreaInsets } from 'react-native-safe-area-context'
import 'react-native-get-random-values'

import { Button, CartInfo, Icons } from '@/components'
import { RadioButton } from 'components/common/RadioButtons'
import { useGetPaymentMethodsQuery } from '@/services'
import { HandleResponse, Loading, TextField } from 'components'
import { useForm } from 'react-hook-form'
import { yupResolver } from '@hookform/resolvers/yup'
import { logInSchema } from 'utils'
import Toast from 'react-native-toast-message'
import { useTranslation } from 'react-i18next'
import { useLazyGetSingleOrderQuery, useUpdateOrderMutation } from 'services'
import { _get, _remove } from 'utils/storage'
import { clearCart, removeOrderId } from 'store'
import { clearReservation } from 'store/slices/reservation.slice'
import { OrderStatus } from 'enums'
import { useAppDispatch, useAppSelector } from 'hooks'
import { useSignalR } from 'contexts/signalRProvider'

export default function PaymentScreen(props) {
  //? Props
  const { orderId } = useLocalSearchParams()

  //? Assets
  const insets = useSafeAreaInsets()
  const router = useRouter()
  const { t } = useTranslation()
  const dispatch = useAppDispatch()
  const { sendMessageToGroup } = useSignalR()
  const { tableNumber } = useAppSelector(state => state.reservation)

  //? Get Data
  const { paymentMethods, isFetching, refetch } = useGetPaymentMethodsQuery(
    {
      includeDeleted: false,
    },
    {
      selectFromResult: ({ data, ...args }) => ({
        paymentMethods: data?.data?.items ?? [],
        ...args,
      }),
    }
  )

  //? Get order by id
  const [
    triggerGetOrderById,
    { isFetching: isOrderFetching, refetch: orderRefetch, isSuccess, isError, error },
  ] = useLazyGetSingleOrderQuery()

  const [
    updateOrder,
    {
      isLoading: isUpdateOrderLoading,
      isSuccess: isUpdateOrderSuccess,
      isError: isUpdateOrderError,
      error: updateOrderError,
    },
  ] = useUpdateOrderMutation()

  //? Form Hook
  const {
    handleSubmit,
    formState: { errors: formErrors },
    setFocus,
    control,
  } = useForm({
    resolver: yupResolver(logInSchema),
    defaultValues: { code: '' },
  })

  //? States
  const [paymentMethod, setPaymentMethod] = useState(8)
  const [order, setOrder] = useState(null)
  const [table, setTable] = useState(null)

  //? Handlers
  const handleRouter = async () => {
    const type = paymentMethods.find(x => x.paymentMethodId === paymentMethod)?.name
    if (!type) {
      Toast.show({
        text1: t('no-payment-selected'),
        type: 'error',
      })
      return
    } else if (type === 'Trực tiếp tại bàn') {
      if (order) {
        let body = {
          ...order,
          latestStatus: OrderStatus.Ready,
          latestStatusUpdate: new Date(),
          isPaid: false,
          isPreparationDelayed: false,
          isCanceled: false,
          isReady: true,
          isCompleted: false,
          action: 'pay',
        }

        setTimeout(() => {
          updateOrder({ body }).unwrap()
        }, 600)
      }
    }
    // router.navigate({ pathname: '/payment/paymentMethod', params: { method: type } })
  }

  useEffect(() => {
    const getOrder = async () => {
      const res = await triggerGetOrderById({ id: orderId })
      if (res && res?.data && res?.data?.data) {
        setOrder(res?.data?.data)
      }
    }

    orderId && getOrder()
  }, [orderId])

  useEffect(() => {
    async function fetchTable() {
      const myTable = await _get('my-table')
      setTable(myTable)
    }

    fetchTable()
  }, [])

  if (isFetching || isOrderFetching) {
    return (
      <View className="flex-1 bg-white justify-center">
        <Loading />
      </View>
    )
  }

  return (
    <>
      <Stack.Screen
        options={{
          title: t('pay'),
          headerBackTitleVisible: false,
        }}
      />
      {/*  Handle Create Order Response */}
      {(isUpdateOrderSuccess || isUpdateOrderError) && (
        <HandleResponse
          isError={isUpdateOrderError}
          isSuccess={isUpdateOrderSuccess}
          error={updateOrderError?.data?.message}
          message={'Thành công'}
          onSuccess={async () => {
            await sendMessageToGroup(table, `Khách bàn số ${tableNumber} yêu cầu thanh toán`, 'pay')
            await _remove('orderId')
            dispatch(clearCart())
            dispatch(clearReservation())
            dispatch(removeOrderId())
            router.navigate('(tabs)')
          }}
        />
      )}
      <>
        <View className="h-full bg-white relative">
          <ScrollView
            className="bg-white"
            refreshControl={
              <RefreshControl
                refreshing={isFetching || isOrderFetching}
                onRefresh={() => {
                  refetch && refetch()
                  orderRefetch && orderRefetch()
                }}
              />
            }
          >
            <View className="py-2 space-y-3">
              {/* header */}
              <View className="py-2">
                <View className="flex flex-row items-center justify-evenly">
                  <Link href="/cart" asChild>
                    <Pressable className="flex flex-col items-center gap-y-2">
                      <Icons.AntDesign
                        name="shoppingcart"
                        size={18}
                        className="text-red-300 icon"
                      />
                      <Text className="font-normal text-red-300">{t('cart')}</Text>
                    </Pressable>
                  </Link>

                  <View className="h-[1px] w-8  bg-red-300" />
                  <View className="flex flex-col items-center gap-y-2">
                    <Icons.AntDesign name="wallet" size={28} className="text-red-500 icon" />
                    <Text className="text-base font-normal text-red-500">{t('pay')}</Text>
                  </View>
                </View>
              </View>

              <View className="section-divide-y h-2 bg-gray-100" />

              {/* products */}
              <View className="py-1">
                <View className="flex flex-row gap-1 px-3 items-center">
                  <Icons.MaterialIcons name="payment" size={20} />
                  <Text className="text-[16px]">{t('payment-method')}</Text>
                </View>
                <View className="py-2 space-y-3">
                  {paymentMethods && paymentMethods.length > 0 ? (
                    paymentMethods.map((item, index) => (
                      <RadioButton
                        key={index}
                        text={item.name}
                        leftImage={{ uri: item.picture }}
                        isChecked={paymentMethod === item.paymentMethodId}
                        handleCheck={() => setPaymentMethod(item.paymentMethodId)}
                      />
                    ))
                  ) : (
                    <Text className="text-center text-red-500">
                      Không tìm thấy phương thức thanh toán
                    </Text>
                  )}
                </View>
              </View>

              <View className="section-divide-y h-2 bg-gray-100" />

              {/* cart info */}
              <View className="lg:border lg:border-gray-200 lg:rounded-md lg:h-fit">
                <View className="px-3 gap-2">
                  <View className="flex flex-row gap-1 items-center">
                    <Image
                      source={{ uri: 'https://cdn-icons-png.flaticon.com/512/2298/2298270.png' }}
                      className="w-5 h-5"
                    />
                    <Text className="text-[16px]">{t('discount')}</Text>
                  </View>
                  <View className="relative justify-center">
                    <TextField
                      errors={formErrors.code}
                      placeholder={t('input-code')}
                      name="code"
                      keyboardType="default"
                      autoCapitalize="none"
                      control={control}
                    />
                    <Button className={`flex-1 self-start absolute right-1 top-[1vw]`}>
                      {t('apply')}
                    </Button>
                  </View>
                </View>
                <CartInfo />
              </View>
            </View>
          </ScrollView>
          <View
            className="fixed bottom-0 left-0 right-0 z-10 flex flex-row items-center justify-between px-3 py-3 bg-white border-t border-gray-300 shadow-3xl"
            style={{ bottom: insets.bottom }}
          >
            <Button
              onPress={handleRouter}
              disabled={isUpdateOrderLoading}
              isLoading={isUpdateOrderLoading}
              className="w-full max-w-5xl mx-auto"
            >
              {t('submit')}
            </Button>
          </View>
        </View>
      </>
    </>
  )
}
