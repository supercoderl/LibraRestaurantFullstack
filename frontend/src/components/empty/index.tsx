import React from "react";
import { Body, Container, Image, Title } from "./style"

type EmptyType = {
    title: string;
}

export const Empty: React.FC<EmptyType> = ({ title }) => {
    return (
        <Container>
            <Body>
                <Image src="https://static.thenounproject.com/png/203873-200.png" alt="empty" />
                <Title><strong>{title}</strong></Title>
            </Body>
        </Container>
    )
}