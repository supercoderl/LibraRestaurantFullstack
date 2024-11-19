import { Title } from "@/components/title"
import { CircleButtonIcon, FoodContainer, MenuLink, MidContainer } from "./style"
import React from "react";
import Item from "@/type/Item";
import { FoodItemSkeleton } from "@/components/food-item/skeleton";
import { Empty } from "@/components/empty";
import { TFunction } from "i18next";
import FoodItem2 from "@/components/food-item/item2";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import ArrowRight from "../../../public/assets/icons/right-arrow-icon.svg";

interface FoodProps {
    currentCategory: number;
    items: Item[];
    showTitle?: boolean;
    isReservation?: boolean;
    loading: boolean;
    t: TFunction<"translation", undefined>
}

export const Food: React.FC<FoodProps> = ({ items, showTitle, isReservation, loading, t }) => {
    const { width } = useWindowDimensions();

    return (
        <>
            {
                showTitle &&
                <MidContainer>
                    <Title $isBigger>{t("popular-food")}</Title>

                    {
                        width > 767 ?
                            <MenuLink href="menu">{t("go-to-menu")}</MenuLink>
                            :
                            <CircleButtonIcon href="menu">
                                <ArrowRight width={20} height={20} fill="white" />
                            </CircleButtonIcon>
                    }
                </MidContainer>
            }

            {
                !loading && items.length <= 0 && (
                    <Empty title={t("food-empty")} />
                )
            }

            <FoodContainer $isReservation={isReservation} className="food-container">
                {
                    loading ?
                        Array.from({ length: 8 }).map((_, index) => (
                            <FoodItemSkeleton key={index} />
                        ))
                        :
                        items.map(e => (
                            <FoodItem2 {...e} key={e.itemId}></FoodItem2>
                        ))
                }
            </FoodContainer>
        </>
    )
}