import { checkTimeDifference, formatDate, generateOrderNo } from "@/utils/date";
import { BodyContainer, CartContainer, FluidContainer, CustomerCartText, HeaderContainer, HeaderText, HeaderTime, ImageDesktop, ImageItemContainer, ImageMobile, ItemContainer, ItemInfoContainer, ItemInfoMoreContainer, ItemInfoMoreText, ItemInfoPriceContainer, ItemInfoPriceDiscount, ItemInfoPriceText, ItemInfoPriceTotal, ItemInfoTextContainer, ItemInfoTitle, LeftContainer, Price, PriceCalculate, PriceCalculateContainer, PriceCalculateText, PriceCalculateTotal, PriceContainer, PriceTotal, PriceTotalNumber, PriceTotalText, RightContainer, ShippingText, Container, CenterContainer, Button, ButtonContainer, QuantityContainer, QuantityButton, ItemInfoPriceContainerMobile, PromoContainer, PromoInput, PromoButtonContainer, PromoButton, PromoSvg, DiscountText, HeaderWarning } from "./style";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import Header from "@/components/header";
import { useRouter } from "next/navigation";
import { DiscountStatus, OrderStatus } from "@/enums";
import React, { useContext, useEffect, useState } from "react";
import { actionOrder, order } from "@/api/business/orderApi";
import { Order } from "@/type/Order";
import { Spinner } from "@/components/loading/spinner";
import { Payment } from "./payment";
import AddIcon from '../../../public/assets/icons/add-icon.svg';
import CloseIcon from '../../../public/assets/icons/close-icon.svg';
import SubtractIcon from '../../../public/assets/icons/subtract-icon.svg';
import { ArrowEffect } from "@/components/arrows/effect";
import { changeQuantity, removeItem, submitOrder, updateItemsInCart } from "@/redux/slices/cart-slice";
import { toast } from "react-toastify";
import { v4 as uuid } from 'uuid';
import { get, set } from "@/utils/localStorage";
import { TFunction } from "i18next";
import { useSignalR } from "@/context/signalRProvider";
import { fetchDiscountTypeByCodeData, updateDiscount } from "@/redux/slices/discountTypes-slice";
import { calculateDiscountPrice, calculateTotal } from "@/utils/price";
import { mergeItemsAndOrderLines } from "@/utils/check";
import { OrderLine } from "@/type/OrderLine";
import { ThemeContext } from "@/theme/theme-provider";

export default function OrderContainer({ t }: { t: TFunction<"translation", undefined> }) {
    const router = useRouter();
    const [orderId, setOrderId] = useState<string | null>(null);
    const [loading, setLoading] = useState(true);
    const [show, setShow] = useState(false);
    const [orderS, setOrderS] = useState<Order | null>(null);
    const [count, setCount] = useState(0);
    const dispatch = useStoreDispatch();
    const { sendMessageToGroup } = useSignalR();
    const [discount, setDiscount] = useState("");
    const myTable = get("my-table");
    const [now, setNow] = useState(new Date());
    const themeContext = useContext(ThemeContext);

    const { itemsInCart, id, storeId, tableNumber, discountLoading, discountType, items, orderLoading, customerId } = useStoreSelector(
        state => ({
            itemsInCart: state.cart.itemsInCart,
            id: state.reservation.id,
            storeId: state.reservation.storeId,
            tableNumber: state.reservation.tableNumber,
            discountLoading: state.mainDiscountTypeSlice.loading,
            discountType: state.mainDiscountTypeSlice.discountType,
            items: state.mainProductSlice.items,
            orderLoading: state.cart.loading,
            customerId: state.reservation.customerId
        })
    );

    const handlePromoChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (itemsInCart.length <= 0) {
            toast(t("cannot-input-promo"), { type: "warning" });
            return;
        }

        setDiscount(e.target.value);

        if (e.target.value === "") {
            dispatch(updateDiscount(null));
        }
    }

    const onAcceptCode = () => {
        dispatch(fetchDiscountTypeByCodeData(discount));
    }

    const onCancel = () => {
        if (orderS) {
            if (checkTimeDifference(orderS?.latestStatusUpdate, 10))
                toast(t("food-ready"), { type: "warning" });
        }
        else {
            toast(t("order-not-exists"), { type: "error" });
        }
    }

    const onSubmit = async () => {
        if (!id) {
            toast(t("you-have-not-reservation"), { type: "warning" });
            return;
        }
        else if (!myTable) {
            toast(t("your-reservation-have-error"), { type: "warning" });
            return;
        }

        else if (!storeId) {
            toast(t("please-scan-again"), { type: "warning" });
            return;
        }

        let body: any = {
            orderNo: generateOrderNo(Number(tableNumber)),
            storeId,
            reservationId: Number(id),
            priceCalculated: calculateTotal(itemsInCart, discountType),
            subtotal: calculateTotal(itemsInCart, discountType),
            tax: 0,
            total: calculateTotal(itemsInCart, discountType),
            customerId,
            latestStatus: OrderStatus.InPreperation,
            latestStatusUpdate: new Date(),
            isPaid: false,
            isPreparationDelayed: false,
            isCanceled: false,
            isReady: false,
            isCompleted: false,
            orderLines: itemsInCart.map(e => ({
                orderId: uuid(),
                itemId: e.item.itemId,
                quantity: e.quantityOrder,
                isCanceled: false,
                foodPrice: e.item?.discount ? calculateDiscountPrice(
                    e.item.price,
                    e.item?.discount?.discountValue,
                    e.item?.discount?.isPercentage,
                    e.item.discountStatus
                ) : e.item.price
            }))
        };

        setCount(count + 1);
        if (orderId) {
            body.orderId = orderId;
            body.action = "update";
        }
        dispatch(submitOrder({ order: body, action: orderId ? "update" : "create" })).then(async (res) => {
            if (submitOrder.fulfilled.match(res)) {
                if (orderId) {

                }
                else {
                    set("orderId", res.payload.orderId);
                    setOrderId(res.payload.orderId);
                }
                await sendMessageToGroup(myTable, `Khách bàn số ${tableNumber} đã đặt món`, "order");
                toast(t("order-success"), { type: "success" })
            }
        });
    }

    //? Init
    useEffect(() => {
        const orderId = get("orderId");
        if (orderId && typeof orderId === "string") setOrderId(orderId);
    }, []);

    useEffect(() => {
        setInterval(() => {
            setNow(new Date());
        }, 1000);
    }, []);

    //? Focus On Mount
    useEffect(() => {
        const getOrder = async () => {
            setLoading(true);
            try {
                const res = await order(orderId || "");
                if (res && res.data) {
                    setOrderS(res.data);
                    if (itemsInCart.length === 0 && res.data?.orderLines && res.data?.orderLines.length > 0) {
                        const itemsAddToCart = res.data?.orderLines.map((x: OrderLine) => {
                            const item = items.find(i => i.itemId === x.itemId);
                            if (item) {
                                return {
                                    item,
                                    quantityOrder: x.quantity
                                };
                            }
                            return null;
                        }).filter(Boolean);
                        dispatch(updateItemsInCart(itemsAddToCart));
                    }
                }
            }
            catch (error) { console.log(error) }
            finally {
                setTimeout(() => setLoading(false), 2000);
            };
        }

        orderId ? getOrder() : setLoading(false);
    }, [count, orderId]);

    return (
        <Container>
            <CenterContainer>
                <Header t={t} />
                <FluidContainer>
                    <HeaderContainer>
                        <HeaderText>{t("my-cart")}: #13432</HeaderText>
                        <HeaderTime>{`${formatDate(new Date(), t)} - ${now.toLocaleString("en-US", { hour: "numeric", minute: "numeric", second: "2-digit", hour12: false })}`}</HeaderTime>
                        {!orderId && <HeaderWarning>{t("please-scan")}</HeaderWarning>}
                    </HeaderContainer>
                    <BodyContainer>
                        <LeftContainer>
                            <CartContainer>
                                <CustomerCartText>{t("cart")}</CustomerCartText>
                                {
                                    mergeItemsAndOrderLines(itemsInCart, orderS?.orderLines, items).map((e, index) => {
                                        return (
                                            <ItemContainer key={index}>
                                                <ImageItemContainer>
                                                    <ImageDesktop src={e.item?.picture || process.env.NEXT_PUBLIC_DUMMY_PICTURE} alt="item" />
                                                    <ImageMobile src={e.item?.picture || process.env.NEXT_PUBLIC_DUMMY_PICTURE} alt="item" />
                                                </ImageItemContainer>
                                                <ItemInfoContainer>
                                                    <ItemInfoTextContainer>
                                                        <ItemInfoTitle>{e.item?.title}</ItemInfoTitle>
                                                        <ItemInfoMoreContainer>
                                                            <ItemInfoMoreText><span>Style: </span> Italic Minimal Design</ItemInfoMoreText>
                                                            <ItemInfoMoreText><span>Size: </span> Small</ItemInfoMoreText>
                                                            <ItemInfoMoreText><span>Color: </span> Light Blue</ItemInfoMoreText>
                                                        </ItemInfoMoreContainer>
                                                    </ItemInfoTextContainer>
                                                    <ItemInfoPriceContainerMobile>
                                                        <ItemInfoPriceTotal $isStrike={false}>{
                                                            e.item?.discount &&
                                                            `${calculateDiscountPrice(
                                                                e.item.price * e.quantityOrder,
                                                                e.item?.discount?.discountValue,
                                                                e.item?.discount?.isPercentage,
                                                                e.item.discountStatus
                                                            )} ₫`}</ItemInfoPriceTotal>
                                                        <ItemInfoPriceTotal $isStrike={e.item.discountStatus === DiscountStatus.Active}>{e.item.price * e.quantityOrder} ₫</ItemInfoPriceTotal>
                                                        <QuantityContainer>
                                                            <QuantityButton
                                                                $isDisabled={false}
                                                                onClick={() => dispatch(changeQuantity({ id: e.item.itemId, quantity: e.quantityOrder + 1 }))}>
                                                                <AddIcon width={16} fill="white" />
                                                            </QuantityButton>
                                                            <ItemInfoPriceText>{e.quantityOrder}</ItemInfoPriceText>
                                                            <QuantityButton
                                                                $isDisabled={!e.hasMore || loading}
                                                                disabled={!e.hasMore || loading}
                                                                onClick={() => {
                                                                    if (e.quantityOrder === 1) {
                                                                        dispatch(removeItem(e.item.itemId))
                                                                    }
                                                                    else {
                                                                        dispatch(changeQuantity({ id: e.item.itemId, quantity: e.quantityOrder - 1 }))
                                                                    }
                                                                }}
                                                            >
                                                                {
                                                                    loading ?
                                                                        <Spinner width={16} color="white" />
                                                                        :
                                                                        e.quantityOrder === 1 ?
                                                                            <CloseIcon width={16} height={16} fill="white" />
                                                                            :
                                                                            <SubtractIcon width={16} fill="white" />
                                                                }
                                                            </QuantityButton>
                                                        </QuantityContainer>
                                                    </ItemInfoPriceContainerMobile>
                                                </ItemInfoContainer>
                                            </ItemContainer>
                                        )
                                    })
                                }
                            </CartContainer>

                        </LeftContainer>
                        <RightContainer>
                            <PriceContainer>
                                <Price>
                                    <ShippingText>{t("summary-of-invoice")}</ShippingText>
                                    <PriceCalculate>
                                        <PriceCalculateContainer>
                                            <PriceCalculateText>{t("subtotal")}</PriceCalculateText>
                                            <PriceCalculateTotal>{calculateTotal(itemsInCart, discountType)} ₫</PriceCalculateTotal>
                                        </PriceCalculateContainer>
                                        <PriceCalculateContainer>
                                            <PriceCalculateText>{t("sale")}</PriceCalculateText>
                                            <PriceCalculateTotal>{discountType ? discountType.isPercentage ? `${discountType.value}%` : `${discountType.value} ₫` : `0 ₫`}</PriceCalculateTotal>
                                        </PriceCalculateContainer>
                                        <PriceCalculateContainer>
                                            <PriceCalculateText>{t("tax")}</PriceCalculateText>
                                            <PriceCalculateTotal>0%</PriceCalculateTotal>
                                        </PriceCalculateContainer>
                                        {discountType && <DiscountText>{t("applied-promo")} {discountType.counponCode}</DiscountText>}
                                        <PromoContainer>
                                            <PromoInput
                                                type="text"
                                                placeholder={t("input-discount")}
                                                onChange={handlePromoChange}
                                                value={discount}
                                            />
                                            <PromoButtonContainer>
                                                <PromoButton
                                                    type="submit"
                                                    aria-label="Submit"
                                                    onClick={onAcceptCode}
                                                >
                                                    {discountLoading ?
                                                        <Spinner width={20} color={themeContext.theme === "dark" ? "black" : "white"} />
                                                        :
                                                        <PromoSvg viewBox="0 0 16 6" aria-hidden="true">
                                                            <path
                                                                fill={themeContext.theme === "dark" ? "black" : "white"}
                                                                fillRule="evenodd"
                                                                clipRule="evenodd"
                                                                d="M16 3 10 .5v2H0v1h10v2L16 3Z"
                                                            ></path>
                                                        </PromoSvg>}
                                                </PromoButton>
                                            </PromoButtonContainer>
                                        </PromoContainer>
                                    </PriceCalculate>
                                    <PriceTotal>
                                        <PriceTotalText>{t("total")}</PriceTotalText>
                                        <PriceTotalNumber>{calculateTotal(itemsInCart, discountType)} ₫</PriceTotalNumber>
                                    </PriceTotal>
                                    <ButtonContainer>
                                        <Button onClick={onCancel}>{t("cancel")}</Button>
                                        <Button onClick={onSubmit} className="group">
                                            {
                                                orderLoading && <Spinner width={15} />
                                            }
                                            {t("order")}
                                        </Button>
                                    </ButtonContainer>

                                    <ArrowEffect t={t} />
                                </Price>

                                <ButtonContainer>
                                    <Button onClick={() => {
                                        orderId ? setShow(true) : toast(t("please-call-order"), { type: "warning" });
                                    }} className="group">
                                        {t("pay")}
                                    </Button>
                                </ButtonContainer>
                            </PriceContainer>
                        </RightContainer>
                    </BodyContainer>
                </FluidContainer>
            </CenterContainer>

            <Payment
                show={show}
                setShow={setShow}
                router={router}
                order={orderS}
                amount={calculateTotal(itemsInCart, discountType)}
                dispatch={dispatch}
                tableCode={myTable}
                sendMessageFunction={sendMessageToGroup}
                tableNumber={tableNumber}
            />
        </Container>
    )
}