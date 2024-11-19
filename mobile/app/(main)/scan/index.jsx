import { Button, HandleResponse, Icons } from 'components'
import { router, Stack } from 'expo-router'
import { useEffect, useRef, useState } from 'react'
import { View, Text, StyleSheet, SafeAreaView } from 'react-native'
import { CameraView, useCameraPermissions } from 'expo-camera'
import { Overlay } from './overlay'
import { _set } from 'utils/storage'
import { useAppDispatch, useAppSelector } from 'hooks'
import { updateReservation, updateReservationStatus } from 'store/slices/reservation.slice'
import { Dialog } from 'react-native-paper'
import {
  useLazyGetReservationStatusQuery,
  useUpdateReservationAsyncMutation,
} from 'services/reservation.service'
import { Step1 } from './step1'
import { Step2 } from './step2'
import { useTranslation } from 'react-i18next'
import Toast from 'react-native-toast-message'
import { Status } from 'enums'
import { useSignalR } from 'contexts/signalRProvider'

export default function ScanScreen() {
  //? Assets
  const cameraRef = useRef(null)
  const [scanned, setScanned] = useState(false)
  const [isFirst, setIsFirst] = useState(true)
  const dispatch = useAppDispatch()
  const [step, setStep] = useState(1)
  const [showAlert, setShowAlert] = useState(false)
  const [jsonValue, setJsonValue] = useState(null)
  const { t } = useTranslation()
  const { joinTableGroup } = useSignalR();

  const [facing, setFacing] = useState('back')
  const [permission, requestPermission] = useCameraPermissions()

  const [
    triggerGetReservationStatus,
    { isLoading: getLoading, isSuccess, data: resultData, isError: getError, error: resultError },
  ] = useLazyGetReservationStatusQuery()
  const [updateReservationAsync, { isLoading, isError, error }] =
    useUpdateReservationAsyncMutation()

  //? States
  const reservation = useAppSelector(state => state.reservation)

  //? Handlers
  const toggleCameraFacing = () => {
    setFacing(current => (current === 'back' ? 'front' : 'back'))
  }

  const handleBarCodeScanned = async ({ type, data }) => {
    setScanned(true)
    if (data) {
      const jsonData = JSON.parse(data)
      setJsonValue(jsonData)
      await triggerGetReservationStatus({
        tableNumber: jsonData?.tableNumber,
        storeId: jsonData?.storeId,
      }) // Gọi API với categoryId
    }
  }

  useEffect(() => {
    if (resultData) {
      dispatch(
        updateReservation({
          reservationId: resultData?.data?.reservationId,
          isChanged:
            reservation.tableNumber !== -1 &&
            reservation.tableNumber !== resultData?.data?.tableNumber,
          capacity: resultData?.data?.capacity,
          status: resultData?.data?.status,
          storeId: resultData?.data?.storeId,
          tableNumber: resultData?.data?.tableNumber,
          customerId: resultData?.data?.customerId,
          customerPhone: resultData?.data?.customerPhone,
        })
      )
    }
  }, [resultData])

  useEffect(() => {
    if (jsonValue && reservation.status !== -1) {
      setShowAlert(true)
    }
  }, [reservation.status, jsonValue])

  if (!permission) {
    return <View />
  }

  if (!permission.granted) {
    // Camera permissions are not granted yet.
    return (
      <View className="flex-1 justify-center px-4">
        <Text className="text-center pb-[10px]">Bạn phải mở quyền để truy cập vào máy ảnh</Text>
        <Button onPress={requestPermission}>Mở quyền</Button>
      </View>
    )
  }

  const onSubmit = async ({ name, mobile }) => {
    setScanned(false)
    setShowAlert(false)
    setStep(1)
    if (reservation.status === Status.Available) {
      const res = await updateReservationAsync({
        reservationId: reservation.id,
        status: Status.Occupied,
        customerName: name,
        customerPhone: mobile,
      })

      if (res?.data?.success) {
        dispatch(
          updateReservation({
            reservationId: reservation.id,
            isChanged: reservation.isChanged,
            capacity: reservation.capacity,
            status: reservation.status,
            storeId: reservation.storeId,
            tableNumber: reservation.tableNumber,
            customerId: res?.data?.data,
            customerPhone: mobile,
          })
        )
      }
    } else if (reservation.status === Status.Occupied) {
      if (reservation.customerPhone !== mobile) {
        Toast.show({
          text1: 'Lỗi đặt bàn',
          text2: 'Số điện thoại không trùng khớp, vui lòng thử lại!',
          type: 'error',
        })
        return
      }
    }

    await joinTableGroup(`${jsonValue?.storeId}-${jsonValue?.tableNumber}`)
    Toast.show({
      text1: 'Trạng thái đặt bàn',
      text2: 'Đặt bàn thành công',
      type: 'success',
    })
    router.navigate('(tabs)')
  }

  const onClose = () => {
    setScanned(false)
    setShowAlert(false)
    setStep(1)
    dispatch(
      updateReservation({
        reservationId: null,
        isChanged: false,
        capacity: 0,
        customerId: null,
        customerPhone: null,
        status: -1,
        storeId: null,
        tableNumber: -1,
      })
    )
  }

  return (
    <SafeAreaView style={StyleSheet.absoluteFillObject}>
      <Stack.Screen
        options={{
          headerLeft: () => (
            <Icons.MaterialCommunityIcons
              name="arrow-left"
              size={25}
              color="#1F2937"
              className="mr-10"
              onPress={() => {
                setShowAlert(false)
                setScanned(false)
                setStep(1)
                router.back()
              }}
            />
          ),
          title: t('scan'),
          headerRight: () => (
            <>
              <Icons.MaterialIcons
                name="flip-camera-android"
                size={25}
                color="#1F2937"
                onPress={toggleCameraFacing}
              />
            </>
          ),
        }}
      />

      {(isSuccess || getError) && (
        <HandleResponse
          isError={getError}
          isSuccess={isSuccess}
          error={resultError?.data?.message || 'Có lỗi xảy ra'}
          message={t('get-reservation-success')}
          onSuccess={() => {}}
        />
      )}

      <CameraView
        facing={facing}
        style={StyleSheet.absoluteFillObject}
        ref={cameraRef}
        onBarcodeScanned={scanned || isFirst ? undefined : handleBarCodeScanned}
      />
      <Overlay />

      {isFirst && (
        <Button className="absolute w-[96%] self-center bottom-2" onPress={() => setIsFirst(false)}>
          {t('start-scan')}
        </Button>
      )}

      {scanned && (
        <Button
          className="absolute w-[96%] self-center bottom-2"
          onPress={() => setScanned(false)}
          isLoading={getLoading}
          disabled={getLoading}
        >
          {t('scan-again')}
        </Button>
      )}

      <Dialog visible={showAlert} onDismiss={() => setShowAlert(false)} className="bg-white">
        {step === 1 && (
          <Step1
            status={reservation.status}
            isChanged={reservation.isChanged}
            handleClose={onClose}
            tableNumber={reservation.tableNumber}
            handleSubmit={() => setStep(2)}
          />
        )}
        {step === 2 && (
          <Step2 status={reservation.status} handleClose={() => setStep(1)} onSubmit={onSubmit} />
        )}
      </Dialog>
    </SafeAreaView>
  )
}
