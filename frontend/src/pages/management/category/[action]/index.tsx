
import { actionCategory, category } from "@/api/business/categoryApi";
import { CategoryForm } from "@/forms/category";
import Category from "@/type/Category";
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

const CategoryAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['name'], value: '' },
        { name: ['description'], value: '' },
        { name: ['isActive'], value: true },
        { name: ['picture'], value: null },
        { name: ['base64'], value: null },
    ]);
    const [src, setSrc] = useState("");
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("category-create"));

    const onLoad = async () => {
        if (searchParams.get('categoryId')) {
            try {
                const res = await category(Number(searchParams.get('categoryId')));
                if (res && res.success) {
                    setFields([
                        { name: 'name', value: res.data?.name },
                        { name: 'description', value: res.data?.description || '' },
                        { name: 'isActive', value: res.data?.isActive },
                        { name: 'picture', value: res.data?.picture }
                    ]);
                    setSrc(res.data?.picture || "")
                    setState(t("category-update"));
                }
            }
            catch (error) {
                console.log("Get category by id: ", error);
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
            if (searchParams.get('categoryId')) {
                values = { ...values, categoryId: Number(searchParams.get('categoryId')) };
            }

            if (values.base64 && values.base64.length > 0) {
                values = { ...values, base64: Array.isArray(values.base64) ? values.base64[0]?.thumbUrl : null };
            }

            const res = await actionCategory(values as Category, searchParams.get('categoryId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('categoryId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with category: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <CategoryForm
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

export default CategoryAction;