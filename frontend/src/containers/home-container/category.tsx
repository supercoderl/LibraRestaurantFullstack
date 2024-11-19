import { Title } from "@/components/title";
import { FoodCategoriesContainer, MidContainer } from "./style";
import { ArrowButton } from "@/components/arrow-button";
import FoodCategory from "@/components/food-category";
import { EmblaCarouselType, EmblaOptionsType } from "embla-carousel";
import React, { useCallback, useEffect, useState } from "react";
import useEmblaCarousel from "embla-carousel-react";
import Category from "@/type/Category";
import { Empty } from "@/components/empty";
import { TFunction } from "i18next";
import { FoodCategorySkeleton } from "@/components/food-category/skeleton";

interface CategoryProps {
  categories: Category[];
  t: TFunction<"translation", undefined>;
  loading: boolean;
}

export const CategorySlide: React.FC<CategoryProps> = ({ categories, t, loading }) => {
  const OPTIONS: EmblaOptionsType = { slidesToScroll: 2, containScroll: 'trimSnaps', align: 'start' };
  const [emblaApi, setEmblaApi] = useState<EmblaCarouselType | null>(null);

  return (
    <>
      <MidContainer>
        <Title $isBigger>{t("catalog")}</Title>
        <ArrowButton embla={emblaApi} />
      </MidContainer>
      <FoodCategoriesContainer>
        {
          loading ? (
            <Carousel
              options={OPTIONS}
              setEmbla={setEmblaApi}
            >
              {
                Array.from({ length: 8 }).map((_, index) => (
                  <div className='embla__slide__1' key={index}>
                    <FoodCategorySkeleton />
                  </div>
                ))
              }
            </Carousel>
          )
            :
            categories.length <= 0 ? (
              <Empty title={t("catalog-empty")} />
            )
              :
              <Carousel
                options={OPTIONS}
                setEmbla={setEmblaApi}
              >
                {categories.map(e => (
                  <div className='embla__slide__1' key={e.categoryId}>
                    <FoodCategory t={t} category={e}></FoodCategory>
                  </div>
                ))}
              </Carousel>
        }
      </FoodCategoriesContainer>
    </>
  )
}

type PropType = {
  options?: EmblaOptionsType;
  setEmbla: React.Dispatch<React.SetStateAction<EmblaCarouselType | null>>;
  children?: React.ReactNode;
};

const Carousel: React.FC<PropType> = (props) => {
  const { options, children, setEmbla } = props
  const [emblaRef, emblaApi] = useEmblaCarousel(options);

  const [visibleSlides, setVisibleSlides] = useState<React.ReactNode[]>([]);
  const maxVisibleSlides = 4;

  const updateVisibleSlides = useCallback((emblaApi: EmblaCarouselType) => {
    const index = emblaApi.selectedScrollSnap();
    const allChildren = React.Children.toArray(children);
    const nextVisibleSlides = allChildren.slice(index, index + maxVisibleSlides);
    setVisibleSlides(nextVisibleSlides);
  }, [children]);

  useEffect(() => {
    if (!emblaApi) return;

    updateVisibleSlides(emblaApi);

    emblaApi.on('select', () => {
      updateVisibleSlides(emblaApi);
      setEmbla(emblaApi);
    });
    emblaApi.on('reInit', () => {
      updateVisibleSlides(emblaApi);
      setEmbla(emblaApi);
    });
    setEmbla(emblaApi);
  }, [emblaApi, updateVisibleSlides]);

  return (
    <div className="embla">
      <div className="embla__viewport" ref={emblaRef}>
        <div className="embla__container">
          {children}
        </div>
      </div>
    </div>
  )
}