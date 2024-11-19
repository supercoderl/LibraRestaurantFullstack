import { Button } from "components";
import { Status } from "enums";
import { useTranslation } from "react-i18next";
import { Text } from "react-native";
import { Dialog } from "react-native-paper";

export const Step1 = (props) => {
    const { status, isChanged, handleClose, handleSubmit, tableNumber } = props;

    //Assets
    const { t } = useTranslation()

    const render = () => {
        switch (status) {
            case Status.Available:
                return {
                    title: isChanged ? `${t('table-change')} ${tableNumber}?` : t('table-empty'),
                    icon: "check-decagram-outline",
                    allowNext: true
                }
            case Status.Occupied:
                return {
                    title: "Bạn đang có món, bạn có muốn gọi thêm không?",
                    icon: "chili-medium-outline",
                    allowNext: true
                }
            case Status.Reserved:
                return {
                    title: "Bàn đã được đặt trước, vui lòng thông báo cho nhân viên nếu đó là bàn của bạn.",
                    icon: "clipboard-alert-outline",
                    allowNext: false
                }
            case Status.Cleaning:
                return {
                    title: "Bàn đang được dọn dẹp, vui lòng chờ trong ít phút.",
                    icon: "delete-clock-outline",
                    allowNext: false
                }
            case Status.OutOfService: {
                return {
                    title: "Bàn không phục vụ lúc này",
                    icon: "coffee-off-outline",
                    allowNext: false
                }
            }
        }
        return {
            title: t('error-occur'),
            icon: "virus-outline",
            allowNext: false
        }
    }

    return (
        <>
            <Dialog.Icon icon={render().icon} size={50} />
            <Dialog.Title className="text-center font-medium">{t('submit-reservation')}</Dialog.Title>
            <Dialog.Content>
                <Text variant="bodyMedium" className="text-center text-[16px] text-gray-500">{render().title}</Text>
            </Dialog.Content>
            <Dialog.Actions className="items-center justify-center">
                <Button
                    onPress={handleClose}
                    isOutlined
                    className="flex-1"
                >
                    {t('cancel')}
                </Button>
                {
                    render().allowNext &&
                    <Button
                        onPress={handleSubmit}
                        className="flex-1"
                    >
                        {t('submit')}
                    </Button>
                }
            </Dialog.Actions>
        </>
    )
}