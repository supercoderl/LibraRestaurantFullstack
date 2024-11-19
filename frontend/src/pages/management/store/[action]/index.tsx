
import { actionStore, store } from "@/api/business/storeApi";
import { StoreForm } from "@/forms/store";
import { Store } from "@/type/Store";
import { NextPage } from "next";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { useTranslation } from "next-i18next";
import { toast } from "react-toastify";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";

export async function getServerSideProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const StoreAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['name'], value: '' },
        { name: ['cityId'], value: 1 },
        { name: ['disctrictId'], value: 1 },
        { name: ['wardId'], value: 1 },
        { name: ['taxCode'], value: '' },
        { name: ['address'], value: '' },
        { name: ['gpsLocation'], value: '' },
        { name: ['postalCode'], value: '' },
        { name: ['phone'], value: '' },
        { name: ['fax'], value: '' },
        { name: ['email'], value: '' },
        { name: ['website'], value: '' },
        { name: ['logo'], value: '' },
        { name: ['bankBranch'], value: '' },
        { name: ['bankCode'], value: '' },
        { name: ['bankAccount'], value: '' },
        { name: ['isActive'], value: true }
    ]);
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("store-create"));

    const onLoad = async () => {
        if (searchParams.get('storeId')) {
            try {
                const res = await store(searchParams.get('storeId'));
                if (res && res.success) {
                    setFields([
                        { name: 'name', value: res.data?.name },
                        { name: 'cityId', value: res.data?.cityId },
                        { name: 'districtId', value: res.data?.districtId },
                        { name: 'wardId', value: res.data?.wardId },
                        { name: 'taxCode', value: res.data?.taxCode || '' },
                        { name: 'address', value: res.data?.address },
                        { name: 'gpsLocation', value: res.data?.gpsLocation || '' },
                        { name: 'postalCode', value: res.data?.postalCode || '' },
                        { name: 'phone', value: res.data?.phone || '' },
                        { name: 'fax', value: res.data?.fax || '' },
                        { name: 'email', value: res.data?.email || '' },
                        { name: 'website', value: res.data?.website || '' },
                        { name: 'logo', value: res.data?.logo || '' },
                        { name: 'bankBranch', value: res.data?.bankBranch || '' },
                        { name: 'bankCode', value: res.data?.bankCode || '' },
                        { name: 'bankAccount', value: res.data?.bankAccount || '' },
                        { name: 'isActive', value: res.data?.isActive }
                    ]);
                    setState(t("store-update"));
                }
            }
            catch (error) {
                console.log("Get store by id: ", error);
            }
        }
    };

    const onFinish = async () => {
        setLoading(true);
        let values = fields.reduce((acc, field) => {
            if (Array.isArray(field.name) && typeof field.name[0] === 'string') {
                acc[field.name[0]] = field.value;
            }
            else if (typeof field.name === 'string') {
                acc[field.name] = field.value;
            }
            return acc;
        }, {} as { [key: string]: any });

        try {
            if (searchParams.get('storeId')) {
                values = { ...values, storeId: searchParams.get('storeId') };
            }

            const res = await actionStore(values as Store, searchParams.get('storeId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('storeId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with store: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <StoreForm
        t={t}
        fields={fields}
        onChange={(newFields) => {
            setFields(newFields);
        }}
        title={state}
        onFinish={onFinish}
        loading={loading}
    />
}

export default StoreAction;