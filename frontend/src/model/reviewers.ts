import { TFunction } from "i18next";

export const reviewers = (t: TFunction<"translation", undefined>) => {
    return [
        {
            picture: "https://cdn.tuoitre.vn/zoom/720_450/1200/900/ttc/r/2021/05/30/food-reviewer-bi-bat-01-1622348843.jpeg",
            name: `${t("mr")} Quoc Trung`,
            tag: t("loyal-customer"),
            text: t("review-1")
        },
        {
            picture: "https://giadinh.mediacdn.vn/zoom/700_438/296230595582509056/2022/12/4/3134279044302104059601017106308727301893929n-16701610798731583337403-0-0-556-889-crop-16701613871431694739965.jpg",
            name: `${t("mrs")} Nguyen Thuy Hanh`,
            tag: t("vblogger"),
            text: t("review-2")
        },
        {
            picture: "https://i.ytimg.com/vi/2s-m2XeTcH4/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLAl3zcSH3BqFyL6m_wryyiiVqJV-A",
            name: `${t("mr2")} Nakamoto Hondo`,
            tag: t("president"),
            text: t("review-3")
        }
    ]
}