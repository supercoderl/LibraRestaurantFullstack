import { DiscountType } from "@/type/DiscountType"
import { Center, CenterCoupon, CenterSmall, CenterTitle, Counpon, Left, LeftItem, Right, RightItem } from "./style"
import React from "react";

type PromoCardProps = {
    item: DiscountType;
    selectedItem: number | null;
    handleSelect: (id: number) => void;
}

export const PromoCard: React.FC<PromoCardProps> = ({ item, selectedItem, handleSelect }) => {
    return (
        <Counpon $isActive={selectedItem === item.discountTypeId} onClick={() => handleSelect(item.discountTypeId)}>
            <Left>
                <LeftItem>{item.name}</LeftItem>
            </Left>
            <Center>
                <div>
                    <CenterTitle>Giảm {item.value.toString().replace(/000$/, 'k')} {item.isPercentage && ` %`}</CenterTitle>
                    <CenterCoupon>Phiếu giảm giá</CenterCoupon>
                    <CenterSmall>Giới hạn đến 10/20/2024</CenterSmall>
                </div>
            </Center>

            <Right>
                <RightItem>87871112</RightItem>
            </Right>

        </Counpon>
    )
}