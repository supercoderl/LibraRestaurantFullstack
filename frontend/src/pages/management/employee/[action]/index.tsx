import { actionEmployee, employee } from "@/api/business/userApi";
import { EmployeeForm } from "@/forms/employee";
import { Employee } from "@/type/Employee";
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

const EmployeeAction: NextPage = () => {
    const searchParams = useSearchParams();
    const { t } = useTranslation();
    const router = useRouter();
    const [fields, setFields] = useState<FieldData[]>([
        { name: ['firstName'], value: '' },
        { name: ['lastName'], value: '' },
        { name: ['storeId'], value: '' },
        { name: ['email'], value: '' },
        { name: ['mobile'], value: '' },
        { name: ['password'], value: 'Password123!' }
    ]);
    const [loading, setLoading] = useState(false);
    const [state, setState] = useState(t("employee-create"));

    const onLoad = async () => {
        if (searchParams.get('employeeId')) {
            try {
                const res = await employee(searchParams.get('employeeId'));
                if (res && res.success) {
                    setFields([
                        { name: 'firstName', value: res.data?.firstName },
                        { name: 'lastName', value: res.data?.lastName },
                        { name: 'storeId', value: res.data?.storeId },
                        { name: 'email', value: res.data?.email },
                        { name: 'mobile', value: res.data?.mobile },
                        { name: 'password', value: res.data?.password }
                    ]);
                    setState(t("employee-update"));
                }
            }
            catch (error) {
                console.log("Get employee by id: ", error);
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
            if (searchParams.get('employeeId')) {
                values = { ...values, id: searchParams.get('employeeId') };
            }

            const res = await actionEmployee(values as Employee, searchParams.get('employeeId') ? 'edit' : 'create');
            if (res && res.success) {
                toast(`${searchParams.get('employeeId') ? t("update") : t("create")} ${t("success")}`, {
                    type: "success"
                });
                router.push("general");
            }
        }
        catch (error) {
            console.log("Action with employee: ", error);
        }
        finally {
            setLoading(false);
        }
    }

    useEffect(() => {
        onLoad();
    }, []);

    return <EmployeeForm
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

export default EmployeeAction;