import styled from "styled-components";
import tw, { theme } from "twin.macro";
import StarIcon from '../../../public/assets/icons/star-icon.svg';


type textProps = {
  $isAlternativeColor?: boolean;
  color?: string;
  $isEnd?: boolean;
}

type titleProps = {
  $isSkeleton?: boolean;
}

export const Container = tw.div`flex relative flex-col h-72 md:h-64 w-full md:w-72 cursor-pointer transform transition duration-300 hover:scale-105`

export const ImageContainer = tw.div`w-full h-2/3 rounded-xl relative overflow-hidden`

export const ImageSkeleton = tw.div`w-full h-full rounded-md bg-[rgba(130,_130,_130,_0.2)] bg-[length:800px_100px] animate-[wave-squares_2s_infinite_ease-out]`

export const TimeContainer = tw.div`flex items-center justify-center absolute transition duration-300 left-0 top-0 w-24 h-10 rounded-br-xl bg-secondary group-hover:scale-105`

export const TimeText = tw.p`text-sm text-primary`

export const Title = styled.p<titleProps>`
${({ $isSkeleton }) => $isSkeleton ? tw`bg-[rgba(130,_130,_130,_0.2)] bg-[length:800px_100px] animate-[wave-squares_2s_infinite_ease-out] text-[transparent]` : tw`text-primary`}
${tw`text-lg font-semibold`}
`

export const DetailContainer = tw.div`w-full h-1/3 mt-4 px-4`;

export const RowContainer = tw.div`flex w-full h-1/2 items-center justify-between`;

export const Text = styled.p<textProps>`
${tw`text-sm mr-2`};
${({ $isAlternativeColor }) => $isAlternativeColor ? tw`text-gray-400` : tw`text-primary`}
${({ $isEnd }) => $isEnd && tw`ml-auto`}
`

export const StrikeText = styled.span<{ $isStrike: boolean }>`
${({ $isStrike }) => $isStrike && tw`line-through`}
`

export const PlusContainer = tw.div`absolute h-full w-full bg-secondary z-10 brightness-75 opacity-0 rounded-md flex flex-col gap-2 items-center justify-center hover:opacity-90 transition duration-500`

export const StyledStarIcon = tw(StarIcon)`
mr-1.5
mb-0.5
`

export const ContainerItem2 = tw.div`w-full relative`

export const DzImageBox = tw.div`
  bg-primary p-[18px] flex flex-col h-[180px] relative z-[0] overflow-hidden 
  rounded-[10px] shadow-lg transition duration-500 
  before:content-[''] before:h-0 before:w-0 before:bg-main
  before:transition-all before:duration-500 before:absolute before:bottom-0 
  before:right-0 before:z-[-1] before:rounded-full
  hover:before:h-full hover:before:w-full hover:before:scale-150
  hover:before:bottom-0 hover:before:right-0 border-2
`;
export const MenuDetail = tw.div`flex items-center mb-3`

export const DzMedia = tw.div`mr-4 w-[60px] min-w-[60px] h-[60px]`

export const FoodImage = tw.img`object-cover`

export const ContentTitle = tw.h5`mb-[3px] duration-500 font-semibold leading-7`

export const ContentTitleLink = tw.a`group-hover:text-white`

export const ContentResume = tw.p`text-sm text-gray-600 group-hover:text-white`

export const MenuFooter = tw.div`mt-auto`

export const RegularPrice = tw.span`text-[13px] text-gray-500 block group-hover:text-white`

export const Price = tw.span`transition duration-500 block text-[18px] text-main font-semibold group-hover:text-white`

export const AddButton = tw.button`bg-main w-[40px] h-[40px] leading-[40px] flex items-center justify-center block absolute bottom-0 right-0 rounded-ss-[10px] transition duration-500 group-hover:bg-white`

export const AddIcon = tw.svg`group-hover:animate-rotate360 group-hover:fill-main`

export const AddIconPath = tw.path`group-hover:fill-main fill-white`

export const DetailSection = tw.section`lg:pt-[50px] sm:pt-[20px] pt-[10px] lg:pb-[100px] pb-[50px] relative bg-primary overflow-hidden`

export const FoodDetailContainer = tw.div`md:grid md:grid-cols-3 content-center	gap-x-6 pb-10`

export const FoodDetailImageContainer = tw.div`w-full px-[15px]`

export const FoodDetailMedia = tw.div`rounded-[10px] overflow-hidden w-full`

export const FoodDetailImage = tw.img`h-full w-full object-cover`

export const FoodDetailContentContainer = tw.div`w-full px-[15px] col-span-2 mt-10 md:mt-0`

export const FoodDetailTag = tw.span`mb-[10px] p-[2px] font-semibold text-sm leading-5 text-[#666666] flex items-center rounded-[10px]`

export const FoodDetailTitle = tw.h2`mb-2 lg:text-4xl sm:text-[2rem] text-[1.75rem] font-bold`

export const FoodDetailRating = tw.div`text-sm flex items-center gap-x-1 leading-[21px] mb-2`

export const FoodDetailSummary = tw.p`text-[15px] my-4`

export const FoodDetailList = tw.ul`flex my-[25px]`

export const FoodDetailItem = tw.li`mr-[45px] text-[15px] font-medium leading-[22px]`

export const FoodDetailContainerButton = tw.div`lg:flex justify-between md:absolute md:bottom-0 w-full md:w-1/3`

export const FoodDetailPrice = tw.span`block text-main text-xl font-semibold leading-[30px] mt-1`

export const FoodDetailQuantity = tw.div`mt-1`

export const FoodDetailSKU = tw.p`text-sm mt-3 italic`