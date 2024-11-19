import { actionItem, item } from "@/api/business/itemApi";
import { ItemForm } from "@/forms/item";
import Item from "@/type/Item";
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

const ItemAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['title'], value: '' },
        { name: ['slug'], value: '' },
        { name: ['summary'], value: '' },
        { name: ['sku'], value: '' },
        { name: ['price'], value: '' },
        { name: ['quantity'], value: '' },
        { name: ['recipe'], value: '' },
        { name: ['instruction'], value: '' },
        { name: ['picture'], value: null },
        { name: ['base64'], value: null },
        { name: ['categoryIds'], value: [] }
    ]);
    const [src, setSrc] = useState("");
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("food-create"));

    const onLoad = async () => {
        if (searchParams.get('itemId')) {
            try {
                const res = await item(Number(searchParams.get('itemId')));
                if (res && res.success) {
                    setFields([
                        { name: 'title', value: res.data?.title },
                        { name: 'slug', value: res.data?.slug },
                        { name: 'summary', value: res.data?.summary || '' },
                        { name: 'sku', value: res.data?.sku },
                        { name: 'price', value: res.data?.price },
                        { name: 'quantity', value: res.data?.quantity },
                        { name: 'recipe', value: res.data?.recipe || '' },
                        { name: 'instruction', value: res.data?.instruction || '' },
                        { name: 'categoryIds', value: res.data?.categoryIds || [] },
                        { name: 'picture', value: res.data?.picture }
                    ]);
                    setSrc(res.data?.picture || "")
                    setState(t("food-update"));
                }
            }
            catch (error) {
                console.log("Get item by id: ", error);
            }
        }
    };

    const onFinish = async () => {
        setLoading(true);

        let values = fields.reduce((acc, field) => {
            if (typeof field.name === 'string') {
                acc[field.name] = field.value;
            }

            else if (Array.isArray(field.name) && typeof field.name[0] === 'string') {
                acc[field.name[0]] = field.value;
            }
            return acc;
        }, {} as { [key: string]: any });

        try {
            if (searchParams.get('itemId')) {
                values = { ...values, itemId: Number(searchParams.get('itemId')) };
            }
            if (values.base64 && values.base64.length > 0) {
                values = { ...values, base64: Array.isArray(values.base64) ? values.base64[0]?.thumbUrl : null };
            }

            const res = await actionItem(values as Item, searchParams.get('itemId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('itemId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with item: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <ItemForm
        t={t}
        fields={fields}
        onChange={(newFields) => {
            setFields(newFields);
        }}
        title={state}
        onFinish={onFinish}
        src={src}
        loading={loading}
    />
}

export default ItemAction;