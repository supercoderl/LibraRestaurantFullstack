import { Container, Image, Text } from "./style"
import React from "react"

type HeroProps = {
    title: string;
}

export const Hero: React.FC<HeroProps> = ({ title }) => {
    return (
        <Container>
            <Image src="https://fastly.4sqi.net/img/general/1398x536/50831682_WnIuyuze-vDL_GIOmpMeclFdxYadrDD_gbEQgKfSAzY.jpg" alt="" />
            <Text>{title}</Text>
        </Container>
    )
}