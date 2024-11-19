
import React from "react";
import SunIcon from "../../public/assets/icons/sun-icon.svg";
import MoonIcon from "../../public/assets/icons/moon-icon.svg";
import { TFunction } from "i18next";

type ThemeProps = {
    value: string;
    label: string;
    icon?: React.ComponentType
}

export const themes = (t: TFunction<"translation", undefined>): ThemeProps[] => {
    return [
        {
            value: "light",
            label: t("light"),
            icon: SunIcon,
        },
        {
            value: "dark",
            label: t("dark"),
            icon: MoonIcon,
        },
        {
            value: "default",
            label: t("default"),
        }
    ]
}