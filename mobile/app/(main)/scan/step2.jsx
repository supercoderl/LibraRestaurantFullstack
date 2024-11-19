import { yupResolver } from '@hookform/resolvers/yup'
import { Button, TextField } from 'components'
import { Status } from 'enums'
import { useState } from 'react'
import { useForm } from 'react-hook-form'
import { useTranslation } from 'react-i18next'
import { Text, View } from 'react-native'
import { Dialog } from 'react-native-paper'
import { getCombinedSchema, mobileSchema, nameSchema } from 'utils'
import * as Yup from 'yup'

export const Step2 = props => {
  const { status, handleClose, onSubmit } = props
  const [loading, setLoading] = useState(false)

  //? Assets
  const { t } = useTranslation()

  //? Form Hook
  const {
    handleSubmit,
    formState: { errors: formErrors },
    control,
  } = useForm({
    resolver: yupResolver(getCombinedSchema(status === Status.Available)),
    defaultValues: { name: '', mobile: '' },
  })

  return (
    <>
      <Dialog.Icon icon="information-outline" size={50} />
      <Dialog.Title className="text-center font-medium">{t('cus-info')}</Dialog.Title>
      <Dialog.Content>
        <Text className="text-center text-gray-500">
          {t('reservation-status')}: {Status[status]}
        </Text>

        <View className="mt-3">
          {status === Status.Available && (
            <TextField
              errors={formErrors.name}
              placeholder={t('cus-name')}
              name="name"
              keyboardType="default"
              autoCapitalize="none"
              control={control}
              style={{ marginTop: 5 }}
              label={t('cus-name')}
            />
          )}

          <TextField
            errors={formErrors.mobile}
            placeholder={t('cus-phone')}
            name="mobile"
            keyboardType="numeric"
            autoCapitalize="none"
            control={control}
            style={{ marginTop: 5 }}
            label={t('cus-phone')}
          />
        </View>
      </Dialog.Content>
      <Dialog.Actions className="items-center justify-center">
        <Button onPress={handleClose} isOutlined className="flex-1">
          {t('back')}
        </Button>
        <Button onPress={handleSubmit(onSubmit)} className="flex-1" isLoading={loading}>
          {t('submit')}
        </Button>
      </Dialog.Actions>
    </>
  )
}
