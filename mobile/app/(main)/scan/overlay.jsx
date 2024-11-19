import { Canvas, DiffRect, rect, rrect } from "@shopify/react-native-skia";
import { Dimensions, Platform, StyleSheet } from "react-native";
import screen from "utils/screen";

const innerDimension = 260;

const outer = rrect(rect(0, 0, screen.width, screen.height), 0, 0);
const inner = rrect(
    rect(
        screen.width / 2 - innerDimension / 2,
        screen.height / 2 - innerDimension / 2,
        innerDimension,
        innerDimension
    ),
    50,
    50
);

export const Overlay = () => {
    return (
        <Canvas
            style={
                Platform.OS === "android" ? { flex: 1 } : StyleSheet.absoluteFillObject
            }
        >
            <DiffRect inner={inner} outer={outer} color="black" opacity={0.5} />
        </Canvas>
    );
};