import MenuContainer from "@/containers/menu-container";
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

const Menu: NextPage = () => {
    const { t } = useTranslation();

    return <MenuContainer t={t} />
}

export default Menu;