import { EmblaOptionsType } from "embla-carousel"
import { Body, ContentContainer, DzContent, DzMedia, HeaderTitle, Image, InnerContainer, Quota, Review, Reviewer, ReviewInfo, ReviewText, RowHeader, RowHeaderCol, Section, ServiceHeader, Tag, TitleSpan } from "./style"
import EmblaCarousel from "@/plugins/Carousel/EmblaCarousel"
import { reviewers } from "@/model/reviewers"
import { TFunction } from "i18next"
import React from "react"

type CustomerReviewProps = {
    t: TFunction<"translation", undefined>;
}

export const CustomerReview: React.FC<CustomerReviewProps> = ({ t }) => {
    const OPTIONS: EmblaOptionsType = { loop: true }

    return (
        <Section>
            <InnerContainer>
                <RowHeader>
                    <RowHeaderCol>
                        <ServiceHeader>
                            <HeaderTitle>{t("review-of")} <TitleSpan>{t("customer")}</TitleSpan></HeaderTitle>
                        </ServiceHeader>
                    </RowHeaderCol>
                </RowHeader>
                <EmblaCarousel options={OPTIONS} isAutoPlay={true}>
                    {
                        reviewers(t).map((item, index) => (
                            <div className="embla__slide" key={index}>
                                <div
                                    className={'embla__lazy-load'}
                                >
                                    <ContentContainer>
                                        <Body>
                                            <DzMedia>
                                                <Image src={item.picture} alt="/" />
                                            </DzMedia>
                                            <DzContent>
                                                <Review>
                                                    <ReviewText>{item.text}</ReviewText>
                                                </Review>
                                                <ReviewInfo>
                                                    <Reviewer>{item.name}</Reviewer>
                                                    <Tag>{item.tag}</Tag>
                                                </ReviewInfo>
                                                <Quota viewBox="0 -2 20 20" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink">
                                                    <title>editor / 24 - editor, end, quotation, quote, testimonial, tool icon</title>
                                                    <g id="Free-Icons" stroke="none" strokeWidth="1" fill="none" fillRule="evenodd" strokeLinecap="round" strokeLinejoin="round">
                                                        <g transform="translate(-1265.000000, -454.000000)" id="Group" stroke="#D12121" strokeWidth="2">
                                                            <g transform="translate(1263.000000, 450.000000)" id="Shape">
                                                                <path d="M21,14.0199615 L14.6315789,14.0199615 C14.2827675,14.0199615 14,13.7302634 14,13.3729027 L14,5.64705882 C14,5.2896981 14.2827675,5 14.6315789,5 L20.3684211,5 C20.7172325,5 21,5.2896981 21,5.64705882 L21,15.7647059 C21,17.5515095 19.5861624,19 17.8421053,19">

                                                                </path>
                                                                <path d="M10,14.0199615 L3.63157895,14.0199615 C3.28276753,14.0199615 3,13.7302634 3,13.3729027 L3,5.64705882 C3,5.2896981 3.28276753,5 3.63157895,5 L9.36842105,5 C9.71723247,5 10,5.2896981 10,5.64705882 L10,15.7647059 C10,17.5515095 8.58616237,19 6.84210526,19">

                                                                </path>
                                                            </g>
                                                        </g>
                                                    </g>
                                                </Quota>
                                            </DzContent>
                                        </Body>
                                    </ContentContainer>
                                </div>
                            </div>
                        ))
                    }
                </EmblaCarousel>
            </InnerContainer>
        </Section>
    )
}