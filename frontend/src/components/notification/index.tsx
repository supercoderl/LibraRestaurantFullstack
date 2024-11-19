import { Notification } from "@/type/Notification"
import { CloseOutlined, CreditCardOutlined, LoadingOutlined, MailOutlined, ShoppingOutlined, WarningOutlined } from "@ant-design/icons";
import { Alert, Button, Card } from "antd"
import React, { useEffect, useRef } from "react";
import { MainText, Tag, TitleContainer } from "./style";
import { fetchNotifications, updateNotificationStatus } from "@/redux/slices/message-slice";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";

type NotificationProps = {
    notifications: Notification[];
}

type NotificationItemProps = {
    icon: React.ReactNode;
    title: string;
    type: "info" | "success" | "warning" | "error";
}

const notificationIcon = (type: string): NotificationItemProps => {
    switch (type) {
        case "order":
            return {
                icon: <ShoppingOutlined />,
                title: "Khách đặt món",
                type: "info"
            }
        case "message":
            return {
                icon: <MailOutlined />,
                title: "Tin nhắn mới",
                type: "success"
            }
        case "pay":
            return {
                icon: <CreditCardOutlined />,
                title: "Yêu cầu thanh toán",
                type: "error"
            }
        default:
            return {
                icon: <WarningOutlined />,
                title: "Cảnh báo",
                type: "warning"
            }
    }
}

const title = (text: string, isRead: boolean, loading: boolean) => {
    return (
        <TitleContainer>
            <MainText>{text}</MainText>
            <Tag style={{ backgroundColor: isRead ? "#C65BCF" : "#DC5F00" }}>
                {
                    loading ?
                        <LoadingOutlined />
                        :
                        isRead ? "đã xác nhận" : "chưa xác nhận"
                }
            </Tag>
        </TitleContainer>
    )
}

export const NotificationCard: React.FC<NotificationProps> = ({ notifications }) => {
    const dispatch = useStoreDispatch();
    const { loadingMessageId, loading, hasMore, size } = useStoreSelector(state => state.mainNotificationSlice);
    const cardRef = useRef<HTMLDivElement | null>(null);

    const handleClick = async (notification: Notification) => {
        dispatch(updateNotificationStatus(notification));
    }

    const handleScroll = () => {
        if (cardRef.current) {
            const { scrollTop, clientHeight, scrollHeight } = cardRef.current;
            if (scrollTop + clientHeight >= scrollHeight - 10) { // Giảm sai số với `-10`
                if (!loading && hasMore) {
                    dispatch(fetchNotifications({ type: "order", pageSize: size }));
                }
            }
        }
    };

    useEffect(() => {
        const cardElement = cardRef.current;
        if (cardElement) {
            cardElement.addEventListener('scroll', handleScroll);
        }
        return () => {
            if (cardElement) {
                cardElement.removeEventListener('scroll', handleScroll);
            }
        };
    }, [loading, hasMore]);

    return (
        <>
            <Card
                ref={cardRef}
                style={{ maxHeight: "30vw", minWidth: "25vw", overflow: "auto", boxShadow: "rgba(99, 99, 99, 0.2) 0px 2px 8px 0px" }}
                className='custom-alert-card'
            >
                {notifications.map(notification => (
                    <div key={notification?.messageId} style={{ paddingTop: "0.5rem" }}>
                        <Alert
                            onClick={() => handleClick(notification)}
                            message={title(notificationIcon(notification.messageType).title, notification.isRead, loadingMessageId === notification?.messageId)}
                            description={notification.content}
                            showIcon
                            type={notificationIcon(notification.messageType).type}
                            icon={notificationIcon(notification.messageType).icon}
                            onClose={() => console.log("asd")}
                            className={`custom-alert-notification`}
                            action={
                                <Button icon={<CloseOutlined style={{ color: "rgba(0, 0, 0, 0.4)" }} />} type="text" size="small" />
                            }
                        />
                    </div>
                ))}
                {loading && <div style={{ textAlign: 'center', padding: '1rem' }}>Loading...</div>}
            </Card>
        </>
    )
}