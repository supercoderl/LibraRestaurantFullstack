import React from 'react';
import type { NextPage } from 'next';
import FoodContainer from '@/containers/food-container';
import { useTranslation } from 'react-i18next';
import { serverSideTranslations } from 'next-i18next/serverSideTranslations';

export async function getStaticProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ['common'])),
    },
  };
}

const Food: NextPage = () => {
  const { t } = useTranslation();

  return <FoodContainer t={t} />;
};

export default Food;