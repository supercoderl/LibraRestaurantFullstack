
import { actionRole, role } from "@/api/business/roleApi";
import { RoleForm } from "@/forms/role";
import { Role } from "@/type/Role";
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

const RoleAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['roleId'], value: "0" },
        { name: ['name'], value: '' },
        { name: ['description'], value: '' },
    ]);
    const [src, setSrc] = useState("");
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("role-create"));

    const onLoad = async () => {
        if (searchParams.get('roleId')) {
            try {
                const res = await role(Number(searchParams.get('roleId')));
                if (res && res.success) {
                    setFields([
                        { name: 'roleId', value: res.data?.roleId },
                        { name: 'name', value: res.data?.name },
                        { name: 'description', value: res.data?.description || '' }
                    ]);
                    setSrc(res.data?.picture || "")
                    setState(t("role-update"));
                }
            }
            catch (error) {
                console.log("Get role by id: ", error);
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
            const res = await actionRole(values as Role, searchParams.get('roleId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('roleId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with role: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <RoleForm
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

export default RoleAction;