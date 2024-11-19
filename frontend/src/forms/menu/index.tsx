import { Button, Form, Input, Select } from "antd"
import { Container } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { TFunction } from "i18next";


type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    onFinish: () => void;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const MenuForm: React.FC<FormProps> = ({ onChange, fields, title, onFinish, loading, t }) => {
    const [form] = Form.useForm();

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
                        label={t("menu-name")}
                        name="name"
                        rules={[{ required: true, message: t("menu-require") }]}
                    >
                        <Input placeholder={t("input-menu")} />
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
                            options={[
                                {
                                    value: 'jack',
                                    label: 'Jack',
                                },
                                {
                                    value: 'lucy',
                                    label: 'Lucy',
                                },
                                {
                                    value: 'tom',
                                    label: 'Tom',
                                },
                            ]}
                        />
                    </Form.Item>
                    <Form.Item
                        label={t("status")}
                        tooltip={{ title: t("show-status") }}
                        name="isActive"
                        rules={[{ required: true, message: t("status-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={t("choose-status")}
                            optionFilterProp="label"
                            options={[
                                {
                                    value: true,
                                    label: t("active"),
                                },
                                {
                                    value: false,
                                    label: t("blocked"),
                                }
                            ]}
                        />
                    </Form.Item>
                    <Form.Item label={t("description")} name="description">
                        <Input.TextArea rows={5} placeholder={t("input-description")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}