import { View, Text } from "react-native"

export const DiscountTag = (props) => {
    //? Assets
    const { text } = props

    return (
        <View className={`bg-red-400 px-2 py-0.3 rounded-2xl`}>
            <Text className="text-white text-xs">{text}</Text>
        </View>
    )
}