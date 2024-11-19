import { actionMenu, menu } from "@/api/business/menuApi";
import { MenuForm } from "@/forms/menu";
import Menu from "@/type/Menu";
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

const MenuAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['name'], value: '' },
        { name: ['storeId'], value: '' },
        { name: ['description'], value: '' },
        { name: ['isActive'], value: true }
    ]);
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("menu-create"));

    const onLoad = async () => {
        if (searchParams.get('menuId')) {
            try {
                const res = await menu(Number(searchParams.get('menuId')));
                if (res && res.success) {
                    setFields([
                        { name: 'name', value: res.data?.name },
                        { name: 'storeId', value: res.data?.storeId },
                        { name: 'description', value: res.data?.description || '' },
                        { name: 'isActive', value: res.data?.isActive }
                    ]);
                    setState(t("menu-update"));
                }
            }
            catch (error) {
                console.log("Get menu by id: ", error);
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
            if (searchParams.get('menuId')) {
                values = { ...values, menuId: Number(searchParams.get('menuId')) };
            }

            const res = await actionMenu(values as Menu, searchParams.get('menuId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('menuId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with menu: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <MenuForm
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

export default MenuAction;