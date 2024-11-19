import { Link } from 'expo-router';
import React from 'react';
import { View, Text, Image, StyleSheet, TouchableOpacity, Pressable } from 'react-native';
import { truncate } from 'utils';
import screen from 'utils/screen';

const CategoryItem = ({ title, picture, id }) => {
    return (
        <Link
            href={{
                pathname: `/categories/${id}`,
                params: { picture, title }
            }}
            asChild
        >
            <Pressable style={styles.container} className="flex-1 h-[100px] m-2 rounded-md relative bg-white active:scale-95">
                <Image
                    source={{ uri: picture }}
                    style={styles.image}
                    className="w-full h-full absolute"
                />
                {/* Overlay two white triangles */}
                <View style={styles.leftTriangle} className="absolute w-1/2 h-[300%] bg-white rotate-[40deg]" />
                <View style={styles.rightTriangle} className="absolute w-1/2 h-[300%] bg-white rotate-[-30deg]" />
                {/* Title */}
                <Text className="absolute bottom-5 left-2 text-black text-[16px] font-bold">{truncate(title, screen.width * 0.03)}</Text>
            </Pressable>
        </Link>
    );
};

const styles = StyleSheet.create({
    container: {
        overflow: "hidden",
        shadowColor: "#000",
        shadowOffset: {
            width: 0,
            height: 1,
        },
        shadowOpacity: 0.15,
        shadowRadius: 1.84,
        elevation: 5,
    },
    image: {
        left: screen.width * 0.1,
        width: "100%"
    },
    leftTriangle: {
        top: -(screen.width * 0.2),
        right: screen.width * 0.3,
    },
    rightTriangle: {
        top: -(screen.width * 0.2),
        left: screen.width * 0.02,
    },
});

export default CategoryItem;