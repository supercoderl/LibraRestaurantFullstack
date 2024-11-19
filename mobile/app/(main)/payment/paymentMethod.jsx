import { Button } from 'components';
import { CreditCard } from 'components/payments/credit-card';
import { StripeCard } from 'components/payments/stripe/card'
import { Stack, useLocalSearchParams } from 'expo-router'
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { KeyboardAvoidingView, ScrollView, Text, View } from 'react-native'

export default function PaymentMethodScreen(props) {
    //? Assets
    const { t } = useTranslation();

    //? Props
    const { method } = useLocalSearchParams();

    //? States
    const [cardInfo, setCardInfo] = useState(null);

    return (
        <>
            <Stack.Screen
                options={{
                    title: `${t('payment-method')}: ${method}`,
                    headerBackTitleVisible: false,
                }}
            />

            <View className="flex-1 bg-white py-4">
                {/* <CreditCard
                    holder={"Anonymous"}
                    number={"•••• •••• •••• ••••"}
                    expiration={cardInfo && cardInfo?.expiryMonth ? `${cardInfo?.expiryMonth} / ${cardInfo?.expiryYear}` : "•• / ••"}
                    logo="https://cdn.icon-icons.com/icons2/1186/PNG/512/1490135017-visa_82256.png"
                    country={cardInfo && cardInfo?.country ? cardInfo?.country : ""}
                />
                <KeyboardAvoidingView contentContainerStyle={{ flex: 1 }} className="mt-4 flex-1">
                    {
                        method === "Stripe"
                            ?
                            <StripeCard setCardInfo={setCardInfo} />
                            :
                            <Text>Không có phương thức vào được chọn</Text>
                    }
                </KeyboardAvoidingView> */}

                <Button className="mx-2">{t('submit')}</Button>
            </View>
        </>
    )
}
