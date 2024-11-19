import { DashboardLayout } from "@/layouts/DashboardLayout"
import { Button, Checkbox, CheckboxProps, DatePicker, DatePickerProps, Divider, Image, Input, Popconfirm, Select, Table, TableColumnsType, Tag, Tooltip } from "antd";
import { ActionContainer, AlignContainer, HeaderText, TableContainer, ToolbarContainer } from "./style";
import { DeleteOutlined, EditOutlined, EyeOutlined, LoadingOutlined, PlusOutlined, ReloadOutlined, RollbackOutlined } from "@ant-design/icons";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { ListRep } from "@/type/objectTypes";
import { useState } from "react";
import { Reservation } from "@/type/Reservation";
import { Status } from "@/enums";
import { MobileTable } from "@/components/mobile/tables/mobile-table";
import { useRouter } from "next/navigation";
import { TFunction } from "i18next";
import ClearIcon from "../../../public/assets/icons/clear-icon.svg";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { deleteReservationAsync, updateReservationAsync } from "@/redux/slices/reservation-slice";
import { toast } from "react-toastify";

type HeaderProps = {
    isShowText?: boolean;
    t: TFunction<"translation", undefined>
}

const Header: React.FC<HeaderProps> = ({ isShowText, t }) => {
    const router = useRouter();
    return (
        <ToolbarContainer $isRow={true}>
            <HeaderText>{t("reservation-management-full")}</HeaderText>
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

                <Checkbox onChange={onChangeCheckbox}>{t("reservation-deleted")}</Checkbox>

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

type ReservationProps = {
    result?: ListRep | null;
    loading: boolean;
    onReload: () => void;
    onPaginationChange?: (page: number, pageSize: number) => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

export const ReservationContainer: React.FC<ReservationProps> = ({ result, loading, onReload, onPaginationChange, onSearch, t }) => {
    const columns: TableColumnsType<Reservation> = [
        {
            title: t("qr-code"),
            dataIndex: 'qrCode',
            align: 'center',
            width: 80,
            render: (text: string) => <Image width={60} src={text} alt="qr" />
        },
        {
            title: t("table-number"),
            dataIndex: 'tableNumber',
            render: (text: number) => <a>{text}</a>,
        },
        {
            title: t("capacity"),
            dataIndex: 'capacity',
        },
        {
            title: t("store"),
            dataIndex: 'storeName'
        },
        {
            title: t("status"),
            dataIndex: 'status',
            width: 100,
            align: 'center',
            render: (status: number) => <Tag color="#0958d9">{Status[status as keyof typeof Status]}</Tag>
        },
        {
            title: t("actions"),
            dataIndex: '',
            key: 'x',
            width: '10%',
            align: 'center',
            render: (row: Reservation) => (
                <ActionContainer>
                    <Tooltip title={t("clean")}>
                        <Button
                            icon={(reservationLoading && reservationLoadingId === row.reservationId) ? <LoadingOutlined /> : <ClearIcon fill="#ff4d4f" />}
                            type="link"
                            disabled={reservationLoading && reservationLoadingId === row.reservationId}
                            danger
                            onClick={() => handleSubmitClean(row)}
                        />
                    </Tooltip>
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
                    <Tooltip title={t("edit")}>
                        <Button
                            icon={<EditOutlined />}
                            type="link"
                            danger
                            disabled={reservationLoading && reservationLoadingId === row.reservationId}
                            href={`edit?reservationId=${row.reservationId}`}
                        />
                    </Tooltip>
                    <Tooltip title={t("delete")}>
                        <Popconfirm
                            title="Xóa bàn"
                            description="Bạn có chắc muốn xóa bàn này?"
                            okText="Đồng ý"
                            disabled={reservationLoading && reservationLoadingId === row.reservationId}
                            cancelText="Không"
                            okButtonProps={{ loading: reservationLoading && reservationLoadingId === row.reservationId }}
                            onConfirm={() => dispatch(deleteReservationAsync(row.reservationId)).then(onReload)}
                        >
                            <Button
                                icon={<DeleteOutlined />}
                                type="link"
                                danger
                            />
                        </Popconfirm>
                    </Tooltip>
                </ActionContainer>
            ),
        }
    ];

    const rowSelection = {
        onChange: (selectedRowKeys: React.Key[], selectedRows: Reservation[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        getCheckboxProps: (record: Reservation) => ({
            name: record.storeId,
        }),
    };

    const [isOpen, setIsOpen] = useState(false);
    const [itemSelected, setItemSelected] = useState<Reservation | null>(null);
    const { width } = useWindowDimensions();
    const { reservationLoading, reservationLoadingId } = useStoreSelector(state => ({
        reservationLoading: state.reservation.loading,
        reservationLoadingId: state.reservation.loadingId
    }));

    const dispatch = useStoreDispatch();

    const handleSubmitClean = (reservation: Reservation) => {
        dispatch(updateReservationAsync({
            reservationId: reservation.reservationId,
            isChanged: false,
            capacity: reservation.capacity,
            storeId: reservation.storeId,
            tableNumber: reservation.tableNumber,
            status: Status.Available,
            customerName: "",
            customerPhone: ""
        })).then((res) => {
            if (updateReservationAsync.fulfilled.match(res)) {
                toast("Cập nhật bàn thành công", { type: "success" });
                onReload();
            }
        });
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
                            rowKey={(record) => record.reservationId}
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
                            title={`${t("table-no")} ${item.tableNumber}`}
                            subTitle={item.storeName}
                            description={String(Status[item.status as keyof typeof Status])}
                            image="https://cdn-icons-png.flaticon.com/512/11138/11138514.png"
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

