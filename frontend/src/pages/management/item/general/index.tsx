import { items } from "@/api/business/itemApi";
import { ItemContainer } from "@/containers/dashboard-container/items";
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

const MenuItem: NextPage = () => {
    const { t } = useTranslation();
    const [result, setResult] = useState<ListRep | null | undefined>(null);
    const [loading, setLoading] = useState(true);
    const [index, setIndex] = useState(1);
    const [size, setSize] = useState(10);
    const [searchText, setSearchText] = useState<string | null>(null);

    const onLoad = async () => {
        setLoading(true);
        try {
            const res = await items({ page: index, pageSize: size, searchTerm: searchText });
            if (res && res.success) {
                setResult(res.data);
            }
        }
        catch (error) {
            console.log("OnLoad Item: ", error);
        }
        finally {
            setTimeout(() => {
                setLoading(false);
            }, 300);
        }
    }

    useEffect(() => { onLoad() }, [index, size, searchText]);

    return <ItemContainer
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

export default MenuItem;