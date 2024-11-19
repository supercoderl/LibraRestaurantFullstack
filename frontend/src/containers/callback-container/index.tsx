import { BillContainer, Card, Container, ContentContainer, Curved, FlexContainer, HomeButton, Icon, IconContainer, ItemContainer, Logo, LogoImg, LogoRed, StatusText, Subtitle, TextFooter, TextItem, TextItemContainer, Title } from "./style";
import logo from "../../../public/assets/images/logo/logo-removebg-preview.png";
import { Loading } from "@/components/loading";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { useEffect, useState } from "react";
import { useSearchParams } from "next/navigation";
import { PaymentMethod, PaymentStatus } from "@/enums";
import { useRouter } from "next/router";
import { checkStripe, checkVNPay } from "@/utils/payment";
import { updatePayment } from "@/api/business/paymentHistoryApi";
import { toast } from "react-toastify";
import HandIcon from "../../../public/assets/icons/hand-icon.svg";
import SadIcon from "../../../public/assets/icons/sad-icon.svg";
import { v4 as uuid } from "uuid";
import { useStoreDispatch } from "@/redux/store";
import { clearCart } from "@/redux/slices/cart-slice";
import { clearReservation } from "@/redux/slices/reservation-slice";
import { useTranslation } from "next-i18next";
import { remove } from "@/utils/localStorage";

export default function Cart() {
    const { width } = useWindowDimensions();
    const [loading, setLoading] = useState(true);
    const [statusType, setStatusType] = useState(PaymentStatus.Success);
    const searchParams = useSearchParams();
    const router = useRouter();
    const dispatch = useStoreDispatch();
    const { t } = useTranslation();

    const onCheckStatus = async () => {
        let object: any = {};

        if (checkVNPay(searchParams.entries())) {
            object = {
                amount: searchParams.get("vnp_Amount"), //100.000 VND
                status: searchParams.get("vnp_TransactionStatus") == "00" ? PaymentStatus.Success : PaymentStatus.Fail, //00 - success;
                transactionId: searchParams.get("vnp_TxnRef"),
                responseJSON: router.asPath.split('?')[1],
                callbackURL: process.env.NEXT_PUBLIC_BASE_URL + router.asPath,
                orderId: searchParams.get("vnp_OrderInfo")?.split('_')[1],
                paymentMethodId: PaymentMethod.VNPay
            }
        }

        else if (checkStripe(searchParams.entries())) {
            object = {
                amount: Number(searchParams.get("stripe_amount")) / 100, //10 USD
                status: searchParams.get("stripe_status") == "00" ? PaymentStatus.Success : PaymentStatus.Fail, //00 - success;
                transactionId: searchParams.get("stripe_transaction"),
                responseJSON: router.asPath.split('?')[1],
                callbackURL: process.env.NEXT_PUBLIC_BASE_URL + router.asPath,
                orderId: searchParams.get("stripe_order"),
                paymentMethodId: PaymentMethod.Stripe
            }
        }

        setStatusType(object?.status);

        try {
            if (object?.status === PaymentStatus.Success) {
                await updatePayment(object);
                remove("orderId");
                dispatch(clearCart());
                dispatch(clearReservation());
            }
        }
        catch (error) {
            console.log("Update payment: ", error);
            toast("Có lỗi xảy ra!", { type: "error" });
        }
        finally {
            setTimeout(() => setLoading(false), 600);
        }
    }

    useEffect(() => {
        onCheckStatus();
    }, [searchParams]);

    return (
        <Container>
            {
                !loading ?
                    <Card>
                        <Curved />
                        <Logo>
                            Libra
                            <LogoRed> Restaurant</LogoRed>
                            <LogoImg src={logo.src} alt="" />
                        </Logo>
                        <IconContainer>
                            <Icon
                                loading="lazy"
                                src={statusType === PaymentStatus.Success ?
                                    "https://icons.veryicon.com/png/o/miscellaneous/8atour/success-35.png"
                                    :
                                    "https://icons.veryicon.com/png/o/miscellaneous/ds/38-operation-failed.png"
                                }
                                alt=""
                            />
                        </IconContainer>

                        <ContentContainer>
                            <StatusText>{statusType === PaymentStatus.Success ? t("paid") : t("paid-failed")}</StatusText>

                            <BillContainer className="receipt">
                                <FlexContainer>
                                    <Title>{statusType === PaymentStatus.Success ? t("thanks") : t("sorry")}</Title>
                                    {statusType === PaymentStatus.Success ? <HandIcon fill="none" width="6%"></HandIcon> : <SadIcon width="6%"></SadIcon>}
                                </FlexContainer>
                                {
                                    statusType === PaymentStatus.Fail && <Subtitle>{t("pay-again")}</Subtitle>
                                }
                                <ItemContainer>
                                    <TextItemContainer>
                                        <TextItem>{t("transaction-store")}</TextItem>
                                        <TextItem>{t("libra-restaurant")}</TextItem>
                                    </TextItemContainer>
                                    <TextItemContainer>
                                        <TextItem>{t("transactionId")}</TextItem>
                                        <TextItem>{uuid().substring(0, 8)}</TextItem>
                                    </TextItemContainer>
                                    <TextItemContainer>
                                        <TextItem>{t("orderId")}</TextItem>
                                        <TextItem>{uuid().substring(0, 8)}</TextItem>
                                    </TextItemContainer>
                                    <TextItemContainer>
                                        <TextItem>{t("payment-method")}</TextItem>
                                        <TextItem>
                                            {
                                                checkVNPay(searchParams.entries()) ? "VNPay" :
                                                    checkStripe(searchParams.entries()) ? "Stripe" : null
                                            }
                                        </TextItem>
                                    </TextItemContainer>
                                    <TextItemContainer>
                                        <TextItem>{t("date")}</TextItem>
                                        <TextItem>10. 10. 2024 / 14:26:42</TextItem>
                                    </TextItemContainer>
                                    <TextItemContainer>
                                        <TextItem isBold>{t("total")}</TextItem>
                                        <TextItem isBold>100,000 đ</TextItem>
                                    </TextItemContainer>
                                </ItemContainer>
                                <HomeButton
                                    onClick={() => router.replace("/")}
                                >
                                    {t("back-home")}
                                </HomeButton>
                            </BillContainer>
                        </ContentContainer>
                        <TextFooter>{t("gPay-support")}</TextFooter>
                    </Card>
                    :
                    <Loading width={width > 767 ? "10%" : "40%"} />
            }
        </Container>
    )
}