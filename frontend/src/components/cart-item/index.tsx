import React from 'react';
import {
  Container,
  ImageContainer,
  Input,
  PriceContainer,
  RemoveText,
  Text,
  TextContainer,
} from './style';
import Image from 'next/image';
import { useStoreDispatch } from 'src/redux/store';
import { changeQuantity, removeItem } from 'src/redux/slices/cart-slice';
import Item from '@/type/Item';

type propsType = {
  item: Item;
  quantityOrder: number;
};

export default function CartItem({
  item: { picture, title, price, itemId },
  quantityOrder,
}: propsType) {
  const dispatch = useStoreDispatch();
  const handleClick = () => {
    dispatch(removeItem(itemId));
  };
  const handleInput = (event: React.ChangeEvent<HTMLInputElement>) => {
    let value = parseInt(event.target.value);
    if (isNaN(value)) {
      value = 0;
    }
    dispatch(changeQuantity({ id: itemId, quantity: value }));
  };
  return (
    <Container>
      <ImageContainer>
        <Image
          alt="temp"
          src={picture || process.env.NEXT_PUBLIC_DUMMY_PICTURE || ""}
          width="100"
          height="100"
          style={{ objectFit: "cover" }}
          fill
        />
      </ImageContainer>
      <TextContainer>
        <Input
          value={quantityOrder.toString()}
          type="number"
          onChange={handleInput}
        />
        <Text>x</Text>
        <Text>{title}</Text>
      </TextContainer>
      <PriceContainer>
        <Text $isAlternativeColor>
          ${Math.round(price * quantityOrder * 100) / 100}
        </Text>
        <RemoveText onClick={handleClick}>remove</RemoveText>
      </PriceContainer>
    </Container>
  );
}
