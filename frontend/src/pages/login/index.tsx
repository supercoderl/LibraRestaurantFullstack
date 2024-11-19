import { LoginContainer } from "@/containers/login-container";
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

const Login: NextPage = () => {
    const { t } = useTranslation();

    return (
        <LoginContainer t={t} />
    )
}

export default Login;