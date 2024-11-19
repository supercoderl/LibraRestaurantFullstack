import styled from "styled-components";
import tw from "twin.macro";
import Image from 'next/image';


type textType = {
  $color?: string;
  $isSkeleton: boolean;
}

type imageType = {
  $isDarkTheme?: boolean
}

type categoryItemType = {
  $isActive?: boolean
}

export const Container = styled.div`
${tw`bg-no-repeat border-4 bg-cover cursor-pointer relative bg-center opacity-100 transition duration-300 delay-0 rounded-md pt-5 px-5 pb-10 bg-catagoryBackground hover:bg-main`}
`

export const CategoryImageContainer = tw.div`relative z-10 h-[145px] md:h-[245px] mb-[30px] flex justify-center w-full`

export const CategoryContentContainer = styled.div<textType>`
${({ $isSkeleton }) => !$isSkeleton && tw`before:absolute before:top-[2.4rem] before:content-[""] before:left-[40%] before:w-[54px] before:h-[2px] before:bg-productQuantity group-hover:before:bg-white before:transition before:duration-500`}
${tw`text-center relative`}
`

export const Text = styled.p<textType>`
${({ $isSkeleton }) => $isSkeleton ? tw`bg-[rgba(130,_130,_130,_0.2)] bg-[length:800px_100px] animate-[wave-squares_2s_infinite_ease-out] text-[transparent]` : tw`text-primary group-hover:text-white`}
${tw`text-3xl mt-[-40px] mb-2 font-bold transition duration-500`}
`

export const TextQuantity = styled.p<textType>`
${({ $isSkeleton }) => $isSkeleton ? tw`bg-[rgba(130,_130,_130,_0.2)] bg-[length:800px_100px] animate-[wave-squares_2s_infinite_ease-out] text-[transparent]` : tw`text-productQuantity group-hover:text-white`}
${tw`text-lg font-semibold transition duration-500`}
`

export const StyledImage = styled(Image) <imageType>`
  ${tw`border-0 opacity-100 transition duration-300 delay-0 object-contain`}
`

export const ImageSkeleton = tw.div`w-[13vw] h-[14vw] bg-[rgba(130,_130,_130,_0.2)] bg-[length:800px_100px] animate-[wave-squares_2s_infinite_ease-out]`

export const SecondaryContainer = tw.div`w-full md:w-fit md:px-20 mb-10`;

export const CategoryFood = tw.ul`list-none md:flex md:flex-nowrap justify-center p-0`;

export const CategoryFoodItem = styled.li<categoryItemType>`
${tw`relative flex flex-col items-center justify-center cursor-pointer w-20 h-20 md:w-28 md:h-28 rounded-md border-2 border-[rgba(255, 255, 255, 0.3)] shadow-secondaryCategory transition duration-500 hover:scale-90`}
${({ $isActive }) => $isActive && tw`bg-main text-white`}
`;

export const CategoryFoodItemLink = tw.a`flex flex-col justify-center items-center w-auto px-[10px]`

export const CategoryFoodItemText = tw.span`text-center whitespace-nowrap`

export const CategoryFoodItemImage = tw.img`w-[40px] mb-[5px]`