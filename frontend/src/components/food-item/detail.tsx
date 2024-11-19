import Item from "@/type/Item";
import { DetailSection, FoodDetailContainer, FoodDetailContainerButton, FoodDetailContentContainer, FoodDetailImage, FoodDetailImageContainer, FoodDetailItem, FoodDetailList, FoodDetailMedia, FoodDetailPrice, FoodDetailQuantity, FoodDetailRating, FoodDetailSKU, FoodDetailSummary, FoodDetailTag, FoodDetailTitle } from "./style";
import { Button, ItemInfoPriceText, QuantityButton, QuantityContainer } from "@/containers/order-container/style";
import AddIcon from '../../../public/assets/icons/add-icon.svg';
import SubtractIcon from '../../../public/assets/icons/subtract-icon.svg';
import React, { useState } from "react";
import StarIcon from "../../../public/assets/icons/star-fill-icon.svg";
import { addItem, changeQuantity } from "@/redux/slices/cart-slice";
import { toast } from "react-toastify";
import { useStoreSelector } from "@/redux/store";
import { TFunction } from "i18next";
import { DetailTabs } from "@/containers/detail-container/tab";

type DetailProps = {
    item: Item;
    dispatch: any;
    t: TFunction<"translation", undefined>;
}

export const Detail: React.FC<DetailProps> = ({ item, dispatch, t }) => {
    const [quantity, setQuantity] = useState(1);
    const { id } = useStoreSelector(state => state.reservation);

    const addToCart = () => {
        if (!id) {
            toast(t("you-have-not-reservation"), { type: "warning" })
        }
        else {
            dispatch(addItem({ item, quantity }));
        }
    }

    return (
        <DetailSection>
            <FoodDetailContainer>
                <FoodDetailImageContainer>
                    <FoodDetailMedia>
                        <FoodDetailImage src={process.env.NEXT_PUBLIC_DUMMY_PICTURE} alt="/" />
                    </FoodDetailMedia>
                </FoodDetailImageContainer>
                <FoodDetailContentContainer>
                    <div style={{ position: "relative", height: "100%" }}>
                        <FoodDetailRating>
                            <StarIcon width={18} height={18} fill="orange" /> <span className="text-bodycolor"><strong className="font-medium">{item.ratingScore}</strong> {t("rating-score")}</span>
                        </FoodDetailRating>
                        <div>
                            <FoodDetailTitle>{item?.title}</FoodDetailTitle>
                        </div>
                        <FoodDetailSKU>{t("sku")}: <strong>{item?.sku}</strong></FoodDetailSKU>
                        <FoodDetailSummary>{item?.summary}</FoodDetailSummary>
                        <FoodDetailList>
                            <FoodDetailItem>
                                {t("price")}
                                <FoodDetailPrice>{item?.price} ₫</FoodDetailPrice>
                            </FoodDetailItem>
                            <FoodDetailItem>{t("quantity")}
                                <FoodDetailQuantity>
                                    <QuantityContainer>
                                        <QuantityButton
                                            $isDisabled={false}
                                            disabled={false}
                                            onClick={() => {
                                                if (quantity > 1) {
                                                    setQuantity(quantity - 1)
                                                }
                                            }}
                                        >
                                            <SubtractIcon width={16} fill="white" />
                                        </QuantityButton>
                                        <ItemInfoPriceText>{quantity}</ItemInfoPriceText>
                                        <QuantityButton
                                            $isDisabled={false}
                                            onClick={() => setQuantity(quantity + 1)}>
                                            <AddIcon width={16} fill="white" />
                                        </QuantityButton>
                                    </QuantityContainer>
                                </FoodDetailQuantity>
                            </FoodDetailItem>
                        </FoodDetailList>
                        <FoodDetailContainerButton>
                            <Button onClick={addToCart} className="group">
                                {t("add-to-cart")}
                            </Button>
                        </FoodDetailContainerButton>
                    </div>
                </FoodDetailContentContainer>
            </FoodDetailContainer>

            <DetailTabs item={item} dispatch={dispatch} t={t} />
        </DetailSection>
    )
}