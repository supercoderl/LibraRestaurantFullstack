import { ButtonCheckout, Component, ComponentInfo, Container, EmptyContainer, EmptyText, Image, Plan, PlanAction, PlanContent, PlanContentName, PlanContentPrice, PlanContentQuantity, PlanImage, SvgCheckout, Title } from "./style";
import TrashIcon from '../../../public/assets/icons/trash-icon.svg';
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { shallowEqual } from "react-redux";
import CartOrderIcon from "../../../public/assets/icons/cart-order-icon.svg";
import React, { useState } from "react";
import { removeItem } from "@/redux/slices/cart-slice";
import { useRouter } from "next/navigation";
import { CustomerLikeStatus } from "@/enums";
import { toast } from "react-toastify";
import { actionListOrderLine } from "@/api/business/orderLineApi";
import { OrderLine } from "@/type/OrderLine";
import { TFunction } from "i18next";

type OrderProps = {
    tableNumber: number;
    orderId: string;
    storeId?: string | null;
    reservationId: number;
    t: TFunction<"translation", undefined>;
}

export const Order: React.FC<OrderProps> = ({ tableNumber, orderId, storeId, reservationId, t }) => {
    const { itemsInCart } = useStoreSelector(
        state => ({
            itemsInCart: state.cart.itemsInCart
        }),
        shallowEqual,
    );
    const [loading, setLoading] = useState(false);
    const dispatch = useStoreDispatch();
    const router = useRouter();

    const handleRemove = (itemId: number) => {
        dispatch(removeItem(itemId));
    }

    const handleSubmit = async () => {
        setLoading(true);
        const body = itemsInCart.map((e) => ({
            orderId,
            itemId: e.item.itemId,
            quantity: e.quantityOrder,
            isCanceled: false,
            customerLike: CustomerLikeStatus.NotSet
        }));
        try {
            const res = await actionListOrderLine(body as OrderLine[], "create");
            if (res?.success) {
                router.push(`/myorder?tableNumber=${tableNumber}&reservationId=${reservationId}&storeId=${storeId}`);
            }
        }
        catch (error) {
            console.log("Create order line: ", error);
        }
        finally {
            setTimeout(() => setLoading(false), 600);
        }
    }

    return (
        <Container>
            <Component>
                <ComponentInfo>
                    <Title>{t("my-cart")}</Title>
                    {
                        itemsInCart && itemsInCart.length > 0 ?
                            itemsInCart.map((e, index) => (
                                <Plan title={e.item.title} key={index}>
                                    <PlanImage>
                                        <Image src={e.item?.picture || process.env.NEXT_PUBLIC_DUMMY_PICTURE} alt="item picture" />
                                    </PlanImage>
                                    <PlanContent>
                                        <PlanContentName>{e.item.title}</PlanContentName>
                                        {/* <PlanContentPrice>{e.item.price}VND</PlanContentPrice> */}
                                        <PlanContentQuantity>{t("quantity")}: {e.quantityOrder}</PlanContentQuantity>
                                    </PlanContent>
                                    <PlanAction onClick={() => handleRemove(e.item.itemId)}>
                                        <TrashIcon width={18} height={18} />
                                    </PlanAction>
                                </Plan>
                            ))
                            :
                            (
                                <EmptyContainer>
                                    <CartOrderIcon fill='#29223d' />
                                    <EmptyText>{t("order-empty")}</EmptyText>
                                </EmptyContainer>
                            )
                    }
                    {
                        itemsInCart && itemsInCart.length > 0 && (
                            <ButtonCheckout className="group" onClick={handleSubmit}>
                                <SvgCheckout loading={loading} fill="white" version="1.1" id="Layer_1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 512 512" xmlSpace="preserve">
                                    <g>
                                        <g>
                                            <path d="M483.871,301.511c3.569-16.755,5.374-33.897,5.374-51.2c0-65.341-25.445-126.771-71.648-172.974
                        S309.964,5.689,244.622,5.689S117.851,31.134,71.648,77.337S0,184.97,0,250.311s25.445,126.771,71.648,172.974
                        c46.203,46.203,107.633,71.648,172.974,71.648c13.379,0,26.704-1.108,39.822-3.255v14.633h170.667v-73.956h-0.094
                        C489.594,406.387,512,365.05,512,318.578v-17.067H483.871z M284.444,432.356v24.669c-13.064,2.497-26.409,3.775-39.822,3.775
                        c-116.064,0-210.489-94.425-210.489-210.489S128.559,39.822,244.622,39.822c116.065,0,210.489,94.425,210.489,210.489
                        c0,17.368-2.108,34.537-6.27,51.2H413.43c5.005-16.484,7.548-33.617,7.548-51.2c0-97.242-79.112-176.356-176.356-176.356
                        c-97.242,0-176.356,79.113-176.356,176.356s79.113,176.356,176.356,176.356c10.053,0,20.084-0.857,29.936-2.541
                        c3.201,2.89,6.529,5.637,9.981,8.23H284.444z M248.307,392.482c-1.228,0.032-2.455,0.051-3.685,0.051
                        c-78.421,0-142.222-63.801-142.222-142.222s63.801-142.222,142.222-142.222s142.222,63.801,142.222,142.222
                        c0,17.752-3.197,34.925-9.496,51.2H227.556v17.067C227.556,345.625,235.147,370.932,248.307,392.482z M420.978,472.178h-102.4
                        v-20.917c15.896,6.154,33.159,9.539,51.2,9.539s35.304-3.385,51.2-9.539V472.178z M369.778,426.667
                        c-53.794,0-98.537-39.501-106.743-91.022h213.487C468.314,387.165,423.572,426.667,369.778,426.667z"/>
                                        </g>
                                    </g>
                                </SvgCheckout>
                                {t("order")}
                            </ButtonCheckout>
                        )
                    }
                </ComponentInfo>
            </Component>
        </Container>
    )
}