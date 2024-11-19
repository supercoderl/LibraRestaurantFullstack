
import { actionCategory, category } from "@/api/business/categoryApi";
import { CategoryForm } from "@/forms/category";
import Category from "@/type/Category";
import { NextPage } from "next";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { useTranslation } from "next-i18next";
import { toast } from "react-toastify";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";
import { actionDiscountType, discountType } from "@/api/business/discountTypeApi";
import { DiscountType } from "@/type/DiscountType";
import { DiscountTypeForm } from "@/forms/discount-type";
import dayjs from "dayjs";

export async function getServerSideProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const DiscountTypeAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['name'], value: '' },
        { name: ['description'], value: '' },
        { name: ['isPercentage'], value: null },
        { name: ['range'], value: null },
        { name: ['counponCode'], value: '' },
        { name: ['minOrderValue'], value: null },
        { name: ['minItemQuantity'], value: null },
        { name: ['maxDiscountValue'], value: null }
    ]);
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("discount-create"));

    const onLoad = async () => {
        if (searchParams.get('discountTypeId')) {
            try {
                const res = await discountType(Number(searchParams.get('discountTypeId')));
                if (res && res.success) {
                    setFields([
                        { name: 'name', value: res.data?.name },
                        { name: 'description', value: res.data?.description || '' },
                        { name: 'isPercentage', value: res.data?.isPercentage },
                        { name: 'range', value: [dayjs(res.data?.startTime), dayjs(res.data?.endTime)] },
                        { name: 'value', value: res.data?.value },
                        { name: 'counponCode', value: res.data?.counponCode },
                        { name: 'minOrderValue', value: res.data?.minOrderValue },
                        { name: 'minItemQuantity', value: res.data?.minItemQuantity },
                        { name: 'maxDiscountValue', value: res.data?.maxDiscountValue }
                    ]);
                    setState(t("discount-update"));
                }
            }
            catch (error) {
                console.log("Get discount by id: ", error);
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
            if (searchParams.get('discountTypeId')) {
                values = { ...values, discountTypeId: Number(searchParams.get('discountTypeId')) };
            }

            values = { ...values, startTime: values.range[0].$d, endTime: values.range[1].$d };

            const res = await actionDiscountType(values as DiscountType, searchParams.get('discountTypeId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('discountTypeId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with discount: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <DiscountTypeForm
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

export default DiscountTypeAction;