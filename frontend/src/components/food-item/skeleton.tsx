import { Container, DetailContainer, ImageContainer, ImageSkeleton, PlusContainer, RowContainer, StyledStarIcon, TimeContainer, TimeText, Title } from "./style"

export const FoodItemSkeleton = () => {
    return (
        <Container className='group'>
            <ImageContainer>
                <ImageSkeleton className="custom-gradient" />
            </ImageContainer>
            <DetailContainer>
                <RowContainer>
                    <Title $isSkeleton className="custom-gradient">asdasdasdasdasda</Title>
                </RowContainer>
                <RowContainer>
                    <Title $isSkeleton className="custom-gradient">asdasdaaa</Title>
                    <Title $isSkeleton className="custom-gradient">asdasda</Title>
                </RowContainer>
            </DetailContainer>
        </Container>
    )
}