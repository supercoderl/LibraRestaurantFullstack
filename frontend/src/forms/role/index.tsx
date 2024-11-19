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

export const RoleForm: React.FC<FormProps> = (props: FormProps) => {
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
                >
                    <Form.Item
                        label={props.t("roleId")}
                        name="roleId"
                        rules={[{ required: true, message: props.t("roleId-require") }]}
                    >
                        <Input placeholder={props.t("input-roleId")} />
                    </Form.Item>
                    <Form.Item
                        label={props.t("role-name")}
                        name="name"
                        rules={[{ required: true, message: props.t("role-name-require") }]}
                    >
                        <Input placeholder={props.t("input-role-name")} />
                    </Form.Item>
                    <Form.Item label={props.t("description")} name="description">
                        <Input.TextArea rows={5} placeholder={props.t("input-description")} />
                    </Form.Item>
                </Form>
            </Container>
        </DashboardLayout>
    )
}