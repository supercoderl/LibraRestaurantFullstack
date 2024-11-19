import React, { useEffect, useState } from 'react';
import Image from 'next/image';
import {
  Container,
  DetailContainer,
  ImageContainer,
  PlusContainer,
  RowContainer,
  StrikeText,
  StyledStarIcon,
  Text,
  TimeContainer,
  TimeText,
  Title,
} from './style';
import { useStoreDispatch, useStoreSelector } from 'src/redux/store';
import { addItem } from 'src/redux/slices/cart-slice';
import { theme } from 'twin.macro';
import PlusIcon from 'public/assets/icons/plus-icon.svg';
import Item from '@/type/Item';
import { toast } from 'react-toastify';
import { useTranslation } from "next-i18next";
import { calculateDiscountPrice } from '@/utils/price';
import { DiscountStatus } from '@/enums';

export default function FoodItem(item: Item) {
  const { picture, title, price, discount } = item;
  const dispatch = useStoreDispatch();
  const id = useStoreSelector(state => state.reservation.id);
  const { t } = useTranslation();

  const handleClick = () => {
    if (!id) {
      toast(t("you-have-not-reservation"), { type: "warning" })
    }
    else {
      dispatch(addItem({ item }));
    }
  };

  return (
    <Container onClick={handleClick} className='group'>
      <PlusContainer>
        <PlusIcon fill="black" />
        <Title>{t("add-to-cart")}</Title>
      </PlusContainer>
      <ImageContainer>
        <Image
          src={picture || process.env.NEXT_PUBLIC_DUMMY_PICTURE || ""}
          alt={`${title} image`}
          style={{ objectFit: "contain" }}
          fill
          sizes="100vw"
        />
        <TimeContainer>
          <TimeText>
            <b>26-30</b> {t("minutes")}
          </TimeText>
        </TimeContainer>
      </ImageContainer>
      <DetailContainer>
        <RowContainer>
          <Title>{title}</Title>
        </RowContainer>
        <RowContainer>
          <StyledStarIcon
            fill={theme`textColor.primary`}
            height="15"
            tw="mr-1.5"
          />
          <Text $isAlternativeColor>{item.sku}</Text>
          <Text $isAlternativeColor $isEnd>
            {calculateDiscountPrice(price, discount?.discountValue, discount?.isPercentage, item.discountStatus) && `${calculateDiscountPrice(price, discount?.discountValue, discount?.isPercentage, item.discountStatus)} ₫`} &nbsp;
            <StrikeText $isStrike={discount !== null && discount !== undefined && item.discountStatus === DiscountStatus.Active}>{price}</StrikeText> ₫
          </Text>
        </RowContainer>
      </DetailContainer>
    </Container>
  );
}
