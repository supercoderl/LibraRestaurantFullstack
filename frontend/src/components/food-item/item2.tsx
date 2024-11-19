import Item from "@/type/Item";
import { AddButton, AddIcon, AddIconPath, ContainerItem2, ContentResume, ContentTitle, ContentTitleLink, DzImageBox, DzMedia, FoodImage, MenuDetail, MenuFooter, Price, RegularPrice } from "./style";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { useTranslation } from "next-i18next";
import { toast } from "react-toastify";
import { addItem } from "@/redux/slices/cart-slice";

export default function FoodItem2(item: Item) {
    const dispatch = useStoreDispatch();
    const id = useStoreSelector(state => state.reservation.id);
    const { t } = useTranslation();

    const handleClick = () => {
        if (!id) {
            toast(t("you-have-not-reservation"), { type: "warning" });
        }
        else {
            dispatch(addItem({ item }));
        }
    };

    return (
        <ContainerItem2>
            <DzImageBox className="group">
                <MenuDetail>
                    <DzMedia>
                        <FoodImage loading="lazy" src={item.picture || process.env.NEXT_PUBLIC_DUMMY_IMAGE} alt="/" />
                    </DzMedia>
                    <div className="dz-content">
                        <ContentTitle><ContentTitleLink href={`detail/${item.slug}`}>{item.title}</ContentTitleLink></ContentTitle>
                        <ContentResume>#{item.sku}</ContentResume>
                    </div>
                </MenuDetail>
                <MenuFooter>
                    <RegularPrice>Giá gốc</RegularPrice>
                    <Price>{item.price} ₫</Price>
                </MenuFooter>
                <AddButton onClick={handleClick}>
                    <AddIcon width={24} height={26} viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <AddIconPath d="M12 4a1 1 0 0 1 1 1v6h6a1 1 0 1 1 0 2h-6v6a1 1 0 1 1-2 0v-6H5a1 1 0 1 1 0-2h6V5a1 1 0 0 1 1-1z" />
                    </AddIcon>
                </AddButton>
            </DzImageBox>
        </ContainerItem2>
    )
}