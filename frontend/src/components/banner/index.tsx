import React, { useEffect } from 'react'
import EmblaCarousel from 'src/plugins/Carousel/EmblaCarousel'
import { EmblaOptionsType } from 'embla-carousel';
import { LazyLoadImage } from 'src/plugins/Carousel/EmblaCarouselLazyLoadImage';
import { banners } from './item';

export default function Banner() {
    const OPTIONS: EmblaOptionsType = { loop: true }

    return (
        <EmblaCarousel
            className='banner'
            isAutoPlay={true}
            options={OPTIONS}>
            {
                banners.map((item, index) => (
                    <LazyLoadImage
                        key={index}
                        index={index}
                        imgSrc={process.env.NEXT_PUBLIC_CLOUDINARY_URL + item}
                        inView={banners.indexOf(item) > -1}
                    />
                ))
            }
        </EmblaCarousel>
    );
}