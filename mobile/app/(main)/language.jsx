import React from 'react';
import {
    Text,
    TouchableOpacity,
    View,
    FlatList,
} from 'react-native';
import i18next, { languageResources } from '../../services/i18next';
import { useTranslation } from 'react-i18next';
import languagesList from '../../services/languagesList.json';
import { Stack } from 'expo-router';
import { Icons } from 'components';

export default function LanguageScreen() {
    const { t } = useTranslation();

    const changeLng = lng => {
        i18next.changeLanguage(lng);
    };

    return (
        <>
            <Stack.Screen
                options={{
                    title: t('change-language')
                }}
            />
            <View className="bg-white flex-1">
                <FlatList
                    data={Object.keys(languageResources)}
                    renderItem={({ item }) => (
                        <TouchableOpacity
                            className="flex flex-row justify-between items-center p-3 border-b-[1px] border-gray-300"
                            onPress={() => changeLng(item)}>
                            <Text className="">
                                {languagesList[item].nativeName}
                            </Text>
                            {item === i18next.language && <Icons.MaterialCommunityIcons name='check' size={20} />}
                        </TouchableOpacity>
                    )}
                />
            </View>
        </>
    );
};