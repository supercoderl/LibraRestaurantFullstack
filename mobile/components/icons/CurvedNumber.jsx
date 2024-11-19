import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import Svg, { Circle, Path, Rect } from 'react-native-svg';

export const CurvedNumberIcon = ({ number, size }) => {
    return (
        <View style={[styles.container]}>
            <Svg style={styles.svg} width={size} height={size} viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg">
                <Rect width="48" height="48" fill="white" fillOpacity="0.01" />
                <Path d="M5 7C5 5.34315 6.34315 4 8 4H32C33.6569 4 35 5.34315 35 7V44H8C6.34315 44 5 42.6569 5 41V7Z" fill="none" stroke="black" strokeWidth="4" strokeLinejoin="round" />
                <Path d="M35 24C35 22.8954 35.8954 22 37 22H41C42.1046 22 43 22.8954 43 24V41C43 42.6569 41.6569 44 40 44H35V24Z" stroke="black" strokeWidth="4" strokeLinejoin="round" />
                <Path d="M11 12H19" stroke="white" strokeWidth="4" strokeLinecap="round" strokeLinejoin="round" />
                <Path d="M11 19H23" stroke="white" strokeWidth="4" strokeLinecap="round" strokeLinejoin="round" />
            </Svg>
            <Text style={[{ fontSize: size / 2.6 }, styles.number]}>{number}</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        position: 'relative', // Allows positioning of the number
        justifyContent: 'center',
        alignItems: 'center',
    },
    svg: {
        position: "absolute"
    },

    number: {
        fontWeight: "bold",
        color: "black",
        zIndex: 1,
        marginRight: 4
    }
});
