import { Button, Col, Form, Input, Row, Select, Upload, UploadProps } from "antd"
import { Container } from "../style"
import { DashboardLayout } from "@/layouts/DashboardLayout";
import HeaderTitle from "@/components/dashboard/headerTitle";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { useState } from "react";
import { LoadingOutlined, PlusOutlined } from "@ant-design/icons";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { filterDistrictsAndWards, filterWards } from "@/redux/slices/locations-slice";
import { TFunction } from "i18next";


type FormProps = {
    onChange: (fields: FieldData[]) => void;
    fields: FieldData[];
    title: string;
    onFinish: () => void;
    loading: boolean;
    t: TFunction<"translation", undefined>
};

export const StoreForm: React.FC<FormProps> = ({ onChange, fields, title, onFinish, loading, t }) => {
    const [form] = Form.useForm();
    const { cities, districts, wards } = useStoreSelector(
        state => ({
            cities: state.mainLocationSlice.cities,
            districts: state.mainLocationSlice.districts,
            wards: state.mainLocationSlice.wards
        })
    );
    const dispatch = useStoreDispatch();

    const { width } = useWindowDimensions();

    const [imageUrl, setImageUrl] = useState<string>();

    const handleChange: UploadProps['onChange'] = (info) => {
        if (info.file.status === 'uploading') {
            return;
        }
        if (info.file.status === 'done') {
            // Get this url from response in real world.
        }
    };

    const handleChangeCity = (cityId: number) => {
        dispatch(filterDistrictsAndWards(cityId));
    }

    const handleChangeDistrict = (districtId: number) => {
        dispatch(filterWards(districtId));
    }

    const uploadButton = (
        <button style={{ border: 0, background: 'none' }} type="button">
            {loading ? <LoadingOutlined /> : <PlusOutlined />}
            <div style={{ marginTop: 8 }}>Upload</div>
        </button>
    );

    return (
        <DashboardLayout t={t}>
            <HeaderTitle
                title={title}
                isShowText={width > 767}
                onSubmit={onFinish}
                loading={loading}
                t={t}
            />
            <Container>
                <Form
                    form={form}
                    layout="vertical"
                    fields={fields}
                    onFieldsChange={(_, allFields) => {
                        onChange(allFields);
                    }}
                >
                    <Row gutter={[16, 0]} justify="center">
                        <Col span={6}>
                            <Form.Item
                                label={t("store-name")}
                                name="name"
                                rules={[{ required: true, message: t("store-name-require") }]}
                            >
                                <Input placeholder={t("input-store")} />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("city")}
                                name="cityId"
                                rules={[{ required: true, message: t("city-require") }]}
                            >
                                <Select
                                    showSearch
                                    placeholder={t("choose-city")}
                                    optionFilterProp="label"
                                    onChange={handleChangeCity}
                                    options={cities.map(city => ({ value: city.cityId, label: city.name }))}
                                />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("district")}
                                name="districtId"
                                rules={[{ required: true, message: t("district-require") }]}
                            >
                                <Select
                                    showSearch
                                    placeholder={t("choose-district")}
                                    optionFilterProp="label"
                                    onChange={handleChangeDistrict}
                                    options={districts.map(district => ({ value: district.districtId, label: district.name }))}
                                />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("ward")}
                                name="wardId"
                                rules={[{ required: true, message: t("ward-require") }]}
                            >
                                <Select
                                    showSearch
                                    placeholder={t("choose-ward")}
                                    optionFilterProp="label"
                                    options={wards.map(ward => ({ value: ward.wardId, label: ward.name }))}
                                />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("tax-code")}
                                name="taxCode"
                            >
                                <Input placeholder={t("input-tax-code")} />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("postal-code")}
                                name="postalCode"
                            >
                                <Input placeholder={t("input-postal-code")} />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("phone")}
                                name="phone"
                            >
                                <Input placeholder={t("input-phone")} />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Form.Item
                                label={t("fax")}
                                name="fax"
                            >
                                <Input placeholder={t("input-fax")} />
                            </Form.Item>
                        </Col>
                        <Col span={6}>
                            <Row>

                                <Col span={12}>
                                    <Form.Item
                                        label="Logo"
                                        name="logo"
                                    >
                                        <Upload
                                            name="avatar"
                                            listType="picture-card"
                                            className="avatar-uploader"
                                            showUploadList={false}
                                            action="https://660d2bd96ddfa2943b33731c.mockapi.io/api/upload"
                                            onChange={handleChange}
                                        >
                                            {imageUrl ? <img src={imageUrl} alt="avatar" style={{ width: '100%' }} /> : uploadButton}
                                        </Upload>
                                    </Form.Item>
                                </Col>
                            </Row>
                        </Col>
                        <Col span={18}>
                            <Row gutter={[16, 0]}>
                                <Col span={8}>
                                    <Form.Item
                                        label={t("bank-branch")}
                                        name="bankBranch"
                                    >
                                        <Input placeholder={t("input-bank-branch")} />
                                    </Form.Item>
                                </Col>
                                <Col span={8}>
                                    <Form.Item
                                        label={t("email")}
                                        name="email"
                                    >
                                        <Input placeholder={t("input-email")} />
                                    </Form.Item>
                                </Col>
                                <Col span={8}>
                                    <Form.Item
                                        label={t("website")}
                                        name="website"
                                    >
                                        <Input placeholder={t("input-website")} />
                                    </Form.Item>
                                </Col>
                                <Col span={8}>
                                    <Form.Item
                                        label={t("bank-code")}
                                        name="bankCode"
                                    >
                                        <Input placeholder={t("input-bank-code")} />
                                    </Form.Item>
                                </Col>
                                <Col span={8}>
                                    <Form.Item
                                        label={t("account-number")}
                                        name="bankAccount"
                                    >
                                        <Input placeholder={t("input-account-number")} />
                                    </Form.Item>
                                </Col>
                                <Col span={8}>
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
                                </Col>
                            </Row>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label={t("address")}
                                name="address"
                                rules={[{ required: true, message: t("address-require") }]}

                            >
                                <Input.TextArea rows={6} value={t("input-address")} />
                            </Form.Item>
                        </Col>
                        <Col span={12}>
                            <Form.Item
                                label={t("gps")}
                                name="gpsLocation"
                            >
                                <Input.TextArea rows={6} value={t("input-gps")} />
                            </Form.Item>
                        </Col>
                    </Row>
                </Form>
            </Container>
        </DashboardLayout>
    )
}