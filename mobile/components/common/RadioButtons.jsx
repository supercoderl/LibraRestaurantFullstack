import React from 'react';
import { TouchableOpacity, Image, Text, StyleSheet, View } from 'react-native';

export const RadioButton = ({ isChecked, handleCheck, leftImage, text }) => {
    return <TouchableOpacity
        onPress={handleCheck}
        className="flex flex-row items-center justify-between p-3 border-b-[1px] border-gray-200"
    >
        <View className="flex flex-row items-center gap-x-3">
            <View className="p-3 rounded-2xl bg-gray-100">
                <Image source={leftImage} style={radioStyle.leftImg} />
            </View>
            <Text className="text-lg font-semibold">{text}</Text>
        </View>
        {isChecked ?
            <Image
                source={require("../../assets/icons/radio-checked.png")}
                className="w-5 h-5"
                style={{ tintColor: isChecked ? "#FF6500" : "#B7B7B7" }}
            />
            :
            <Image
                source={require("../../assets/icons/radio-unchecked.png")}
                className="w-5 h-5"
                style={{ tintColor: isChecked ? "#FF6500" : "#B7B7B7" }}
            />
        }
    </TouchableOpacity>
}

const radioStyle = StyleSheet.create({
    leftImg: { height: 20, width: 20, resizeMode: 'contain' },
    tick: { position: 'absolute', right: 0, height: 30, width: 30, marginRight: 30, tintColor: 'white' }
});