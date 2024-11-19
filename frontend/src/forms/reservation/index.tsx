import { DatePicker, Form, Input, Select } from "antd"
import { Container } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { Store } from "@/type/Store";
import { TFunction } from "i18next";


type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    onFinish: () => void;
    loading: boolean;
    stores: Store[];
    t: TFunction<"translation", undefined>;
};

export const ReservationForm: React.FC<FormProps> = ({ onChange, fields, title, onFinish, loading, stores, t }) => {
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
                        label={t("table-number")}
                        name="tableNumber"
                        rules={[{ required: true, message: t("table-number-require") }]}
                    >
                        <Input placeholder={t("input-table-number")} />
                    </Form.Item>
                    <Form.Item
                        label={t("capacity")}
                        name="capacity"
                        rules={[{ required: true, message: t("capacity-require") }]}
                    >
                        <Input placeholder={t("input-capacity")} />
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
                    <Form.Item
                        label={t("status")}
                        tooltip={{ title: t("show-status") }}
                        name="status"
                        rules={[{ required: true, message: t("status-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={t("choose-status")}
                            optionFilterProp="label"
                            options={[
                                {
                                    value: 0,
                                    label: t("active"),
                                },
                                {
                                    value: 1,
                                    label: t("blocked"),
                                }
                            ]}
                        />
                    </Form.Item>
                    <Form.Item label={t("description")} name="description">
                        <Input.TextArea rows={5} placeholder={t("input-description")} />
                    </Form.Item>
                    <Form.Item
                        label={t("reservation-time")}
                        name="reservationTime"
                    >
                        <DatePicker placeholder={t("choose-time")} />
                    </Form.Item>
                    <Form.Item
                        label={t("customer-name")}
                        name="customerName"
                    >
                        <Input placeholder={t("input-customer-name")} />
                    </Form.Item>
                    <Form.Item
                        label={t("customer-phone")}
                        name="customerPhone"
                    >
                        <Input placeholder={t("input-customer-phone")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}