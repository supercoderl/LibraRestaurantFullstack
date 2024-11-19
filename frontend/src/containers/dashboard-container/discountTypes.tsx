import { DashboardLayout } from "@/layouts/DashboardLayout"
import { Button, Checkbox, CheckboxProps, DatePicker, DatePickerProps, Divider, Input, Select, Table, TableColumnsType, Tag, Tooltip, Image } from "antd";
import { ActionContainer, AlignContainer, HeaderText, TableContainer, ToolbarContainer } from "./style";
import { DeleteOutlined, EditOutlined, EyeOutlined, PlusOutlined, ReloadOutlined, RollbackOutlined } from "@ant-design/icons";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { ListRep } from "@/type/objectTypes";
import { useState } from "react";
import { MobileTable } from "@/components/mobile/tables/mobile-table";
import { useRouter } from "next/navigation";
import { TFunction } from "i18next";
import { DiscountType } from "@/type/DiscountType";
import { dateFormatter, formatDate } from "@/utils/date";

type HeaderProps = {
    isShowText?: boolean;
    t: TFunction<"translation", undefined>;
}

const Header: React.FC<HeaderProps> = ({ isShowText, t }) => {
    const router = useRouter();

    return (
        <ToolbarContainer $isRow={true}>
            <HeaderText>{t("discount-type-management-full")}</HeaderText>
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

                <Checkbox onChange={onChangeCheckbox}>{t("discount-type-deleted")}</Checkbox>

                <Button>{t("filter")}</Button>

                <Button type="primary">{t("reset")}</Button>

                <Button type="primary" danger icon={<PlusOutlined />} href="create">{t("create")}</Button>
            </AlignContainer>

            <Divider type="vertical" />

            <AlignContainer>
                <Button type="primary" icon={<ReloadOutlined />} onClick={onReload}>{t("reload")}</Button>

                <Search placeholder={t("search")} onSearch={onSearch} />
            </AlignContainer>
        </ToolbarContainer>
    )
}

type DiscountTypeProps = {
    result?: ListRep | null;
    loading: boolean;
    onReload?: () => void;
    onPaginationChange?: (page: number, pageSize: number) => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

export const DiscountTypeContainer: React.FC<DiscountTypeProps> = ({ result, loading, onReload, onPaginationChange, onSearch, t }) => {
    const columns: TableColumnsType<DiscountType> = [
        {
            title: t("discount-type-name"),
            dataIndex: 'name',
            render: (text: string) => <a>{text}</a>,
        },
        {
            title: t("discount-value"),
            dataIndex: 'value',
        },
        {
            title: t("counpon-code"),
            dataIndex: 'counponCode',
        },
        {
            title: t("start-time"),
            dataIndex: 'startTime',
            render: (date: Date) => <p>{dateFormatter(date)}</p>
        },
        {
            title: t("end-time"),
            dataIndex: 'endTime',
            render: (date: Date) => <p>{dateFormatter(date)}</p>
        },
        {
            title: t("actions"),
            dataIndex: '',
            key: 'x',
            width: '10%',
            align: 'center',
            render: (row: DiscountType) => (
                <ActionContainer>
                    <Tooltip title={t("edit")}>
                        <Button
                            icon={<EditOutlined />}
                            type="link"
                            danger
                            href={`edit?discountTypeId=${row.discountTypeId}`}
                        />
                    </Tooltip>
                    <Tooltip title={t("delete")}>
                        <Button
                            icon={<DeleteOutlined />}
                            type="link"
                            danger
                        />
                    </Tooltip>
                </ActionContainer>
            ),
        }
    ];

    const rowSelection = {
        onChange: (selectedRowKeys: React.Key[], selectedRows: DiscountType[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        getCheckboxProps: (record: DiscountType) => ({
            name: record.name,
        }),
    };

    const [isOpen, setIsOpen] = useState(false);
    const [itemSelected, setItemSelected] = useState<DiscountType | null>(null);
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
                            rowKey={(record) => record.discountTypeId}
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
                            title={item.name}
                            subTitle={item.isActive ? t("active") : t("blocked")}
                            description={item.description}
                            image="https://cdn-icons-png.flaticon.com/512/11618/11618642.png"
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

