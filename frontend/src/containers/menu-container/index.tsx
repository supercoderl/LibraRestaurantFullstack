import Header from "@/components/header"
import { BodyContainer, CenterContainer, Container, DzBody, DzContent, DzHead, DzHeadImageLine, DzHeadPrice, DzHeadTitle, DzHeadTitleLink, DzShopCard, FoodColumn, ImageLeft, ImageRight, ImageRight2, MenuHead, MenuHeadTitle, Row, Section } from "./style"
import { Hero } from "@/components/hero"
import Footer from "@/components/footer"
import { TFunction } from "i18next"
import { useStoreDispatch, useStoreSelector } from "@/redux/store"
import friedChicken from "../../../public/assets/images/food/fried-chicken-removebg.png"
import { useEffect } from "react"
import { fetchData } from "@/redux/slices/products-slice"
import plan from "../../../public/assets/images/food/plan-removebg-preview.png"
import { Loading } from "@/components/loading"
import { UsLoading } from "@/components/loading/usLoading"

type MenuProps = {
    t: TFunction<"translation", undefined>;
}

export default function MenuContainer({ t }: MenuProps) {
    const dispatch = useStoreDispatch();

    const { categories, items, loading, categoryLoading } = useStoreSelector(state => ({
        categories: state.mainCategorySlice.categories,
        items: state.mainProductSlice.items,
        loading: state.mainProductSlice.loading,
        categoryLoading: state.mainCategorySlice.loading
    }));

    useEffect(() => {
        dispatch(fetchData({ pageSize: 100 }));
    }, [dispatch]);

    return (
        <Section>
            <Container>
                <BodyContainer>
                    <CenterContainer>
                        <Header t={t} />
                        <Hero title={t("our-menu")} />

                        {
                            loading || categoryLoading ?
                                <UsLoading />
                                :
                                (
                                    <Row>
                                        {
                                            categories.map((item, index) => (
                                                <FoodColumn key={index}>
                                                    <MenuHead>
                                                        <MenuHeadTitle>{item.name}</MenuHeadTitle>
                                                    </MenuHead>
                                                    {
                                                        items.filter(x => x.categoryIds.includes(item.categoryId)).map((food, foodIndex) => (
                                                            <DzShopCard key={foodIndex}>
                                                                <DzContent>
                                                                    <DzHead>
                                                                        <DzHeadTitle><DzHeadTitleLink href="shop-style-2.html">{food.title}</DzHeadTitleLink></DzHeadTitle>
                                                                        <DzHeadImageLine></DzHeadImageLine>
                                                                        <DzHeadPrice>{food.price} ₫</DzHeadPrice>
                                                                    </DzHead>
                                                                    <DzBody>
                                                                        {food.summary}
                                                                    </DzBody>
                                                                </DzContent>
                                                            </DzShopCard>
                                                        ))
                                                    }
                                                </FoodColumn>
                                            ))
                                        }

                                        <ImageLeft src={friedChicken.src} alt="/" />
                                        <ImageRight src="https://www.pngplay.com/wp-content/uploads/6/Cocktail-Drink-Transparent-Background.png" alt="/" />
                                        <ImageRight2 src={plan.src} alt="/" />
                                    </Row>
                                )
                        }
                    </CenterContainer>
                </BodyContainer>

                <Footer t={t} />
            </Container>
        </Section>
    )
}