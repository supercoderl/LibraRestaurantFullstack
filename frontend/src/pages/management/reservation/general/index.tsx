import { reservations } from "@/api/business/reservationApi";
import { ReservationContainer } from "@/containers/dashboard-container/reservations";
import { ListRep } from "@/type/objectTypes";
import { NextPage } from "next";
import { useEffect, useState } from "react";
import { useTranslation } from "next-i18next";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";

export async function getStaticProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const Reservation: NextPage = () => {
    const { t } = useTranslation();
    const [result, setResult] = useState<ListRep | null | undefined>(null);
    const [loading, setLoading] = useState(true);
    const [index, setIndex] = useState(1);
    const [size, setSize] = useState(10);
    const [searchText, setSearchText] = useState<string | null>(null);

    const onLoad = async () => {
        setLoading(true);
        try {
            const res = await reservations({ page: index, pageSize: size, searchTerm: searchText });
            if (res && res.success) {
                setResult(res.data);
            }
            console.log(res);
        }
        catch (error) {
            console.log("OnLoad Reservation: ", error);
        }
        finally {
            setTimeout(() => {
                setLoading(false);
            }, 300);
        }
    }

    useEffect(() => { onLoad() }, [index, size, searchText]);

    return <ReservationContainer
        t={t}
        result={result}
        loading={loading}
        onReload={onLoad}
        onPaginationChange={(index, size) => {
            setIndex(index);
            setSize(size);
        }}
        onSearch={(text) => {
            if (text === "") setSearchText(null);
            else setSearchText(text);
        }}
    />
}

export default Reservation;