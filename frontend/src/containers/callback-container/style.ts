import styled from "styled-components";
import tw from "twin.macro";

export const Container = tw.div`w-full h-screen p-4 md:p-0 flex items-center justify-center`

export const Card = tw.div`w-full md:w-[400px] relative py-8 overflow-hidden my-24 shadow-2xl rounded-xl flex items-center flex-col`

export const Logo = tw.h2`z-10 uppercase font-bold text-[5vw] md:text-[1.5vw] gap-1 flex items-center p-0 m-0`

export const LogoRed = tw.span`text-red-500`

export const LogoImg = tw.img`w-12`

export const Icon = tw.img`w-full`

export const IconContainer = tw.div`bg-white w-[20vw] md:w-[5vw] absolute top-[22vw] md:top-[6vw] z-10 rounded-full shadow-xl`

export const Curved = tw.div`absolute -top-10 left-1/2 -translate-x-1/2 w-[150%] bg-blue-50 h-[42vw] md:h-[11vw] inset-0 rounded-[50%]`

export const ContentContainer = tw.div`z-10 mt-32 px-4 w-full text-center`

export const StatusText = tw.h3`font-bold text-2xl`

export const BillContainer = tw.div`w-full shadow-[rgba(99,_99,_99,_0.2)_0px_2px_8px_0px] mt-4 rounded-md py-6 px-5 flex flex-col gap-3`

export const Title = tw.h4`font-semibold text-xl`

export const Subtitle = tw.p`text-sm text-red-500`

export const FlexContainer = tw.div`flex items-center justify-center gap-1`

export const ItemContainer = tw.div`border-y-[1px] py-2 flex flex-col gap-1.5`

export const TextItemContainer = tw.div`flex justify-between items-center`

type TextItemProps = {
    isBold?: boolean
}

export const TextItem = styled.p<TextItemProps>`
${({ isBold }) => isBold ? tw`font-bold text-gray-700` : tw`text-gray-500`}
`

export const TextFooter = tw.p`mt-4 text-xs text-gray-500 px-4`

export const HomeButton = tw.button`uppercase w-fit self-center font-[600] flex-1 md:flex-none text-xs cursor-pointer transition duration-500 bg-transparent pb-[2px]! border-0 border-b-2 border-main opacity-75 hover:border-[#40b3ff] hover:opacity-100`