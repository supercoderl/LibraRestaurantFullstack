import { useState, useEffect } from 'react'
import { View } from 'react-native'

import CartButtons from './CartButtons'
import { Button } from '../common/Buttons'
import ProductPrice from '../product/ProductPrice'

import { useUserInfo } from '@/hooks'
import { useTranslation } from 'react-i18next'

const AddToCartOperation = props => {
  //? Assets
  const { t } = useTranslation();

  //? Props
  const { product, handleAddItem } = props

  //? Get User Data
  const { userInfo, mustAuthAction } = useUserInfo()

  //? States
  const [currentItemInCart, setCurrentItemInCart] = useState(undefined)

  //? Re-Renders
  useEffect(() => {
    // const item = exsitItem(cartItems, product._id, tempColor, tempSize)
    // setCurrentItemInCart(item)
  }, [])



  //? Render(s)
  return (
    <View className="flex flex-row items-center justify-between p-3 bg-white border-t border-gray-300 px-5 shadow-3xl ">
      {currentItemInCart ? (
        <View className="flex gap-x-4">
          <View className="w-44">
            <CartButtons item={currentItemInCart} />
          </View>
        </View>
      ) : (
        <Button onPress={handleAddItem} className="px-12 text-sm btn">
          {t('add-to-cart')}
        </Button>
      )}

      <View className="min-w-fit">
        <ProductPrice
          inStock={product?.quantity}
          discount={product?.discount || 0}
          price={product?.price}
          singleProduct
        />
      </View>
    </View>
  )
}

export default AddToCartOperation
