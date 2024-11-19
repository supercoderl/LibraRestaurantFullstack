import { FlashList } from '@shopify/flash-list'
import { EmptySearchList, Icons, OrderCard, ShowWrapper } from 'components'
import { Stack } from 'expo-router'
import { useState } from 'react'
import { useTranslation } from 'react-i18next'
import { ActivityIndicator, Pressable, Text, TextInput, View } from 'react-native'
import { useLazyGetOrdersByPhoneQuery } from 'services'

export default function CheckOrderScreen() {
  //? Assets
  const { t } = useTranslation()
  const [phone, setPhone] = useState('')

  //? Get data Query
  const [getOrdersByPhone, { orders, isSuccess, isFetching, isLoading, error, isError }] =
    useLazyGetOrdersByPhoneQuery({
      selectFromResult: ({ data, ...args }) => ({
        orders: data?.data?.items || [],
        ...args,
      }),
    })

  //? Handlers
  const handleSubmit = () => {
    getOrdersByPhone({ phone })
  }

  return (
    <>
      <Stack.Screen
        options={{
          title: 'Kiểm tra hóa đơn',
        }}
      />
      <View className="flex flex-col h-full p-3 bg-white gap-y-3">
        <View className="flex flex-row items-center rounded-md bg-gray-200">
          <TextInput
            className="flex-grow h-11 p-1 pl-3 text-left outline-none input focus:border-none"
            type="text"
            placeholder="Nhập số điện thoại..."
            value={phone}
            onChangeText={text => setPhone(text)}
          />
          <Pressable type="button" className="p-2" onPress={handleSubmit}>
            {isFetching ? (
              <ActivityIndicator size={22} />
            ) : (
              <Icons.MaterialIcons name="search" size={22} color="#1F2937" />
            )}
          </Pressable>
        </View>
        <View className="flex-1 py-3">
          <ShowWrapper
            error={error}
            isError={isError}
            refetch={() => {}}
            isFetching={isFetching}
            isSuccess={isSuccess}
            dataLength={orders?.length || 0}
            emptyComponent={<EmptySearchList />}
            type="list"
          >
            <View className="h-full divide-y divide-neutral-200 space-y-3">
              {orders.length > 0 && (
                <FlashList
                  data={orders}
                  renderItem={({ item, index }) => <OrderCard order={item} key={index} />}
                  onEndReachedThreshold={0}
                  estimatedItemSize={200}
                  showsVerticalScrollIndicator={false}
                />
              )}
            </View>
          </ShowWrapper>
        </View>
      </View>
    </>
  )
}
