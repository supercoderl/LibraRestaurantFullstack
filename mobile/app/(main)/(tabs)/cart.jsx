import { Stack, useRouter } from 'expo-router'
import { View, Text, ScrollView, RefreshControl } from 'react-native'

import { Button, CartInfo, CartItem, EmptyCart } from '@/components'
import { useAppSelector } from '@/hooks'
import { formatNumber } from '@/utils'
import { useAppDispatch } from 'hooks'
import { clearCart, updateItemsInCart, updateOrder, updateOrderId, updateTotalItem } from 'store'
import { HandleResponse, Icons, Loading } from 'components'
import {
  useCreateOrderMutation,
  useLazyGetSingleOrderQuery,
  useUpdateOrderMutation,
} from 'services'
import { OrderStatus } from 'enums'
import { checkTimeDifference, generateOrderNo } from 'utils/date'
import { useEffect, useState } from 'react'
import { _get, _remove, _set } from 'utils/storage'
import 'react-native-get-random-values'
import { v4 as uuid } from 'uuid'
import { useTranslation } from 'react-i18next'
import Alert from 'components/Alert'
import Toast from 'react-native-toast-message'
import screen from 'utils/screen'
import { calculateDiscountPrice, calculateTotal } from 'utils/getTotal'
import { mergeItemsAndOrderLines } from 'utils/check'
import { useSignalR } from 'contexts/signalRProvider'

export default function CartScreen() {
  //? States
  const [count, setCount] = useState(0)
  const router = useRouter()
  const { t } = useTranslation()
  const [cancel, setCancel] = useState(false)
  const [loading, setLoading] = useState(true)
  const [renderItems, setRenderItems] = useState([])
  const [table, setTable] = useState(null)

  //? Create or Update Order
  const [
    createOrderAsync,
    {
      data: createdData,
      isSuccess: isCreatedSuccess,
      isError: isCreatedError,
      isLoading: isCreatedLoading,
      error: createdError,
      reset: createOrderReset,
    },
  ] = useCreateOrderMutation()
  const [
    updateOrderAsync,
    {
      isSuccess: isUpdatedSuccess,
      isError: isUpdatedError,
      isLoading: isUpdatedLoading,
      error: updatedError,
    },
  ] = useUpdateOrderMutation()

  //? Get order by id
  const [triggerGetOrderById, { isFetching, isSuccess, data }] = useLazyGetSingleOrderQuery()

  //? Store
  const { cartItems, totalItems, totalPrice, orderId, order } = useAppSelector(state => state.cart)
  const { tableNumber, storeId, id, customerId } = useAppSelector(state => state.reservation)
  const { discountType } = useAppSelector(state => state.discountType)
  const { items } = useAppSelector(state => state.item)
  const dispatch = useAppDispatch()
  const { sendMessageToGroup } = useSignalR()

  //? Handlers
  const handleRoute = async () => {
    if (!id) {
      Toast.show({
        text1: 'Lỗi gọi món',
        text2: 'Bạn chưa có bàn, vui lòng quét mã QR có trên bàn!',
        type: 'info',
      })
      return
    } else if (!table) {
      Toast.show({
        text1: 'Lỗi bàn',
        text2: t('your-reservation-have-error'),
        type: 'info',
      })
      return
    } else if (!storeId) {
      Toast.show({
        text1: 'Lỗi gọi món',
        text2: 'Vui lòng quét lại mã QR!',
        type: 'info',
      })
      return
    }

    let body = {
      orderNo: generateOrderNo(Number(tableNumber)),
      storeId: storeId,
      reservationId: Number(id),
      priceCalculated: calculateTotal(renderItems, discountType),
      subtotal: calculateTotal(renderItems, discountType),
      tax: 0,
      total: calculateTotal(renderItems, discountType),
      customerId,
      latestStatus: OrderStatus.InPreperation,
      latestStatusUpdate: new Date(),
      isPaid: false,
      isPreparationDelayed: false,
      isCanceled: false,
      isReady: false,
      isCompleted: false,
      orderLines: renderItems.map(item => ({
        orderId: uuid(),
        itemId: item?.itemId,
        quantity: item?.quantityInCart,
        isCanceled: false,
        foodPrice: item?.discount
          ? calculateDiscountPrice(
              item?.price,
              item?.discount?.discountValue,
              item?.discount?.isPercentage,
              item?.discountStatus
            )
          : item?.price,
      })),
    }

    if (orderId) {
      body.orderId = orderId
      body.action = 'update'
      await updateOrderAsync({ body }).unwrap()
    } else {
      await createOrderAsync({ body }).unwrap()
    }
    setCount(count + 1)
    await sendMessageToGroup(table, `Khách bàn số ${tableNumber} đã đặt món`, 'order')
  }

  const handleClickCancel = () => {
    if (order) {
      setCancel(true)
    } else {
      Toast.show({
        text1: 'Đơn không tồn tại!',
        type: 'error',
      })
    }
  }

  //? Init
  useEffect(() => {
    const checkOrder = async () => {
      const id = await _get('orderId')
      if (id) dispatch(updateOrderId(id))
    }

    checkOrder()
  }, [])

  useEffect(() => {
    setRenderItems(mergeItemsAndOrderLines(cartItems, order?.orderLines, items))
    dispatch(
      updateTotalItem({
        totalItems: mergeItemsAndOrderLines(cartItems, order?.orderLines, items).length,
        totalPrice: calculateTotal(mergeItemsAndOrderLines(cartItems, order?.orderLines, items)),
      })
    )
  }, [cartItems, order])

  //? Focus On Mount
  useEffect(() => {
    const getOrder = async () => {
      await triggerGetOrderById({ id: orderId }).finally(() => setLoading(false))
    }
    orderId ? getOrder() : setLoading(false)
  }, [count, orderId])

  useEffect(() => {
    if (isSuccess && data && data?.data) {
      dispatch(updateOrder(data?.data))
      if (cartItems.length === 0 && data?.data?.orderLines && data?.data?.orderLines.length > 0) {
        const itemsAddToCart = data?.data?.orderLines
          .map(x => {
            const item = items.find(i => i.itemId === x.itemId)
            if (item) {
              return {
                ...item,
                quantityInCart: x.quantity,
              }
            }
            return null
          })
          .filter(Boolean)
        dispatch(updateItemsInCart(itemsAddToCart))
      }
    }
  }, [data, isSuccess])

  useEffect(() => {
    async function fetchTable() {
      const myTable = await _get('my-table')
      setTable(myTable)
    }

    fetchTable()
  }, [])

  //? Render(s)
  if (loading) {
    return (
      <View className="flex-1 justify-center items-center bg-white">
        <Loading />
      </View>
    )
  }

  return (
    <>
      <Stack.Screen
        options={{
          title: `${t('cart')} (${t('${quantity}-dishes', { quantity: totalItems })})`,
          headerBackTitleVisible: false,
          headerRight: () => (
            <>
              <Icons.MaterialCommunityIcons
                name="notification-clear-all"
                size={24}
                color="#1F2937"
                className="px-2 py-1"
                onPress={() => dispatch(clearCart())}
              />
            </>
          ),
        }}
      />
      {/*  Handle Order Response */}
      {(isCreatedSuccess || isUpdatedSuccess || isCreatedError || isUpdatedError) && (
        <HandleResponse
          isError={isCreatedError || isUpdatedError}
          isSuccess={isCreatedSuccess || isUpdatedSuccess}
          error={createdError?.data?.message || updatedError?.data?.message || 'Có lỗi xảy ra'}
          message={t('order-success')}
          onSuccess={async () => {
            if (createdData && createdData?.data) {
              await _set('orderId', createdData?.data)
              dispatch(updateOrderId(createdData?.data))
              Toast.show({
                text1: 'Trạng thái gọi món',
                text2: 'Gọi món thành công!',
                type: 'error',
              })
              createOrderReset()
            }
          }}
        />
      )}
      {cartItems.length === 0 ? (
        <ScrollView
          className="bg-white"
          showsVerticalScrollIndicator={false}
          refreshControl={
            <RefreshControl
              refreshing={loading}
              onRefresh={() => {
                dispatch(clearCart())
                setLoading(true)
                setCount(count + 1)
              }}
            />
          }
        >
          <View className=" h-full space-y-3 bg-white">
            <View className="py-20">
              <EmptyCart className="mx-auto h-52 w-52" />
              <Text className="text-base font-bold text-center">{t('empty-cart')}！</Text>
            </View>
          </View>
        </ScrollView>
      ) : (
        <>
          <ScrollView
            className="bg-white"
            showsVerticalScrollIndicator={false}
            refreshControl={
              <RefreshControl
                refreshing={loading}
                onRefresh={() => {
                  setLoading(true)
                  setCount(count + 1)
                }}
              />
            }
          >
            <View className="py-4 mb-5 space-y-3">
              {/* title */}
              <View className="h-fit">
                <View className="flex flex-row justify-between px-4">
                  <View>
                    <Text className="mb-2 text-sm font-bold">{t('my-cart')}</Text>
                  </View>
                  <Text className="">{t('${quantity}-dishes', { quantity: totalItems })}</Text>
                </View>

                {/* carts */}
                <View className="divide-y">
                  {renderItems.map((e, index) => (
                    <CartItem item={e} key={index} loading={isFetching} />
                  ))}
                </View>

                <View
                  className={`flex flex-row px-2 ${!order ? 'justify-end' : 'justify-between'} mt-2`}
                >
                  {order && (
                    <Button
                      style={{ width: screen.width / 2 - 15 }}
                      onPress={handleClickCancel}
                      isOutlined
                    >
                      {t('cancel-order')}
                    </Button>
                  )}

                  <Button
                    style={{ width: screen.width / 2 - 15 }}
                    onPress={handleRoute}
                    disabled={isCreatedLoading || isUpdatedLoading}
                    isLoading={isCreatedLoading || isUpdatedLoading}
                  >
                    {t('order')}
                  </Button>
                </View>
              </View>
              <View className="section-divide-y h-2 bg-gray-100" />
              {/* cart Info */}
              <View className="">
                <View className="">
                  <CartInfo cart />
                </View>
              </View>
            </View>
          </ScrollView>
          {/* to Shipping */}
          <View className="fixed bottom-0 left-0 right-0 flex flex-row items-center justify-between px-3 py-3 bg-white border-t border-gray-300 shadow-3xl lg:hidden">
            <View>
              <Text className="font-light">{t('total')}</Text>
              <View className="flex flex-row items-center">
                <Text className="text-sm">{formatNumber(order?.total || totalPrice)}</Text>
                <Text className="ml-1">₫</Text>
              </View>
            </View>
            <Button
              className="w-1/2"
              onPress={() => {
                if (!orderId) {
                  Toast.show({
                    text1: 'Lỗi thanh toán',
                    text2: 'Hóa đơn không tồn tại, vui lòng đặt bàn và gọi món!',
                    type: 'error',
                  })
                  return
                }
                router.navigate(`payment/${orderId}`)
              }}
            >
              {t('pay')}
            </Button>
          </View>
        </>
      )}

      <Alert
        title={
          checkTimeDifference(order?.latestStatusUpdate, 10)
            ? 'Không thể hủy đơn'
            : 'Xác nhận hủy đơn'
        }
        description={
          checkTimeDifference(order?.latestStatusUpdate, 10)
            ? 'Món ăn của bạn đã được chế biến và sắp được mang ra, do đó bạn không thể hủy đơn này.'
            : 'Bạn chắc chắn muốn hủy đơn này? Việc này sẽ được thông báo ngay đến khu vực bếp!'
        }
        icon="bell-cancel-outline"
        open={cancel}
        handleClose={() => setCancel(false)}
        handleSubmit={() => setCancel(false)}
      />
    </>
  )
}
