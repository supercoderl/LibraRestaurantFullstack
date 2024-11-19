
import CallbackContainer from "@/containers/callback-container";
import { NextPage } from "next";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";

export async function getStaticProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const Callback: NextPage = () => {
    return <CallbackContainer />
};

export default Callback;