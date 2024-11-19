
import { NextPage } from "next";
import { useRouter, useSearchParams } from "next/navigation";
import { useEffect, useState } from "react";
import { useTranslation } from "next-i18next";
import { toast } from "react-toastify";
import { DiscountForm } from "@/forms/discount";
import { Discount } from "@/type/Discount";
import { actionDiscount, discount } from "@/api/business/discountApi";
import { serverSideTranslations } from "next-i18next/serverSideTranslations";

export async function getServerSideProps({ locale }: { locale: string }) {
    return {
        props: {
            ...(await serverSideTranslations(locale, ['common'])),
        },
    };
}

const DiscountAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['discountTypeId'], value: null },
        { name: ['categoryId'], value: null },
        { name: ['itemId'], value: null },
        { name: ['comments'], value: null }
    ]);
    const [src, setSrc] = useState("");
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("discount-create"));

    const onLoad = async () => {
        if (searchParams.get('discountId')) {
            try {
                const res = await discount(Number(searchParams.get('discountId')));
                if (res && res.success) {
                    setFields([
                        { name: 'discountTypeId', value: res.data?.discountTypeId },
                        { name: 'categoryId', value: res.data?.categoryId },
                        { name: 'itemId', value: res.data?.itemId },
                        { name: 'comments', value: res.data?.comments }
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
            if (searchParams.get('discountId')) {
                values = { ...values, discountId: Number(searchParams.get('discountId')) };
            }

            const res = await actionDiscount(values as Discount, searchParams.get('discountId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('discountId') ? t("update") : t("create")} ${t("success")}`, {
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

    return <DiscountForm
        t={t}
        fields={fields}
        onChange={(newFields) => {
            setFields(newFields);
        }}
        title={state}
        src={src}
        onFinish={onFinish}
        loading={loading}
    />
}

export default DiscountAction;