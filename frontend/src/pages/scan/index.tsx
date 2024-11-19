
import { NextPage } from "next";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";
import QRScanContainer from "src/containers/qr-scan-container";

export async function getStaticProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const QR: NextPage = () => {
    return <QRScanContainer />
};

export default QR;