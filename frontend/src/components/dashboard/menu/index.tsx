import React from 'react'
import { Menu, MenuProps } from 'antd'
import { AppstoreOutlined, GiftOutlined, SettingOutlined, ShopOutlined, SolutionOutlined } from '@ant-design/icons'
import Link from 'next/link'
import { useTranslation } from 'react-i18next'

type MenuItem = Required<MenuProps>['items'][number];

export default function MenuComponent() {
    const { t } = useTranslation();

    const items: MenuItem[] = [
        {
            label: (<Link href="/management/dashboard">{t("admin-management")}</Link>),
            key: '1',
        },
        {
            label: (<Link href="/management/store/general">{t("store-management")}</Link>),
            key: '2',
        },
        {
            label: (<Link href="/management/employee/general">{t("staff-management")}</Link>),
            key: '3'
        },
        {
            label: t("restaurant-management"),
            key: 'sub1',
            icon: <SolutionOutlined />,
            children: [
                {
                    label: (<Link href="/management/reservation/general">{t("reservation-management")}</Link>),
                    key: '4'
                },
                {
                    label: (<Link href="/management/item/general">{t("food-management")}</Link>),
                    key: '5'
                },
                {
                    label: (<Link href="/management/category/general">{t("category-management")}</Link>),
                    key: '6'
                },
                {
                    label: (<Link href="/management/menu/general">{t("menu-management")}</Link>),
                    key: '7'
                },
            ],
        },
        {
            label: t("discount-management"),
            key: 'sub2',
            icon: <GiftOutlined />,
            children: [
                {
                    label: (<Link href="/management/discount/general">{t("item-discount-list")}</Link>),
                    key: '8'
                },
                {
                    label: (<Link href="/management/discountType/general">{t("discount-list")}</Link>),
                    key: '9'
                },
            ],
        },
        {
            label: t("order-management"),
            key: 'sub3',
            icon: <AppstoreOutlined />,
            children: [
                {
                    label: (<Link href="/management/order/general">{t("order-list")}</Link>),
                    key: '10'
                },
                {
                    label: (<Link href="/management/payment-history/general">{t("payment-history")}</Link>),
                    key: '11'
                },
            ],
        },
        {
            label: t("options"),
            key: 'sub4',
            icon: <SettingOutlined />,
            children: [
                {
                    label: t("currency"),
                    key: '12'
                },
                {
                    label: t("gps"),
                    key: '13'
                },
                {
                    label: (<Link href="/management/role/general">{t("role")}</Link>),
                    key: '14'
                },
            ],
        },
    ];

    return (
        <Menu
            style={{ textAlign: 'left' }}
            defaultSelectedKeys={['1']}
            mode="inline"
            items={items}
        />
    )
}
