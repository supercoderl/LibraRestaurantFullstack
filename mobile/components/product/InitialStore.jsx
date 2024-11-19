import { useEffect } from 'react'

import { useAppDispatch } from '@/hooks'
import { setTempColor, setTempSize, addToLastSeen } from '@/store'

const InitialStore = props => {
  const { product } = props

  const dispatch = useAppDispatch()

  useEffect(() => {
    // if (product.colors.length > 0) {
    //   dispatch(setTempColor(product?.colors[0]))
    //   dispatch(setTempSize(null))
    // } else if (product.sizes.length > 0) {
    //   dispatch(setTempSize(product?.sizes[0]))
    //   dispatch(setTempColor(null))
    // } else {
    //   dispatch(setTempColor(null))
    //   dispatch(setTempSize(null))
    // }
  }, [])
  useEffect(() => {
    dispatch(
      addToLastSeen({
        productID: product?.itemId,
        image: product?.picture,
        title: product?.title,
      })
    )
  }, [product?.itemId])
  return null
}

export default InitialStore
