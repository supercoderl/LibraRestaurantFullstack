import React from 'react';
import { ButtonDiv, ButtonDivLeft, ButtonDivRight } from './style';
import LeftArrowIcon from '../../../public/assets/icons/left-arrow-icon.svg';
import RightArrowIcon from '../../../public/assets/icons/right-arrow-icon.svg';
import { EmblaCarouselType } from 'embla-carousel';
import { usePrevNextButtons } from 'src/plugins/Carousel/EmblaCarouselArrayButtons';

interface ArrayButtonProps {
    embla: EmblaCarouselType | null;
}

export const ArrowButton: React.FC<ArrayButtonProps> = ({ embla }) => {

    const { onPrevButtonClick, onNextButtonClick } = usePrevNextButtons(embla!);

    return (
        <ButtonDiv>
            <ButtonDivLeft onClick={onPrevButtonClick}>
                <LeftArrowIcon fill="white" height="40%"></LeftArrowIcon>
            </ButtonDivLeft>
            <ButtonDivRight onClick={onNextButtonClick}>
                <RightArrowIcon fill="white" height="40%"></RightArrowIcon>
            </ButtonDivRight>
        </ButtonDiv>
    );
}
