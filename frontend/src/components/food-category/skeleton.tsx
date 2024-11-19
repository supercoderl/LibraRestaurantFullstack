import { CategoryContentContainer, CategoryImageContainer, Container, ImageSkeleton, Text, TextQuantity } from "./style"


export const FoodCategorySkeleton = () => {
    return (
        <Container className="group">
            <CategoryImageContainer>
                <ImageSkeleton />
            </CategoryImageContainer>
            <CategoryContentContainer $isSkeleton={true} className="catagory-product-content text-center">
                <Text $isSkeleton={true}>asdasdasdasd</Text>
                <TextQuantity $isSkeleton={true}>asdasdasda</TextQuantity>
            </CategoryContentContainer>
        </Container>
    )
}