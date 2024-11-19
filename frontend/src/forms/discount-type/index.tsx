import { DatePicker, Form, GetProps, Input, Select } from "antd"
import { Container } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { TFunction } from "i18next";
import dayjs from "dayjs";

const { RangePicker } = DatePicker;
type RangePickerProps = GetProps<typeof DatePicker.RangePicker>;

const disabledDate: RangePickerProps['disabledDate'] = (current) => {
    // Can not select days before today and today
    return current && current < dayjs().endOf('day');
};

type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    onFinish: () => void;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const DiscountTypeForm: React.FC<FormProps> = (props: FormProps) => {
    const [form] = Form.useForm();

    const { width } = useWindowDimensions();

    return (
        <DashboardLayout t={props.t}>
            <HeaderTitle t={props.t} title={props.title} isShowText={width > 767} onSubmit={props.onFinish} loading={props.loading} />
            <Container>
                <Form
                    form={form}
                    layout="vertical"
                    fields={props.fields}
                    onFieldsChange={(_, allFields) => {
                        props.onChange(allFields);
                    }}
                    onFinish={props.onFinish}
                    onFinishFailed={() => console.log("failed")}
                >
                    <Form.Item
                        label={props.t("discount-type-name")}
                        name="name"
                        rules={[{ required: true, message: props.t("discount-type-require") }]}
                    >
                        <Input placeholder={props.t("input-discount-name")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("isPercentage")}
                        name="isPercentage"
                        rules={[{ required: true, message: props.t("isPercentage-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={props.t("choose-isPercentage")}
                            optionFilterProp="label"
                            options={[
                                {
                                    value: true,
                                    label: props.t("yes"),
                                },
                                {
                                    value: false,
                                    label: props.t("no"),
                                }
                            ]}
                        />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-value")}
                        name="value"
                        rules={[{ required: true, message: props.t("discount-value-require") }]}
                    >
                        <Input inputMode="numeric" placeholder={props.t("input-discount-value")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-counpon")}
                        name="counponCode"
                    >
                        <Input placeholder={props.t("input-discount-counpon")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-range-time")}
                        name="range"
                        rules={[{ required: true, message: props.t("discount-time-require") }]}
                    >
                        <RangePicker disabledDate={disabledDate} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-min-order-value")}
                        name="minOrderValue"
                        rules={[{ required: true, message: props.t("discount-min-order-value-require") }]}
                    >
                        <Input inputMode="numeric" placeholder={props.t("input-discount-min-order-value")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-min-item-quantity")}
                        name="minItemQuantity"
                        rules={[{ required: true, message: props.t("discount-min-item-quantity-require") }]}
                    >
                        <Input inputMode="numeric" placeholder={props.t("input-discount-min-item-quantity")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-max-value")}
                        name="maxDiscountValue"
                        rules={[{ required: true, message: props.t("discount-max-value-require") }]}
                    >
                        <Input inputMode="numeric" placeholder={props.t("input-discount-max-value")} />
                    </Form.Item>
                    <Form.Item label={props.t("description")} name="description">
                        <Input.TextArea rows={5} placeholder={props.t("input-description")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}