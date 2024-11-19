import { Image, Form, Input, Select, Upload } from "antd"
import { Container, ImageContainer } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { TFunction } from "i18next";


type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    src?: string | null;
    onFinish: () => void;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const CategoryForm: React.FC<FormProps> = (props: FormProps) => {
    const [form] = Form.useForm();

    const { width } = useWindowDimensions();

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
                        label={props.t("category-name")}
                        name="name"
                        rules={[{ required: true, message: props.t("category-require") }]}
                    >
                        <Input placeholder={props.t("input-category")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("status")}
                        tooltip={{ title: props.t("show-category") }}
                        name="isActive"
                        rules={[{ required: true, message: props.t("status-require") }]}
                    >
                        <Select
                            showSearch
                            placeholder={props.t("choose-status")}
                            optionFilterProp="label"
                            options={[
                                {
                                    value: true,
                                    label: props.t("active"),
                                },
                                {
                                    value: false,
                                    label: props.t("blocked"),
                                }
                            ]}
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
                    <Form.Item label={props.t("description")} name="description">
                        <Input.TextArea rows={5} placeholder={props.t("input-description")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}