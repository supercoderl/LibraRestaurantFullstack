import DashboardContainer from '@/containers/dashboard-container/analytic';
import { useTranslation } from 'next-i18next';
import { serverSideTranslations } from 'next-i18next/serverSideTranslations';
import React from 'react'

export async function getStaticProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ['common'])),
    },
  };
}

const Dashboard = () => {
  const { t } = useTranslation();

  return <DashboardContainer t={t} />
}

export default Dashboard;