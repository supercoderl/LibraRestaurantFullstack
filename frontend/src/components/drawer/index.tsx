import React, { Dispatch, SetStateAction } from "react";
import { Body, Container, ContentContainer, InnerBackground, InnerContainer, OuterBackground, Wrapper } from "./style";

type DrawerProps = {
    open: boolean;
    side: "top" | "left" | "bottom" | "right";
    setOpen: Dispatch<SetStateAction<boolean>>;
    children: React.ReactNode
}

const Drawer: React.FC<DrawerProps> = ({ open, setOpen, side = "right", children }) => {
    return (
        <Container
            id={`dialog-${side}`}
            aria-labelledby="slide-over"
            role="dialog"
            aria-modal="true"
            onClick={() => setOpen(!open)}
        >
            <OuterBackground $open={open} />
            <InnerBackground $open={open}>
                <InnerContainer>
                    <Body $side={side} $open={open}>
                        <Wrapper
                            $side={side}
                            $open={open}
                            onClick={(event) => {
                                event.preventDefault();
                                event.stopPropagation();
                            }}
                        >
                            <ContentContainer>
                                {children}
                            </ContentContainer>
                        </Wrapper>
                    </Body>
                </InnerContainer>
            </InnerBackground>
        </Container>
    );
};

export default Drawer;
