import React from "react";
import { Container, ItemContainer, LoadingContainer, Text } from "./style"
import FoodIcon from "../../../public/assets/icons/food-icon.svg";
import { theme } from "twin.macro";
import { Spinner } from "../loading/spinner";

type AutoCompleteProps = {
    items: any;
    handleChoose: (value: any) => void;
    isOpen: boolean;
    loading: boolean;
}

export const AutoComplete: React.FC<AutoCompleteProps> = ({ items, handleChoose, isOpen, loading }) => {
    return (
        <Container $isOpen={isOpen}>
            {
                loading ?
                    <LoadingContainer>
                        <Spinner width={30} />
                    </LoadingContainer>
                    :
                    items && items.length > 0 &&
                    items.map((item: any, index: number) => (
                        <ItemContainer key={index} onClick={() => handleChoose(item.value)}>
                            <FoodIcon width={20} fill={theme`textColor.primary`} />
                            <Text>{item.label}</Text>
                        </ItemContainer>
                    ))
            }
        </Container>
    )
}