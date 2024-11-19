import React from 'react';
import type { NextPage } from 'next';
import { serverSideTranslations } from 'next-i18next/serverSideTranslations';
import { useTranslation } from 'react-i18next';
import FoodDetailContainer from '@/containers/detail-container';
import { useRouter } from 'next/router';

export async function getServerSideProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const FoodDetail: NextPage = () => {
    const router = useRouter();
    let { slug } = router.query;

    if (Array.isArray(slug)) {
        slug = slug[0]; // Use the first element if it's an array
    }

    const { t } = useTranslation();

    return <FoodDetailContainer t={t} slug={slug} router={router} />;
};

export default FoodDetail;