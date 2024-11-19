import { useEffect, useState } from 'react'
import { useStoreSelector } from 'src/redux/store';
import {shallowEqual} from 'react-redux';

export default function useCartProducts() {
  const [totalPrice, setTotalPrice] = useState(0)

  const {productsInCart} = useStoreSelector(
    state => ({
      productsInCart: state.cart.itemsInCart,
    }),
    shallowEqual,
  );
  useEffect(() => {
    const tempTotalPrice = productsInCart.reduce((a: any,b: any)=> {
      return a + b.product.price * b.quantity
      }  , 0)
    setTotalPrice(tempTotalPrice)
   
  }, [ productsInCart])

  return  totalPrice
}
