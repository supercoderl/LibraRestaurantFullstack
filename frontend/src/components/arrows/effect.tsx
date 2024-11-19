import React from "react"
import { Chevron, Container, Text } from "./style"
import { TFunction } from "i18next"

type ArrowEffectProps = {
    t: TFunction<"translation", undefined>
}

export const ArrowEffect: React.FC<ArrowEffectProps> = ({ t }) => {
    return (
        <>
            <Text>{t("pay-below")}</Text>

            <Container>
                <Chevron className="chevron-effect"></Chevron>
                <Chevron className="chevron-effect"></Chevron>
                <Chevron className="chevron-effect"></Chevron>
            </Container>
        </>
    )
}