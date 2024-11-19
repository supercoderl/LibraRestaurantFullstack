import { Form, Input, Upload, Image, Select } from "antd"
import { Container, ImageContainer } from "../style"
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
    src?: string | null;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const ItemForm: React.FC<FormProps> = (props) => {
    const [form] = Form.useForm();
    const { width } = useWindowDimensions();

    const { categories } = useStoreSelector(state => state.mainCategorySlice);

    const normFile = (e: any) => {
        if (Array.isArray(e)) {
            return e;
        }
        return e?.fileList;
    };

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
                        label={props.t("food-name")}
                        tooltip={props.t("food-require")}
                        name="title"
                        rules={[{ required: true, message: props.t("food-require") }]}
                    >
                        <Input placeholder={props.t("input-food")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("slug")}
                        tooltip={{ title: props.t("slug-replace") }}
                        name="slug"
                        rules={[{ required: true, message: props.t("slug-require") }]}
                    >
                        <Input addonBefore="https://librarestaurant.vn/" placeholder="slug-example" />
                    </Form.Item>
                    <Form.Item label={props.t("description")} name="summary">
                        <Input.TextArea rows={5} placeholder={props.t("input-description")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("sku")}
                        tooltip={props.t("sku-start-with-FD-or-DK")}
                        name="sku"
                        rules={[{ required: true, message: props.t("sku-require") }]}
                    >
                        <Input placeholder={props.t("input-sku")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("category")}
                        name="categoryIds"
                    >
                        <Select
                            showSearch
                            mode="multiple"
                            placeholder={props.t("choose-category")}
                            optionFilterProp="label"
                            options={categories.map((item) => ({ value: item.categoryId, label: item.name }))}
                        />
                    </Form.Item>
                    <Form.Item name="picture" noStyle />
                    <ImageContainer>
                        <Form.Item
                            label={props.t("picture")}
                            name="base64"
                            valuePropName="fileList"
                            getValueFromEvent={normFile}
                        >
                            <Upload
                                listType="picture-card"
                                multiple={false}
                                className="customSizedUpload"
                            >
                                {props.src && props.src !== '' ? props.t("change-picture") : props.t("add-picture")}
                            </Upload>
                        </Form.Item>
                        {
                            props.src && props.src !== '' && <Image
                                width={200}
                                style={{ borderRadius: 5, border: 1, borderStyle: 'solid', borderColor: 'rgba(0, 0, 0, 0.1)', marginTop: 6, padding: 5 }}
                                src={props.src}
                            />
                        }
                    </ImageContainer>
                    <Form.Item
                        label={props.t("price")}
                        tooltip={props.t("food-price")}
                        name="price"
                        rules={[{ required: true, message: props.t("price-require") }]}
                    >
                        <Input placeholder={props.t("input-price")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("quantity")}
                        tooltip={props.t("quantity-in-stock")}
                        name="quantity"
                        rules={[{ required: true, message: props.t("quantity-require") }]}
                    >
                        <Input placeholder={props.t("input-quantity")} />
                    </Form.Item>
                    <Form.Item label={props.t("recipe")} name="recipe">
                        <Input.TextArea rows={5} placeholder={props.t("input-recipe")} />
                    </Form.Item>
                    <Form.Item label={props.t("instruction")} name="instruction">
                        <Input.TextArea rows={5} placeholder={props.t("input-instruction")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout >
    )
}