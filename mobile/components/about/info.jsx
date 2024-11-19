import { useTranslation } from "react-i18next"
import { Image, Text, View } from "react-native"

export const AboutInfo = () => {
    //? Assets
    const {t} = useTranslation();

    return (
        <View className="py-0 px-4">
            <Text className="text-xl font-[500]">{t('project')}</Text>
            <View className="py-5">
                <View className="h-1 w-12 rounded-md bg-blue-300 mb-3" />
                <View className="flex flex-row gap-x-4 items-center">
                    <Image
                        source={{ uri: "https://media.licdn.com/dms/image/v2/D4E03AQEXuv4Tx8mANw/profile-displayphoto-shrink_400_400/profile-displayphoto-shrink_400_400/0/1704889210498?e=2147483647&v=beta&t=IXVqkbQ6V3fgJOO0fuggp_PAexMUcQ0ZVpv20rZydcY" }}
                        alt="avatar"
                        className="w-8 h-8 rounded-full"
                    />
                    <View className="flex justify-center">
                        <Text className="font-[500]">John Smith</Text>
                        <Text className="text-gray-400">{t('build-date')}</Text>
                    </View>
                </View>
            </View>
            <Text className="text-gray-500 text-justify mb-5">{t('about-text1')}</Text>
            <Text className="text-gray-500 text-justify mb-5">{t('about-text2')}</Text>
            <Text className="text-gray-500 text-justify mb-5">{t('about-text3')}</Text>
            <Text className="text-gray-500 text-justify mb-5">{t('about-text4')}</Text>
        </View>
    )
}