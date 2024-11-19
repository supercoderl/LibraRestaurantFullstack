import React from "react";
import styled from "styled-components";
import tw from "twin.macro";

type foodProps = {
  $isReservation?: boolean;
  children?: React.ReactNode;
  className?: string;
}

export const Container = tw.div`flex w-full justify-between`

export const BodyContainer = tw.div`flex flex-col w-full items-center `

export const CenterContainer = tw.div`flex flex-col w-11/12 h-full pb-10 items-center justify-center`

export const CartContainer = styled.div`
${tw`absolute w-full top-0 right-0 bg-secondary shadow-2xl sm:max-w-cart 2xl:(relative shadow-none bg-secondary)`}
`

export const MidContainer = tw.div`flex items-center justify-between w-[90%] my-14 sm:(flex justify-between)`

export const ScrollableContainer = styled.div`${tw`flex overflow-scroll w-full h-auto lg:justify-center`}   
  -ms-overflow-style: none;  
  scrollbar-width: none; 
  &::-webkit-scrollbar {
    display:none;
  } 
`
export const FoodCategoriesContainer = tw.div`flex w-[90%] justify-around`

export const FoodContainer = styled.div<foodProps>`
${({ $isReservation }) => $isReservation ? tw`grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 col-span-3 w-full pt-4 gap-y-8 md:max-h-[800px]` : tw`grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 w-full md:w-[90%] gap-8`}
${tw`grid place-items-center md:place-items-start overflow-x-hidden overflow-y-scroll p-4 pb-12`}
`

export const ServiceContainer = tw.section`py-20 w-[90%]`;

export const RowHeader = tw.div`grid grid-cols-1 md:grid-cols-10 lg:grid-cols-8 gap-4`

export const RowHeaderCol = tw.div`col-span-full md:col-span-10 lg:col-span-8`

export const Grid = tw.div`grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6`

export const ServiceHeader = tw.div`mb-[35px]`;

export const HeaderTitle = tw.h2`relative text-3xl mb-5 text-center pb-5 uppercase font-[700] before:content-[''] before:absolute before:bottom-0 before:left-1/2 before:-translate-x-1/2 before:w-[140px] before:bg-[#f70037] after:content-[''] after:absolute after:bottom-[-1px] after:left-1/2 after:-translate-x-1/2 after:w-[45px] after:h-[3px] after:bg-[#f70037]`;

export const TitleSpan = tw.span`text-[#f70037]`;

export const Description = tw.p`text-[#6f6f71] text-center`;

export const SingleService = tw.div`shadow-[0_0_10px_0_rgba(0,_0,_0,_0.1)] rounded-md hover:scale-90 transition duration-500 cursor-pointer`;

export const Part1 = tw.div`pt-[40px] pb-[25px] px-[40px] border-b-2 border-[rgba(0, 0, 0, 0.08)]`;

export const Part1Title = tw.h3`text-[17px] font-[700]`

export const Part2 = tw.div`pt-[30px] pb-[40px] px-[40px]`

export const Part2Description = tw.p`mb-[22px] text-[#6f6f71]`;

export const Part2Link = tw.a`no-underline`;

export const MenuLink = tw.a`underline text-blue-500 cursor-pointer transition duration-300 hover:translate-x-0.5	`

export const Section = tw.section`overflow-hidden w-full pb-10`;

export const InnerContainer = tw.div`container mx-auto`

export const ContentContainer = tw.div`md:mr-[30px] md:w-[1200px] text-justify`

export const Body = tw.div`flex lg:flex-row flex-col bg-primary relative transition duration-300`

export const DzMedia = tw.div`rounded-lg xl:w-[570px] xl:min-w-[570px] lg:w-[450px] lg:min-w-[450px] w-full relative overflow-hidden`

export const Image = tw.img`object-cover`

export const DzContent = tw.div`lg:pt-5 lg:pb-[30px] lg:pl-[30px] py-5 relative w-full flex flex-col`

export const Review = tw.div`relative mb-[15px] text-base`

export const ReviewText = tw.p`xl:text-[18px] text-base leading-[32px] font-medium text-[#222222] text-primary`

export const ReviewInfo = tw.div`pl-[15px] xl:mt-[60px] relative z-[1] after:content-[''] after:bg-main after:rounded after:h-[5px] after:w-[50px] after:absolute after:top-[25px] after:left-[-22px] after:rotate-90`

export const Reviewer = tw.h5`font-bold leading-[32px] lg:text-[25px] text-lg`

export const Tag = tw.span`leading-[21px] text-sm block text-main`

export const Quota = tw.svg`absolute lg:right-[35px] right-5 bottom-[2vw] w-[6vw] h-[6vw] fill-main inline-flex items-center`

export const CircleButtonIcon = tw.a`w-10 h-10 flex items-center justify-center rounded-full bg-main`