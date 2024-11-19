import React from "react";
import { Dropdown, Menu, Table } from "antd";

import {
    EllipsisOutlined,
    EyeOutlined,
    EditOutlined,
    DeleteOutlined,
} from "@ant-design/icons";

function DropDownRowMenu() {
    const Show = () => { };
    function Edit() { }
    function Delete() { }
    return (
        <Menu style={{ width: 130 }}>
            <Menu.Item icon={<EyeOutlined />} onClick={Show}>
                Show
            </Menu.Item>
            <Menu.Item icon={<EditOutlined />} onClick={Edit}>
                Edit
            </Menu.Item>
            <Menu.Item icon={<DeleteOutlined />} onClick={Delete}>
                Delete
            </Menu.Item>
        </Menu>
    );
}

export default function RecentTable({ ...props }) {
    let { entity, dataTableColumns, data, loading } = props;
    dataTableColumns = [
        ...dataTableColumns,
        {
            title: "",
            render: () => (
                <Dropdown trigger={["click"]}>
                    <EllipsisOutlined style={{ cursor: "pointer", fontSize: "24px" }} />
                </Dropdown>
            ),
        },
    ];

    return (
        <>
            <Table
                columns={dataTableColumns}
                rowKey={(item) => item.key}
                dataSource={data}
                pagination={false}
                loading={loading}
            />
        </>
    );
}