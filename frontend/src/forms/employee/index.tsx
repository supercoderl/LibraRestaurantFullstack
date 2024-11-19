import { Form, Input, Select } from "antd"
import { Container } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { useStoreSelector } from "@/redux/store";
import { TFunction } from "i18next";


type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    onFinish: () => void;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const EmployeeForm: React.FC<FormProps> = ({ onChange, fields, title, onFinish, loading, t }) => {
    const [form] = Form.useForm();
    const { stores } = useStoreSelector(state => state.mainStoreSlice);

    const { width } = useWindowDimensions();

    return (
        <DashboardLayout t={t}>
            <HeaderTitle t={t} title={title} isShowText={width > 767} onSubmit={onFinish} loading={loading} />
            <Container>
                <Form
                    form={form}
                    layout="vertical"
                    fields={fields}
                    onFieldsChange={(_, allFields) => {
                        onChange(allFields);
                    }}
                >
                    <Form.Item
                        label={t("employee-name")}
                        name="firstName"
                        rules={[{ required: true, message: t("employee-name-require") }]}
                    >
                        <Input placeholder={t("input-employee-name")} />
                    </Form.Item>
                    <Form.Item
                        label={t("employee-last")}
                        name="lastName"
                        rules={[{ required: true, message: t("employee-last-require") }]}
                    >
                        <Input placeholder={t("input-employee-last")} />
                    </Form.Item>
                    <Form.Item
                        label={t("email")}
                        name="email"
                        rules={[{ required: true, message: t("email-require") }]}
                    >
                        <Input placeholder={t("input-email")} />
                    </Form.Item>
                    <Form.Item
                        label={t("phone")}
                        name="mobile"
                        rules={[{ required: true, message: t("phone-require") }]}
                    >
                        <Input placeholder={t("input-phone")} />
                    </Form.Item>
                    <Form.Item
                        label={t("store")}
                        tooltip={{ title: t("choose-available-store") }}
                        name="storeId"
                        rules={[{ required: true, message: t("store-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={t("choose-store")}
                            optionFilterProp="label"
                            options={stores.map((item) => ({ value: item.storeId, label: item.name }))}
                        />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}