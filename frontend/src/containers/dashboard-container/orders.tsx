import { DashboardLayout } from "@/layouts/DashboardLayout"
import { Button, Checkbox, CheckboxProps, DatePicker, DatePickerProps, Divider, Input, Modal, Select, Table, TableColumnsType, Tag, Tooltip } from "antd";
import { ActionContainer, AlignContainer, HeaderText, TableContainer, ToolbarContainer } from "./style";
import { CheckOutlined, EyeOutlined, FileDoneOutlined, PlusOutlined, ReloadOutlined, RollbackOutlined } from "@ant-design/icons";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { ListRep } from "@/type/objectTypes";
import { useState } from "react";
import { Order } from "@/type/Order";
import { getOrderStatus } from "@/utils/status";
import { Invoice } from "@/components/invoice";
import { MobileTable } from "@/components/mobile/tables/mobile-table";
import { useRouter } from "next/navigation";
import { OrderStatus } from "@/enums";
import { actionOrder } from "@/api/business/orderApi";
import { toast } from "react-toastify";
import { TFunction } from "i18next";
import { OrderLine } from "@/type/OrderLine";
import { OrderLog } from "@/type/OrderLog";

type HeaderProps = {
    isShowText?: boolean;
    t: TFunction<"translation", undefined>
}

const Header: React.FC<HeaderProps> = ({ isShowText, t }) => {
    const router = useRouter();
    return (
        <ToolbarContainer $isRow={true}>
            <HeaderText>{t("order-management-full")}</HeaderText>
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

                <Checkbox onChange={onChangeCheckbox}>{t("order-deleted")}</Checkbox>

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

type OrderProps = {
    result?: ListRep | null;
    loading: boolean;
    onReload: () => void;
    onPaginationChange?: (page: number, pageSize: number) => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

export const OrderContainer: React.FC<OrderProps> = ({ result, loading, onReload, onPaginationChange, onSearch, t }) => {
    const [showModal, setShowModal] = useState(false);

    const mergedData = (orderLines: OrderLine[], orderLogs: OrderLog[]) => {
        return orderLines.map(line => {
            // Tìm log tương ứng với ItemId
            const log = orderLogs.find(log => log.itemId === line.itemId);

            return {
                itemId: line.itemId,
                foodName: line.foodName,
                quantityOrder: line.quantity,
                quantityChanges: log && log.quantityChanges,
                timeChanges: log && log.timeChanges
            };
        });
    };

    const expandColumns: TableColumnsType<any> = [
        { title: t("food-name"), dataIndex: 'foodName' },
        { title: t("quantity"), dataIndex: 'quantityOrder' },
        { title: t("quantity-call"), dataIndex: 'quantityChanges' },
        { title: t("time-call"), dataIndex: 'timeChanges' }
    ];

    const expandedRowRender = (record: Order) => (
        <Table
            columns={expandColumns}
            dataSource={mergedData(record.orderLines, record.orderLogs)}
            pagination={false}
            bordered
        />
    );

    const columns: TableColumnsType<Order> = [
        {
            title: t("bill-no"),
            dataIndex: 'orderNo',
            render: (text: string) => <a>{text}</a>,
        },
        {
            title: t("customer"),
            dataIndex: 'customerName',
        },
        {
            title: t("store"),
            dataIndex: 'storeName',
        },
        {
            title: t("table-number"),
            dataIndex: 'reservationId',
        },
        {
            title: t("total"),
            dataIndex: 'total',
        },
        {
            title: t("status"),
            dataIndex: 'latestStatus',
            width: 100,
            align: 'center',
            render: (latestStatus: number) => <Tag color={getOrderStatus(latestStatus, t).color}>{getOrderStatus(latestStatus, t).title}</Tag>
        },
        {
            title: t("actions"),
            dataIndex: '',
            key: 'x',
            width: '10%',
            align: 'center',
            render: (row: Order) => (
                <ActionContainer>
                    {row.latestStatus === OrderStatus.Ready &&
                        <Tooltip title={t("submit-paid")}>
                            <Button
                                icon={<CheckOutlined />}
                                type="link"
                                danger
                                onClick={() => handleCheck(row)}
                            />
                        </Tooltip>}
                    <Tooltip title={t("see")}>
                        <Button
                            icon={<EyeOutlined />}
                            type="link"
                            danger
                            onClick={() => {
                                setIsOpen(true);
                                setShowModal(true);
                                setItemSelected(row);
                            }}
                        />
                    </Tooltip>
                </ActionContainer>
            ),
        }
    ];

    const rowSelection = {
        onChange: (selectedRowKeys: React.Key[], selectedRows: Order[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        getCheckboxProps: (record: Order) => ({

        }),
    };

    const [isOpen, setIsOpen] = useState(false);
    const [itemSelected, setItemSelected] = useState<Order | null>(null);
    const { width } = useWindowDimensions();

    const handleCheck = async (order: Order) => {
        let body = {
            ...order,
            isPaid: true,
            isCompleted: true,
            isReady: false,
            latestStatus: OrderStatus.Paid,
            latestStatusUpdate: new Date(),
            action: "pay"
        };

        try {
            await actionOrder(body, "edit");
        }
        catch (err) {
            console.log(err);
        }
        finally {
            toast("Đã cập nhật xong.", { type: "success" });
            onReload();
        }
    }

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
                            expandable={{
                                expandedRowRender,
                                rowExpandable: record => record.orderLines?.length > 0,
                            }}
                            rowKey={(record) => record.orderId}
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
                            title={item.orderNo}
                            subTitle={item.customerName}
                            description={item.total}
                            image="https://cdn-icons-png.flaticon.com/512/3338/3338579.png"
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
            <Modal
                title={t("invoice")}
                centered
                open={showModal}
                onCancel={() => {
                    setShowModal(false);
                    setItemSelected(null);
                }}
                footer={null}
                width={700}
            >
                <Invoice t={t} order={itemSelected} />
            </Modal>
        </DashboardLayout>
    )
}

