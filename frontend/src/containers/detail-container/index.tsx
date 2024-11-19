import Header from "@/components/header";
import { CenterContainer, BodyContainer, Container } from "./style";
import { Hero } from "@/components/hero";
import { TFunction } from "i18next";
import { useEffect } from "react";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { getItemAsync } from "@/redux/slices/products-slice";
import { toast } from "react-toastify";
import { UsLoading } from "@/components/loading/usLoading";
import { NextRouter } from "next/router";
import { Detail } from "@/components/food-item/detail";

type FoodDetailContainerProps = {
    t: TFunction<"translation", undefined>;
    slug?: string;
    router: NextRouter;
}

export default function FoodDetailContainer({ t, slug, router }: FoodDetailContainerProps) {

    const dispatch = useStoreDispatch();
    const { loading, item } = useStoreSelector(state => state.mainProductSlice);

    useEffect(() => {
        if (!slug) {
            toast(t("reservation-not-exists"), {
                type: "error"
            });
            setTimeout(() => router.back(), 1500);
        }
        else {
            dispatch(getItemAsync(slug))
        }
    }, [slug]);

    return (
        <>
            <Container>
                <BodyContainer>
                    <CenterContainer>
                        <Header t={t} />
                        <Hero title={t("food")} />
                        {
                            loading ?
                                <UsLoading />
                                :
                                item &&
                                <Detail item={item} dispatch={dispatch} t={t} />
                        }
                    </CenterContainer>
                </BodyContainer>
            </Container>
        </>
    );
}
