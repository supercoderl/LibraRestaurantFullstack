import { Form, Input, Select } from "antd"
import { Container } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { TFunction } from "i18next";
import { useStoreSelector } from "@/redux/store";


type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    src?: string | null;
    onFinish: () => void;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const DiscountForm: React.FC<FormProps> = (props: FormProps) => {
    const [form] = Form.useForm();

    const { width } = useWindowDimensions();

    const { categories } = useStoreSelector(state => state.mainCategorySlice);
    const { items } = useStoreSelector(state => state.mainProductSlice);
    const { discountTypes } = useStoreSelector(state => state.mainDiscountTypeSlice);

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
                >
                    <Form.Item
                        label={props.t("discount-type-name")}
                        tooltip={{ title: props.t("choose-discount-type") }}
                        name="discountTypeId"
                        rules={[{ required: true, message: props.t("discount-type-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={props.t("choose-discount-type")}
                            optionFilterProp="label"
                            options={discountTypes.map(item => ({ label: item.name, value: item.discountTypeId }))}
                        />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-category")}
                        tooltip={{ title: props.t("choose-category") }}
                        name="categoryId"
                        rules={[{ required: true, message: props.t("discount-category-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={props.t("choose-discount-category")}
                            optionFilterProp="label"
                            options={categories.map(item => ({ label: item.name, value: item.categoryId }))}
                        />
                    </Form.Item>
                    <Form.Item
                        label={props.t("discount-item")}
                        tooltip={{ title: props.t("choose-discount-item") }}
                        name="itemId"
                        rules={[{ required: true, message: props.t("discount-item-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={props.t("choose-discount-item")}
                            optionFilterProp="label"
                            options={items.map(item => ({ label: item.title, value: item.itemId }))}
                        />
                    </Form.Item>
                    <Form.Item label={props.t("comment")} name="comments">
                        <Input.TextArea rows={5} placeholder={props.t("input-comment")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}