import React from 'react';
import type { NextPage } from 'next';
import ReservationContainer from 'src/containers/reservation-container';
import { useSearchParams } from 'next/navigation';
import { serverSideTranslations } from 'next-i18next/serverSideTranslations';
import { useTranslation } from 'react-i18next';

export async function getServerSideProps({ locale }: { locale: string }) {
  return {
    props: {
      ...(await serverSideTranslations(locale, ['common'])),
    },
  };
}

const Reservation: NextPage = () => {
  const searchParams = useSearchParams();
  const { t } = useTranslation();

  return <ReservationContainer t={t} reservationId={searchParams.get("reservationId")} tableNumber={searchParams.get("tableNumber")} storeId={searchParams.get("storeId")} />;
};

export default Reservation;