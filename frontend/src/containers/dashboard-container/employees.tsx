import { DashboardLayout } from "@/layouts/DashboardLayout"
import { Button, Checkbox, CheckboxProps, DatePicker, DatePickerProps, Divider, Input, Modal, Popconfirm, Select, SelectProps, Table, TableColumnsType, Tag, Tooltip, Typography } from "antd";
import { ActionContainer, AlignContainer, HeaderText, TableContainer, ToolbarContainer } from "./style";
import { EditOutlined, LockOutlined, PlusOutlined, ReloadOutlined, SisternodeOutlined, UnlockOutlined } from "@ant-design/icons";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { ListRep } from "@/type/objectTypes";
import { useState } from "react";
import { Employee } from "@/type/Employee";
import { UserStatus } from "@/enums";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { assign } from "@/api/business/roleApi";
import { toast } from "react-toastify";
import { MobileTable } from "@/components/mobile/tables/mobile-table";
import { TFunction } from "i18next";
import { updateEmployeeData } from "@/redux/slices/employee-slice";

type HeaderProps = {
    isShowText?: boolean;
    t: TFunction<"translation", undefined>
}

const Header: React.FC<HeaderProps> = ({ isShowText, t }) => {
    return (
        <ToolbarContainer $isRow={true}>
            <HeaderText>{t("employee-management-full")}</HeaderText>
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

                <Checkbox onChange={onChangeCheckbox}>{t("employee-deleted")}</Checkbox>

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

type EmployeeProps = {
    result?: ListRep | null;
    loading: boolean;
    onReload?: () => void;
    onPaginationChange?: (page: number, pageSize: number) => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

export const EmployeeContainer: React.FC<EmployeeProps> = ({ result, loading, onReload, onPaginationChange, onSearch, t }) => {
    const columns: TableColumnsType<Employee> = [
        {
            title: t("employee-name"),
            dataIndex: 'fullName',
            render: (text: string) => <a>{text}</a>,
        },
        {
            title: t("store"),
            dataIndex: 'storeName',
        },
        {
            title: t("email"),
            dataIndex: 'email',
        },
        {
            title: t("phone"),
            dataIndex: 'mobile',
        },
        {
            title: t("status"),
            dataIndex: 'status',
            width: 100,
            align: 'center',
            render: (status: number) => status === UserStatus.Active ? <Tag color="success">{t("active")}</Tag> : <Tag color="error">{t("blocked")}</Tag>
        },
        {
            title: t("actions"),
            dataIndex: '',
            key: 'x',
            width: '10%',
            align: 'center',
            render: (row: Employee) => (
                <ActionContainer>
                    <Tooltip title={t("permission")}>
                        <Button
                            icon={<SisternodeOutlined />}
                            type="link"
                            danger
                            onClick={() => {
                                setIsOpen(true);
                                setEmployeeSeleted(row);
                                setRolesSelected(row.roleIds || [])
                            }}
                        />
                    </Tooltip>
                    <Tooltip title={t("edit")}>
                        <Button
                            icon={<EditOutlined />}
                            type="link"
                            danger
                            href={`edit?employeeId=${row.id}`}
                        />
                    </Tooltip>
                    <Tooltip title={row.status === UserStatus.Active ? t("block") : "Mở khóa"}>
                        <Popconfirm
                            title={row.status === UserStatus.Active ? "Khóa tài khoản" : "Mở khóa tài khoản"}
                            description={`Bạn có chắc muốn ${row.status === UserStatus.Active ? "khóa" : "mở khóa"} tài khoản nhân viên này?`}
                            okText="Đồng ý"
                            cancelText="Không"
                            okButtonProps={{ loading: employeeLoading }}
                            onConfirm={() => {
                                const body = { ...row, status: row.status === UserStatus.Active ? UserStatus.InActive : UserStatus.Active }
                                dispatch(updateEmployeeData(body)).then(onReload);
                            }}
                        >
                            <Button
                                icon={row.status === UserStatus.Active ? <LockOutlined /> : <UnlockOutlined />}
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
        onChange: (selectedRowKeys: React.Key[], selectedRows: Employee[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        getCheckboxProps: (record: Employee) => ({
            name: record.firstName,
        }),
    };

    const handleChange = (value: number[]) => {
        setRolesSelected(value);
    };

    const handleSubmit = async () => {
        if (!employeeSeleted) {
            toast("Chưa có nhân viên nào được chọn để phân quyền!", { type: "warning" });
            return;
        }
        if (rolesSelected.length <= 0) {
            toast("Chưa chọn quyền!", { type: "warning" });
            return;
        }

        const body = {
            employeeId: employeeSeleted?.id,
            roleIds: rolesSelected
        };

        try {
            const res = await assign(body);

            if (res && res.success) {
                toast("Phân quyền thành công", {
                    type: "success"
                });
            }
            else {
                toast("Phân quyền thất bại", {
                    type: "error"
                });
            }
        }
        catch (error) {
            toast("Lỗi xảy ra", {
                type: "error"
            });
            console.log(error);
        }
        finally {
            setEmployeeSeleted(null);
            setRolesSelected([]);
            setIsOpen(false);
            onReload && onReload();
        }
    }

    const [isOpen, setIsOpen] = useState(false);
    const [rolesSelected, setRolesSelected] = useState<number[]>([]);
    const [employeeSeleted, setEmployeeSeleted] = useState<Employee | null>(null);
    const { width } = useWindowDimensions();
    const { Text } = Typography;
    const { roles, employeeLoading } = useStoreSelector(state => ({
        roles: state.mainRoleSlice.roles,
        employeeLoading: state.mainEmployeeSlice.loading
    }));
    const dispatch = useStoreDispatch();

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
                            rowKey={(record) => record.id}
                            columns={columns}
                            dataSource={result?.items}
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
                            title={item.fullName}
                            subTitle={item.email}
                            description={item.status === 0 ? t("active") : t("blocked")}
                            image="https://cdn-icons-png.flaticon.com/512/11264/11264000.png"
                        />
                    ))
            }
            <Modal title={t("permission")} open={isOpen} onOk={handleSubmit} onCancel={() => setIsOpen(false)}>
                <Text>{t("select-permission-for")} {employeeSeleted?.fullName}. {t("access-3-permission")}</Text>
                <Select
                    mode="multiple"
                    allowClear
                    style={{ width: '100%', marginTop: 10 }}
                    placeholder={t("choose-permission")}
                    onChange={handleChange}
                    value={rolesSelected}
                    options={roles.map(role => ({ value: role.roleId, label: role.name }))}
                />
            </Modal>
        </DashboardLayout>
    )
}

