import { NativeEventEmitter } from 'react-native'
import VNPMerchant, { VnpayMerchantModule } from 'react-native-vnpay-merchant'

const eventEmitter = new NativeEventEmitter(VnpayMerchantModule)

export const VNPayService = url => {
  eventEmitter.addListener('PaymentBack', e => {
    if (e) {
      console.log('e.resultCode = ' + e.resultCode)
      switch (e.resultCode) {
      }

      eventEmitter.removeAllListeners('PaymentBack')
    }

    VNPMerchant.show({
      iconBackName: 'ic_back',
      paymentUrl: url,
      scheme: 'asdasd',
      tmn_code: 'TEDTW1FI',
      title: 'payment',
    })
  })
}
