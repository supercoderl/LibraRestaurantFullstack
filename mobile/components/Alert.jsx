import { Dialog } from 'react-native-paper'
import { Text } from 'react-native'
import { Button } from './common/Buttons'
import { useTranslation } from 'react-i18next'

export default function Alert(props) {
    //? Props
    const { title, description, icon, open, handleClose, handleSubmit } = props

    //? Assets
    const { t } = useTranslation()

    //? Re-Renders
    return (
        <Dialog visible={open} onDismiss={handleClose}>
            <Dialog.Icon icon={icon} />
            <Dialog.Title className="text-center">{title}</Dialog.Title>
            <Dialog.Content>
                <Text variant="bodyMedium">{description}</Text>
            </Dialog.Content>
            <Dialog.Actions className="items-center justify-center">
                <Button onPress={handleClose} isOutlined>{t('cancel')}</Button>
                <Button onPress={handleSubmit}>{t('submit')}</Button>
            </Dialog.Actions>
        </Dialog>
    )
}