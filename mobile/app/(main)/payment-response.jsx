import { Button } from 'components'
import { Stack, useRouter } from 'expo-router'
import LottieView from 'lottie-react-native'
import { useTranslation } from 'react-i18next'
import { Text, View } from 'react-native'
import screen from 'utils/screen'

export default function PaymentResponseScreen() {
    const router = useRouter();

    //? Assets
    const {t} = useTranslation();

    return (
        <>
            <Stack.Screen
                options={{
                    headerShown: false
                }}
            />

            <View className="flex-1 justify-center items-center bg-white">
                <LottieView
                    style={{ width: screen.width * 0.7, height: screen.width * 0.7 }}
                    autoPlay
                    loop={false}
                    className="mt-auto"
                    source={require("../../assets/animations/success.json")}
                />
                <Text className="text-lg font-[500]">{t('thanks')}</Text>

                <View className="mt-auto w-full bg-white py-6 px-4 rounded-t-2xl" style={{
                    shadowColor: "#000",
                    shadowOffset: {
                        width: 0,
                        height: 2,
                    },
                    shadowOpacity: 0.25,
                    shadowRadius: 3.84,
                    elevation: 20,
                }}>
                    <View className="gap-y-5">
                        <View className="flex flex-row justify-between items-center">
                            <Text className="text-gray-400">{t('payment-method')}:</Text>
                            <Text className="font-[500]">VN Pay</Text>
                        </View>
                        <View className="flex flex-row justify-between items-center">
                            <Text className="text-gray-400">{t('pay-date')}:</Text>
                            <Text className="font-[500]">11 Sep 2024</Text>
                        </View>
                        <View className="flex flex-row justify-between items-center">
                            <Text className="text-gray-400">{t('transactionId')}:</Text>
                            <Text className="font-[500]">GODFA11I7</Text>
                        </View>
                        <View className="flex flex-row justify-between items-center">
                            <Text className="text-gray-400">{t('total')}:</Text>
                            <Text className="font-[500]">124,000 đ</Text>
                        </View>
                    </View>

                    <Button className="mt-10" onPress={() => router.navigate("(tabs)")}>
                        {t('back-home')}
                    </Button>
                </View>
            </View>
        </>
    )
}
