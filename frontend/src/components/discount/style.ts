import styled from "styled-components";
import tw from "twin.macro";

type CounponProps = {
    $isActive: boolean;
}

export const Counpon = styled.div<CounponProps>`
${({ $isActive }) => !$isActive && tw`grayscale`}
${tw`cursor-pointer hover:scale-95 transition duration-300 w-full h-[8vw] rounded-[5px] filter drop-shadow-coupon overflow-hidden m-auto flex items-stretch relative uppercase before:content-[''] before:absolute before:top-0 before:w-1/2 before:h-full after:content-[''] after:absolute after:top-0 after:w-1/2 after:h-full before:left-0 before:bg-customRadialBefore after:right-0 after:bg-customRadialAfter`}
`;

export const Left = tw.div`flex items-center justify-center w-[25%] border-r-2 border-dashed border-[rgba(0,_0,_0,_0.13)] z-10`

export const LeftItem = tw.div`rotate-[-90deg] whitespace-nowrap font-bold text-[10px] pt-3`;

export const Center = tw.div`flex items-center justify-center grow text-center z-10 w-[45%]`;

export const Right = tw.div`flex items-center justify-center w-[30%] bg-customRadialRight z-10`

export const RightItem = tw.div`text-[1.5rem] font-[400] rotate-[-90deg] font-barcode pb-2`

export const CenterTitle = tw.h2`bg-black text-[gold] px-[10px] w-fit mx-auto text-[0.8rem] whitespace-nowrap`;

export const CenterCoupon = tw.h3`text-[0.7rem] my-1`;

export const CenterSmall = tw.p`text-[0.5rem] font-[600] text-wrap tracking-widest`