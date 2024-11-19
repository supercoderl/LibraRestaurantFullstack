import { useStoreDispatch, useStoreSelector } from "@/redux/store"
import { CategoryFood, CategoryFoodItem, CategoryFoodItemImage, CategoryFoodItemLink, CategoryFoodItemText, SecondaryContainer } from "./style"
import { EmblaOptionsType } from "embla-carousel";
import EmblaCarousel from 'src/plugins/Carousel/EmblaCarousel'
import { setCurrentCategory } from "@/redux/slices/categories-slice";
import { fetchData } from "@/redux/slices/products-slice";

export const SecondCategory = () => {
    const { categories, currentCategory } = useStoreSelector(state => state.mainCategorySlice);
    const OPTIONS: EmblaOptionsType = { align: "start", slidesToScroll: 1, loop: false, dragFree: true }
    const dispatch = useStoreDispatch();

    const handleClick = (categoryId: number) => {
        dispatch(setCurrentCategory(currentCategory === categoryId ? -1 : categoryId));
        dispatch(fetchData({ categoryId: currentCategory === categoryId ? -1 : categoryId }));
    }

    return (
        <SecondaryContainer>
            <CategoryFood>
                <EmblaCarousel
                    className='category'
                    isAutoPlay={false}
                    options={OPTIONS}>
                    {
                        categories.map((category, index) => (
                            <CategoryFoodItem
                                key={index}
                                onClick={() => handleClick(category.categoryId)}
                                $isActive={currentCategory === category.categoryId}
                            >
                                <CategoryFoodItemLink>
                                    <CategoryFoodItemImage src={category.picture || ""} alt="" />
                                    <CategoryFoodItemText>{category.name}</CategoryFoodItemText>
                                </CategoryFoodItemLink>
                            </CategoryFoodItem>
                        ))
                    }
                </EmblaCarousel>
            </CategoryFood>
        </SecondaryContainer>
    )
}