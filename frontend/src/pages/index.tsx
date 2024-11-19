import React from 'react';
import type { NextPage } from 'next';
import HomeContainer from 'src/containers/home-container';
import { useTranslation } from 'react-i18next';
import { serverSideTranslations } from 'next-i18next/serverSideTranslations';

export async function getStaticProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ['common'])),
    },
  };
}

const Home: NextPage = () => {
  const { t } = useTranslation('common');
  return <HomeContainer t={t} />;
};

export default Home;
