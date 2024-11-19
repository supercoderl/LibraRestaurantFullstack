import { actionReservation, reservation } from "@/api/business/reservationApi";
import { stores } from "@/api/business/storeApi";
import { ReservationForm } from "@/forms/reservation";
import { Reservation } from "@/type/Reservation";
import { Store } from "@/type/Store";
import dayjs from "dayjs";
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

const ReservationAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['tableNumber'], value: '' },
        { name: ['capacity'], value: '' },
        { name: ['storeId'], value: '' },
        { name: ['status'], value: 0 },
        { name: ['description'], value: '' },
        { name: ['reservationTime'], value: dayjs(new Date()) },
        { name: ['customerName'], value: '' },
        { name: ['customerPhone'], value: '' },
        { name: ['code'], value: '' }
    ]);
    const [storeDatas, setStores] = useState<Store[]>([]);
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("reservation-create"));

    const onLoad = async () => {
        if (searchParams.get('reservationId')) {
            try {
                const res = await reservation(Number(searchParams.get('reservationId')));
                if (res && res.success) {
                    setFields([
                        { name: 'tableNumber', value: res.data?.tableNumber },
                        { name: 'capacity', value: res.data?.capacity },
                        { name: 'storeId', value: res.data?.storeId },
                        { name: 'status', value: res.data?.status },
                        { name: 'description', value: res.data?.description || '' },
                        { name: 'reservationTime', value: undefined },
                        { name: 'customerName', value: res.data?.customerName || '' },
                        { name: 'customerPhone', value: res.data?.customerPhone || '' },
                        { name: 'code', value: res.data?.qrCode }
                    ]);
                    setState(t("reservation-update"));
                }
            }
            catch (error) {
                console.log("Get reservation by id: ", error);
            }
        }
    };

    const getStoresAsync = async () => {
        try {
            const res = await stores();
            if (res?.success && res?.data && res?.data.count > 0) setStores(res?.data.items);
        }
        catch (error) {
            console.log("Get stores: ", error);
        }
    }

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
            if (searchParams.get('reservationId')) {
                values = { ...values, reservationId: Number(searchParams.get('reservationId')) };
            }
            else {
                values = {
                    ...values,
                    tableNumber: Number(values.tableNumber),
                    capacity: Number(values.capacity),
                    reservationTime: null,
                    status: 0,
                    description: values.description === '' ? null : values.description,
                    customerName: values.customerName === '' ? null : values.customerName,
                    customerPhone: values.customerPhone === '' ? null : values.customerPhone
                }
            }

            const res = await actionReservation(values as Reservation, searchParams.get('reservationId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('reservationId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with reservation: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
        getStoresAsync();
    }, []);

    return <ReservationForm
        t={t}
        fields={fields}
        onChange={(newFields) => {
            setFields(newFields);
        }}
        title={state}
        onFinish={onFinish}
        loading={loading}
        stores={storeDatas}
    />
}

export default ReservationAction;