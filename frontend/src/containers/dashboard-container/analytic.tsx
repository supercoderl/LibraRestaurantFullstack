import React, { useEffect } from "react";
import { Divider, Row, Col } from "antd";

import { Statistic, Progress, Tag } from "antd";

import { ArrowUpOutlined } from "@ant-design/icons";

import { DashboardLayout } from "@/layouts/DashboardLayout";
import RecentTable from "@/components/recent-table";
import { Container, PreviewContainer, PreviewContentContainer, PreviewStateContainer, PreviewText, PreviewTextContainer, PreviewTextProgress, ProgressContainer, RecentContainer, RecentText, Space, Text, TextContainer } from "./style";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { items } from "@/api/business/itemApi";
import dayjs from "dayjs";
import { useStoreSelector } from "@/redux/store";
import { TFunction } from "i18next";
import { useTranslation } from "next-i18next";
import { Spinner } from "@/components/loading/spinner";

type TopCardProps = {
    title: string;
    tagContent: string;
    tagColor: string;
    prefix: string;
    isFullWidth?: boolean;
    loading: boolean;
}

const TopCard: React.FC<TopCardProps> = ({ title, tagContent, tagColor, prefix, isFullWidth, loading }) => {

    return (
        <Col className="gutter-row" span={isFullWidth ? 24 : 6}>
            <Container>
                <TextContainer>
                    <Text>{title}</Text>
                </TextContainer>
                <Divider style={{ padding: 0, margin: 0 }}></Divider>
                <TextContainer>
                    <Row gutter={[0, 0]}>
                        <Col className="gutter-row" span={11} style={{ textAlign: "left" }}>
                            <div className="left">{prefix}</div>
                        </Col>
                        <Col className="gutter-row" span={2}>
                            <Divider
                                style={{ padding: "10px 0", justifyContent: "center" }}
                                type="vertical"
                            ></Divider>
                        </Col>
                        <Col
                            className="gutter-row"
                            span={11}
                            style={{ display: "flex", justifyContent: "center" }}
                        >
                            <Tag
                                color={tagColor}
                                style={{ alignItems: "center", justifyContent: "center", display: "flex" }}
                            >
                                {loading ? <Spinner width={16} /> : tagContent}
                            </Tag>
                        </Col>
                    </Row>
                </TextContainer>
            </Container>
        </Col>
    );
};

type PreviewStateProps = {
    tag: string;
    color: string;
    value: number;
}

const PreviewState: React.FC<PreviewStateProps> = ({ tag, color, value }) => {
    let colorCode = "#000";
    switch (color) {
        case "bleu":
            colorCode = "#1890ff";
            break;
        case "green":
            colorCode = "#95de64";
            break;
        case "red":
            colorCode = "#ff4d4f";
            break;
        case "orange":
            colorCode = "#ffa940";
            break;
        case "purple":
            colorCode = "#722ed1";
            break;
        case "grey":
            colorCode = "#595959";
            break;
        case "cyan":
            colorCode = "#13c2c2";
            break;
        case "brown":
            colorCode = "#614700";
            break;
        default:
            break;
    }
    return (
        <PreviewStateContainer>
            <PreviewContentContainer>
                <div className="left alignLeft">{tag}</div>
                <div className="right alignRight">{value} %</div>
            </PreviewContentContainer>
            <Progress
                percent={value}
                showInfo={false}
                strokeColor={{
                    "0%": colorCode,
                    "100%": colorCode,
                }}
            />
        </PreviewStateContainer>
    );
};
export default function DashboardContainer({ t }: { t: TFunction<"translation", undefined> }) {
    const { width } = useWindowDimensions();
    const { data, loading } = useStoreSelector(state => ({
        data: state.mainDashboardSlice.data,
        loading: state.mainDashboardSlice.loading
    }));

    const leadColumns = [
        {
            title: "Tên khách hàng",
            dataIndex: "customerName",
        },
        {
            title: "Số điện thoại",
            dataIndex: "customerPhone",
        },
        {
            title: "Trạng thái",
            dataIndex: "status",
            render: (status: string) => {
                let color = status === "pending" ? "volcano" : "green";

                return <Tag color={color}>{status}</Tag>;
            },
        },
    ];

    const productColumns = [
        {
            title: "Tên món",
            dataIndex: "title",
        },

        {
            title: "Đơn giá",
            dataIndex: "price",
        },
        {
            title: "Tồn kho",
            dataIndex: "quantity",
        },
    ];

    return (
        <DashboardLayout t={t}>
            <Row gutter={[24, 24]} style={{ flexDirection: width > 767 ? 'row' : 'column' }}>
                <TopCard
                    title={t("buy-material")}
                    tagColor={"cyan"}
                    prefix={`Tháng ${dayjs(new Date).get('month') + 1}`}
                    tagContent={"34,000,000 ₫"}
                    isFullWidth={width <= 767}
                    loading={loading}
                />
                <TopCard
                    title={t("order")}
                    tagColor={"purple"}
                    prefix={`Tổng số`}
                    tagContent={`${data.orderCount} đơn`}
                    isFullWidth={width <= 767}
                    loading={loading}
                />
                <TopCard
                    title={t("employee")}
                    tagColor={"green"}
                    prefix={`Ca tối`}
                    tagContent={"12 người đang làm"}
                    isFullWidth={width <= 767}
                    loading={loading}
                />
                <TopCard
                    title={t("revenue")}
                    tagColor={"red"}
                    prefix={`Tháng ${dayjs(new Date).get('month') + 1}`}
                    tagContent={`${data.paymentAmount} ₫`}
                    isFullWidth={width <= 767}
                    loading={loading}
                />
            </Row>
            <Space />
            <Row gutter={[24, 24]}>
                {
                    width > 767 &&
                    <Col className="gutter-row" span={18}>
                        <PreviewContainer>
                            <Row className="pad10" gutter={[0, 0]}>
                                <Col className="gutter-row" span={8}>
                                    <PreviewTextContainer>
                                        <PreviewText>{t("review-direct")}</PreviewText>
                                        <PreviewState tag={"Draft"} color={"grey"} value={3} />
                                        <PreviewState tag={"Pending"} color={"bleu"} value={5} />
                                        <PreviewState tag={"Not Paid"} color={"orange"} value={12} />
                                        <PreviewState tag={"Overdue"} color={"red"} value={6} />
                                        <PreviewState
                                            tag={"Partially Paid"}
                                            color={"cyan"}
                                            value={8}
                                        />
                                        <PreviewState tag={"Paid"} color={"green"} value={55} />
                                    </PreviewTextContainer>
                                </Col>
                                <Col className="gutter-row" span={8}>
                                    {" "}
                                    <PreviewTextContainer>
                                        <PreviewText>{t("review-google")}</PreviewText>
                                        <PreviewState tag={"Draft"} color={"grey"} value={3} />
                                        <PreviewState tag={"Pending"} color={"bleu"} value={5} />
                                        <PreviewState tag={"Not Paid"} color={"orange"} value={12} />
                                        <PreviewState tag={"Overdue"} color={"red"} value={6} />
                                        <PreviewState
                                            tag={"Partially Paid"}
                                            color={"cyan"}
                                            value={8}
                                        />
                                        <PreviewState tag={"Paid"} color={"green"} value={55} />
                                    </PreviewTextContainer>
                                </Col>
                                <Col className="gutter-row" span={8}>
                                    {" "}
                                    <PreviewTextContainer>
                                        <PreviewText>{t("review-order")}</PreviewText>
                                        <PreviewState tag={"Draft"} color={"grey"} value={3} />
                                        <PreviewState tag={"Pending"} color={"bleu"} value={5} />
                                        <PreviewState tag={"Not Paid"} color={"orange"} value={12} />
                                        <PreviewState tag={"Overdue"} color={"red"} value={6} />
                                        <PreviewState
                                            tag={"Partially Paid"}
                                            color={"cyan"}
                                            value={8}
                                        />
                                        <PreviewState tag={"Paid"} color={"green"} value={55} />
                                    </PreviewTextContainer>
                                </Col>
                            </Row>
                        </PreviewContainer>
                    </Col>
                }

                <Col className="gutter-row" span={width > 767 ? 6 : 24}>
                    <PreviewContainer>
                        <TextContainer>
                            <PreviewTextProgress>{t("profit-from-cus")}</PreviewTextProgress>

                            {
                                loading ?
                                    <ProgressContainer>
                                        <Spinner width={148} color="#1677ff" />
                                    </ProgressContainer>
                                    :
                                    <Progress type="dashboard" percent={data?.customer?.customerCountInThisMonth} size={148} format={(percent) => `${percent}`} />
                            }

                            <p>{t("quantity-cus-in-month")}</p>
                            <Divider />
                            <Statistic
                                title={data?.customer?.percentage > 0 ? t("increase") : t("decrease")}
                                value={Math.abs(data?.customer?.percentage)}
                                precision={2}
                                valueStyle={{ color: "#3f8600" }}
                                prefix={<ArrowUpOutlined />}
                                suffix="%"
                            />
                        </TextContainer>
                    </PreviewContainer>
                </Col>
            </Row>
            <Space />
            <Row gutter={[24, 24]}>
                <Col className="gutter-row" span={width > 767 ? 12 : 24}>
                    <RecentContainer>
                        <PreviewTextContainer>
                            <RecentText>
                                {t("cus-recent")}
                            </RecentText>
                        </PreviewTextContainer>

                        <RecentTable
                            entity={"lead"}
                            data={data?.customer?.top5Customers?.map((item, index) => ({ ...item, key: index + 1 }))}
                            dataTableColumns={leadColumns}
                            loading={loading}
                        />
                    </RecentContainer>
                </Col>

                <Col className="gutter-row" span={width > 767 ? 12 : 24}>
                    <RecentContainer>
                        <PreviewTextContainer>
                            <RecentText>
                                {t("food-recent")}
                            </RecentText>
                        </PreviewTextContainer>
                        <RecentTable
                            entity={"product"}
                            data={data?.top5Items?.map((item, index) => ({ ...item, key: index + 1 }))}
                            dataTableColumns={productColumns}
                            loading={loading}
                        />
                    </RecentContainer>
                </Col>
            </Row>
        </DashboardLayout>
    );
}