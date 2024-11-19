import styled from "styled-components";
import tw from "twin.macro";


export const Container = tw.main`w-full rounded-md border-2`;

export const Component = tw.div`w-full rounded-md shadow-order pb-1`;

export const ComponentInfo = tw.div`w-[80%] my-[15px] mx-auto`;

export const Title = tw.h2`text-2xl font-bold text-darkBlue`;

export const EmptyContainer = tw.div`flex flex-col bg-veryPaleBlue py-[18px] px-[15px] mt-[16px] items-center gap-1.5`;

export const EmptyText = tw.i`text-secondary`

export const Plan = tw.div`w-full bg-veryPaleBlue p-[10px] rounded-md mt-[16px] mb-[20px] grid grid-cols-4 items-center cursor-pointer transition duration-500 hover:scale-105`;

export const PlanImage = tw.div`col-span-1`;

export const PlanContent = tw.div`col-span-2`;

export const PlanContentName = tw.h4`text-sm font-bold text-darkBlue truncate`;

export const PlanContentPrice = tw.p`text-sm text-desaturatedBlue`;

export const PlanContentQuantity = tw.p`text-sm font-semibold text-desaturatedBlue`

export const PlanAction = tw.div`col-span-1 cursor-pointer ml-auto hover:scale-110 transition duration-500`;

export const Image = tw.img`w-[80%] rounded-lg`;

export const ButtonCheckout = tw.button`w-full h-10 text-white flex items-center justify-center gap-2 bg-main rounded-md text-[rgb(19, 19, 19)] font-semibold border-0 relative cursor-pointer transition duration-500 shadow-buttonCheckout pl-[8px] active:scale-95`;

type SvgProps = {
    loading: boolean;
}

export const SvgCheckout = styled.svg<SvgProps>`
${({loading}) => loading ? tw`animate-spin` : tw`transform`},
${tw`h-[20px] w-[20px] transition duration-500 group-hover:rotate-360`}
`

export const PriceContainer = tw.div`w-full bg-veryPaleBlue py-[10px] px-[15px] rounded-md mt-[16px] mb-[20px] items-center cursor-pointer transition duration-500 hover:scale-105`

export const NormalPrice = tw.p`font-semibold`