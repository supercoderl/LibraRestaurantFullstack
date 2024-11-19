import { useStoreSelector } from "@/redux/store";
import { Component, ComponentInfo, Container, NormalPrice, PriceContainer, Title } from "./style"
import { TFunction } from "i18next";
import React from "react";

type OrderPriceProps = {
    t: TFunction<"translation", undefined>;
}

export const OrderPrice: React.FC<OrderPriceProps> = ({t}) => {
    const { itemsInCart } = useStoreSelector(
        state => ({
            itemsInCart: state.cart.itemsInCart
        }),
    );

    const calculatePriceItems = () => {
        if(itemsInCart && itemsInCart.length > 0)
        {
            const totalPrice = itemsInCart.reduce((total, e) => {
                return total + (e.item.price * e.quantityOrder);
            }, 0);
            return totalPrice;
        }
        return 0;
    }

    return (
        <Container>
            <Component>
                <ComponentInfo>
                    <Title>{t("expenses-paid")}</Title>

                    <PriceContainer>
                        <NormalPrice>{t("calculated")}: &nbsp; <b>{calculatePriceItems()} ₫</b></NormalPrice>
                    </PriceContainer>
                    <PriceContainer>
                        <NormalPrice>{t("tax")}: &nbsp; <b>0%</b></NormalPrice>
                    </PriceContainer>
                    <PriceContainer>
                        <NormalPrice>{t("total-revenue")}: &nbsp; <b>{calculatePriceItems() + calculatePriceItems() * 10 / 100} ₫</b></NormalPrice>
                    </PriceContainer>
                </ComponentInfo>
            </Component>
        </Container>
    )
}