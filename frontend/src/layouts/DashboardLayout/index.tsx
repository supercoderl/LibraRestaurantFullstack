import { Layout, Divider, Button } from 'antd'
import DashboardFooter from '@/components/dashboard/footer'
import DashboardHeader from '@/components/dashboard/header'
import MenuComponent from '@/components/dashboard/menu'
import { BrandText, Logo, LogoContainer } from './style'
import { SpanLogo } from '@/components/title'
import useWindowDimensions from '@/hooks/use-window-dimensions'
import logo from "../../../public/assets/images/logo/logo-removebg-preview.png"
import { useState } from 'react'
import { TFunction } from 'i18next'

const theme = 'light'

const { Header, Footer, Sider, Content } = Layout;

type DashboardProps = {
    children?: React.ReactNode;
    t: TFunction<"translation", undefined>
}

export const DashboardLayout: React.FC<DashboardProps> = ({ children, t }) => {

    const { width } = useWindowDimensions();
    const [posLeft, setPosLeft] = useState('-100%');

    return (
        <Layout>
            <Sider
                style={{
                    overflow: 'auto',
                    height: '100vh',
                    position: width > 767 ? 'fixed' : 'absolute',
                    left: width > 767 ? 0 : posLeft,
                    textAlign: "center",
                }}
                collapsible={width <= 767}
                collapsed={false}
                onCollapse={() => setPosLeft('-100%')}
                theme={theme}
            >
                <LogoContainer>
                    <Logo src={logo.src} alt='logo' />
                    <BrandText>Libra<SpanLogo>Restaurant</SpanLogo></BrandText>
                </LogoContainer>
                <Divider />
                <MenuComponent />
            </Sider>
            <Layout style={{ marginLeft: width > 767 ? 200 : 0 }}>
                <Header style={{ padding: 0 }}>
                    <DashboardHeader t={t} isShowButton={width <= 767} onMenuClick={() => setPosLeft('0')} />
                </Header>
                <Content style={{ margin: '24px 16px 0', overflow: 'initial' }}>
                    {children}
                </Content>
                <Footer style={{ padding: 15 }}>
                    <DashboardFooter />
                </Footer>
            </Layout>
        </Layout>
    )
}