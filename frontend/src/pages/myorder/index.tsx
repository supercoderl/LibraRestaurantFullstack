import OrderContainer from "@/containers/order-container";
import { NextPage } from "next";
import { useTranslation } from "next-i18next";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";

export async function getStaticProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const Order: NextPage = () => {
    const { t } = useTranslation();

    return <OrderContainer t={t} />
}

export default Order;