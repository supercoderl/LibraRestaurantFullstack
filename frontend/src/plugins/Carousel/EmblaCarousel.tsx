import React, { useCallback, useEffect, useState } from 'react'
import { EmblaCarouselType, EmblaOptionsType } from 'embla-carousel'
import useEmblaCarousel from 'embla-carousel-react'
import Autoplay from 'embla-carousel-autoplay';

type PropType = {
  options?: EmblaOptionsType;
  children?: React.ReactNode;
  className?: string;
  isAutoPlay: boolean;
}

const EmblaCarousel: React.FC<PropType> = (props) => {
  const { options, children, className, isAutoPlay } = props
  const [emblaRef, emblaApi] = useEmblaCarousel(options, [
    Autoplay({ playOnInit: isAutoPlay, delay: 3000 })
  ]);

  const [visibleSlides, setVisibleSlides] = useState<React.ReactNode[]>([]);
  const maxVisibleSlides = 4;

  const updateVisibleSlides = useCallback((emblaApi: EmblaCarouselType) => {
    const index = emblaApi.selectedScrollSnap();
    const allChildren = React.Children.toArray(children);
    const nextVisibleSlides = allChildren.slice(index, index + maxVisibleSlides);
    setVisibleSlides(nextVisibleSlides);
  }, [children]);

  useEffect(() => {
    const autoplay = emblaApi?.plugins()?.autoplay;
    if (!autoplay) return;

    if (!emblaApi) return;

    updateVisibleSlides(emblaApi);

    emblaApi.on('select', () => {
      updateVisibleSlides(emblaApi);
    });
    emblaApi.on('reInit', () => {
      updateVisibleSlides(emblaApi);
    });
  }, [emblaApi, updateVisibleSlides]);

  return (
    <div className={`embla ${className}`}>
      <div className="embla__viewport" ref={emblaRef}>
        <div className="embla__container">
          {children}
        </div>
      </div>
    </div>
  )
}

export default EmblaCarousel
