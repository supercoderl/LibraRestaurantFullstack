import { DashboardLayout } from "@/layouts/DashboardLayout"
import { Button, Checkbox, CheckboxProps, DatePicker, DatePickerProps, Divider, Image, Input, Select, Table, TableColumnsType, Tag, Tooltip } from "antd";
import { ActionContainer, AlignContainer, HeaderText, TableContainer, ToolbarContainer } from "./style";
import { DeleteOutlined, EditOutlined, EyeOutlined, PlusOutlined, ReloadOutlined, RollbackOutlined } from "@ant-design/icons";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { ListRep } from "@/type/objectTypes";
import { useState } from "react";
import { MobileTable } from "@/components/mobile/tables/mobile-table";
import { useRouter } from "next/navigation";
import { PaymentHistory } from "@/type/PaymentHistory";
import { TFunction } from "i18next";

type HeaderProps = {
    isShowText?: boolean;
    t: TFunction<"translation", undefined>
}

const Header: React.FC<HeaderProps> = ({ isShowText, t }) => {
    const router = useRouter();
    return (
        <ToolbarContainer $isRow={true}>
            <HeaderText>{t("payment-history")}</HeaderText>
            <Button
                icon={<RollbackOutlined />}
                type="primary"
                danger
                onClick={() => router.back()}
            >
                {isShowText && t("back")}
            </Button>
        </ToolbarContainer>
    )
}

type ToolbarProps = {
    isRow?: boolean;
    onReload?: () => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

const Toolbar: React.FC<ToolbarProps> = ({ isRow, onReload, onSearch, t }) => {
    const onChangeDate: DatePickerProps['onChange'] = (date, dateString) => {
        console.log(date, dateString);
    };

    const onChangeSelect = (value: string) => {
        console.log(`selected ${value}`);
    };

    const onChangeCheckbox: CheckboxProps['onChange'] = (e) => {
        console.log(`checked = ${e.target.checked}`);
    };

    const { Search } = Input;

    return (
        <ToolbarContainer $isRow={isRow}>
            <AlignContainer>
                <DatePicker placeholder={t("update-at")} onChange={onChangeDate} />

                <Select
                    showSearch
                    placeholder={t("filter-by")}
                    optionFilterProp="label"
                    onChange={onChangeSelect}
                    onSearch={onSearch}
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

                <Button>{t("filter")}</Button>

                <Button type="primary">{t("reset")}</Button>
            </AlignContainer>

            <Divider type="vertical" />

            <AlignContainer>
                <Button type="primary" icon={<ReloadOutlined />} onClick={onReload}>{t("reload")}</Button>

                <Search placeholder={t("search")} onSearch={onSearch} />
            </AlignContainer>
        </ToolbarContainer>
    )
}

type PaymentHistoryProps = {
    result?: ListRep | null;
    loading: boolean;
    onReload?: () => void;
    onPaginationChange?: (page: number, pageSize: number) => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

export const PaymentHistoryContainer: React.FC<PaymentHistoryProps> = ({ result, loading, onReload, onPaginationChange, onSearch, t }) => {
    const columns: TableColumnsType<PaymentHistory> = [
        {
            title: t("transactionId"),
            dataIndex: 'transactionId',
            render: (text: string) => <a>{text}</a>,
        },
        {
            title: t("payment-method"),
            dataIndex: 'paymentMethodName',
        },
        {
            title: t("total"),
            dataIndex: 'amount',
        },
        // {
        //     title: 'Mã tiền tệ',
        //     dataIndex: 'currencyName',
        // },
        {
            title: t("status"),
            dataIndex: 'status',
            width: 100,
            align: 'center',
            render: (status: number) => status === 0 ? <Tag color="success">{t("success")}</Tag> : <Tag color="error">{t("failed")}</Tag>
        },
        {
            title: t("actions"),
            dataIndex: '',
            key: 'x',
            width: '10%',
            align: 'center',
            render: (row: PaymentHistory) => (
                <ActionContainer>
                    <Tooltip title={t("see")}>
                        <Button
                            icon={<EyeOutlined />}
                            type="link"
                            danger
                            onClick={() => {
                                setIsOpen(true);
                                setItemSelected(row);
                            }}
                        />
                    </Tooltip>
                </ActionContainer>
            ),
        }
    ];

    const rowSelection = {
        onChange: (selectedRowKeys: React.Key[], selectedRows: PaymentHistory[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        getCheckboxProps: (record: PaymentHistory) => ({
            name: record.transactionId,
        }),
    };

    const [isOpen, setIsOpen] = useState(false);
    const [itemSelected, setItemSelected] = useState<PaymentHistory | null>(null);
    const { width } = useWindowDimensions();

    return (
        <DashboardLayout t={t}>
            <Header t={t} isShowText={width > 767} />
            <Toolbar t={t} isRow={width > 767} onReload={onReload} onSearch={onSearch} />
            {
                width > 767 ?
                    <TableContainer>
                        <Table
                            bordered
                            rowSelection={{
                                type: 'checkbox',
                                ...rowSelection,
                            }}
                            columns={columns}
                            dataSource={result?.items}
                            rowKey={(record) => record.paymentHistoryId}
                            style={{ borderRadius: 0 }}
                            loading={loading}
                            pagination={{ pageSize: result?.pageSize, total: result?.count, onChange: onPaginationChange }}
                        />
                    </TableContainer>
                    :
                    result && result.items &&
                    result.items.map((item, index) => (
                        <MobileTable
                            key={index}
                            title={item.transactionId}
                            subTitle={item.status === 0 ? t("success") : t("failed")}
                            description={item.amount}
                            image="https://cdn-icons-png.flaticon.com/512/9557/9557988.png"
                        />
                    ))
            }
            {/* <ItemDetail
                isOpen={isOpen}
                handleCancel={() => {
                    setIsOpen(false);
                    setItemSelected(null);
                }}
                item={itemSelected}
            /> */}
        </DashboardLayout>
    )
}

