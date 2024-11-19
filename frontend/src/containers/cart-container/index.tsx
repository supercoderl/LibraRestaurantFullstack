import React from 'react';
import CartItem from '@/components/cart-item';
import CheckoutButton from '@/components/checkout-button';
import Divider from '@/components/divider';

import {
  BodyContainer,
  BottomContainer,
  CloseButton,
  Container,
  HeaderContainer,
  HeaderItemsGroup,
  ItemCount,
  Text,
  TotalContainer,
} from './style';
import { useStoreDispatch, useStoreSelector } from 'src/redux/store';
import { clearCart, closeCart } from 'src/redux/slices/cart-slice';
import useCartProducts from 'src/hooks/use-cart-products';
import { shallowEqual } from 'react-redux';

import Swal from 'sweetalert2';
import { theme } from 'twin.macro';

export default function Cart() {
  const totalPrice = useCartProducts();
  const dispatch = useStoreDispatch();
  const handleClose = () => {
    dispatch(closeCart());
  };
  const { itemsInCart } = useStoreSelector(
    state => ({
      itemsInCart: state.cart.itemsInCart,
    }),
    shallowEqual,
  );

  const handleCheckoutButton = () => {
    if (!itemsInCart.length) return;
    dispatch(clearCart());
    Swal.fire({
      customClass: {
        title: 'text-primary',
        htmlContainer: 'text-primary',
        confirmButton: 'bg-purple',
      },
      title: 'Payment Completed',
      text: 'Enjoy your food.',
      background: theme`backgroundColor.secondary`,
    });
  };
  return (
    <Container>
      <BodyContainer>
        <HeaderContainer>
          <CloseButton onClick={handleClose}>X</CloseButton>
          <HeaderItemsGroup>
            <ItemCount>{itemsInCart.length}</ItemCount>
          </HeaderItemsGroup>
        </HeaderContainer>
        <Text>
          <b>My😎</b>
        </Text>
        <Text>
          <b>Order</b>
        </Text>
        {itemsInCart.map((element: any) => (
          <CartItem {...element} key={element.item.itemId}></CartItem>
        ))}
        <TotalContainer>
          <Text>Total:</Text>
          <Text>
            <b>${Math.round(totalPrice * 100) / 100}</b>
          </Text>
        </TotalContainer>
        <Divider />
        <BottomContainer>
          <CheckoutButton onClick={handleCheckoutButton}>
            Checkout {'-->'}
          </CheckoutButton>
        </BottomContainer>
      </BodyContainer>
    </Container>
  );
}
