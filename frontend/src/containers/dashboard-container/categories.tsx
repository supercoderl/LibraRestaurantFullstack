import { DashboardLayout } from "@/layouts/DashboardLayout"
import { Button, Checkbox, CheckboxProps, DatePicker, DatePickerProps, Divider, Input, Select, Table, TableColumnsType, Tag, Tooltip, Image, Popconfirm } from "antd";
import { ActionContainer, AlignContainer, HeaderText, TableContainer, ToolbarContainer } from "./style";
import { DeleteOutlined, EditOutlined, EyeOutlined, PlusOutlined, ReloadOutlined, RollbackOutlined } from "@ant-design/icons";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { ListRep } from "@/type/objectTypes";
import { useState } from "react";
import Category from "@/type/Category";
import { MobileTable } from "@/components/mobile/tables/mobile-table";
import { useRouter } from "next/navigation";
import { TFunction } from "i18next";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { deleteCategoryAsync } from "@/redux/slices/categories-slice";

type HeaderProps = {
    isShowText?: boolean;
    t: TFunction<"translation", undefined>;
}

const Header: React.FC<HeaderProps> = ({ isShowText, t }) => {
    const router = useRouter();

    return (
        <ToolbarContainer $isRow={true}>
            <HeaderText>{t("category-management-full")}</HeaderText>
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

                <Checkbox onChange={onChangeCheckbox}>{t("category-deleted")}</Checkbox>

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

type CategoryProps = {
    result?: ListRep | null;
    loading: boolean;
    onReload?: () => void;
    onPaginationChange?: (page: number, pageSize: number) => void;
    onSearch?: (text: string) => void;
    t: TFunction<"translation", undefined>
}

export const CategoryContainer: React.FC<CategoryProps> = ({ result, loading, onReload, onPaginationChange, onSearch, t }) => {
    const columns: TableColumnsType<Category> = [
        {
            title: t("picture"),
            dataIndex: 'picture',
            align: 'center',
            width: '5%',
            render: (url?: string | null) => <Image width={80} src={url || process.env.NEXT_PUBLIC_DUMMY_PICTURE} />
        },
        {
            title: t("category-name"),
            dataIndex: 'name',
            render: (text: string) => <a>{text}</a>,
        },
        {
            title: t("status"),
            dataIndex: 'isActive',
            width: 100,
            align: 'center',
            render: (isActive: boolean) => isActive ? <Tag color="success">{t("active")}</Tag> : <Tag color="error">{t("blocked")}</Tag>
        },
        {
            title: t("actions"),
            dataIndex: '',
            key: 'x',
            width: '10%',
            align: 'center',
            render: (row: Category) => (
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
                    <Tooltip title={t("edit")}>
                        <Button
                            icon={<EditOutlined />}
                            type="link"
                            danger
                            href={`edit?categoryId=${row.categoryId}`}
                        />
                    </Tooltip>
                    <Tooltip title={t("delete")}>
                        <Popconfirm
                            title="Xóa danh mục"
                            description="Bạn có chắc muốn xóa danh mục này?"
                            okText="Đồng ý"
                            cancelText="Không"
                            okButtonProps={{ loading: categoryLoading }}
                            onConfirm={() => dispatch(deleteCategoryAsync(row.categoryId)).then(onReload)}
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
        onChange: (selectedRowKeys: React.Key[], selectedRows: Category[]) => {
            console.log(`selectedRowKeys: ${selectedRowKeys}`, 'selectedRows: ', selectedRows);
        },
        getCheckboxProps: (record: Category) => ({
            name: record.name,
        }),
    };

    const [isOpen, setIsOpen] = useState(false);
    const [itemSelected, setItemSelected] = useState<Category | null>(null);
    const { width } = useWindowDimensions();
    const { categoryLoading } = useStoreSelector(state => ({
        categoryLoading: state.mainCategorySlice.loading
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
                            columns={columns}
                            dataSource={result?.items}
                            rowKey={(record) => record.categoryId}
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

