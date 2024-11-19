import Header from "@/components/header";
import { Food } from "../home-container/food";
import { useStoreSelector } from "src/redux/store";
import { shallowEqual } from "react-redux";
import { Hero } from "@/components/hero";
import { SecondCategory } from "@/components/food-category/second-category";
import { BodyContainer, CenterContainer, Container, ContentContainer } from "./style";
import { TFunction } from "i18next";

export default function FoodContainer({ t }: { t: TFunction<"translation", undefined> }) {
    const { items } = useStoreSelector(
        state => ({
            items: state.mainProductSlice.items
        }),
        shallowEqual,
    );

    return (
        <Container>
            <BodyContainer>
                <CenterContainer>
                    <Header t={t} />
                    <Hero title={t("food-store")} />
                    <SecondCategory />
                    <ContentContainer>
                        <Food
                            t={t}
                            loading={false}
                            showTitle={false}
                            currentCategory={1}
                            items={items}
                            isReservation={false}
                        />
                    </ContentContainer>
                </CenterContainer>
            </BodyContainer>
        </Container>
    );
}
