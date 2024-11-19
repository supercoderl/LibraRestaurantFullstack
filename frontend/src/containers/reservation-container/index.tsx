import Header from "@/components/header";
import { CenterContainer, BodyContainer, Container, ContentContainer } from "./style";
import { Food } from "../home-container/food";
import { useStoreSelector } from "src/redux/store";
import { shallowEqual } from "react-redux";
import { Hero } from "@/components/hero";
import { SecondCategory } from "@/components/food-category/second-category";
import { useEffect } from "react";
import { toast } from "react-toastify";
import { TFunction } from "i18next";

type ReservationContainerProps = {
    tableNumber?: string | null;
    storeId?: string | null;
    reservationId?: string | null;
    t: TFunction<"translation", undefined>
}

export default function ReservationContainer({ tableNumber, storeId, reservationId, t }: ReservationContainerProps) {
    const { items } = useStoreSelector(
        state => ({
            items: state.mainProductSlice.items
        }),
        shallowEqual,
    );

    useEffect(() => {
        if (!tableNumber || tableNumber === null || !storeId || storeId === null || !reservationId || reservationId === null) {
            toast(t("reservation-not-exists"), {
                type: "error"
            });
            setTimeout(() => window.history.back(), 3000);
        }
    }, []);

    return (
        <>
            <Container>
                <BodyContainer>
                    <CenterContainer>
                        <Header t={t} />
                        <Hero title={t("food-store")} />
                        <SecondCategory />
                        <ContentContainer>
                            <Food
                                loading={false}
                                showTitle={false}
                                currentCategory={1}
                                items={items}
                                isReservation
                                t={t}
                            />
                            {/* {
                                orderId &&
                                <OrderContainer>
                                    <OrderPrice />
                                    <Order
                                        orderId={orderId}
                                        tableNumber={Number(tableNumber) || 0}
                                        storeId={storeId}
                                        reservationId={Number(reservationId) || 0}
                                    />
                                </OrderContainer>
                            } */}
                        </ContentContainer>
                    </CenterContainer>
                </BodyContainer>
            </Container>
        </>
    );
}
