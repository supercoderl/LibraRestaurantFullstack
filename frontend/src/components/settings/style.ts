import styled from "styled-components";
import tw from "twin.macro";

export const Container = tw.div`fixed right-0 top-1/2 z-20`

export const ButtonSetting = tw.button`w-10 h-10 rounded-sm flex justify-center items-center bg-red-500`;

export const Icon = tw.div`animate-spinSlow`

export const TabContainer = tw.div`grid gap-y-8`

export const Tab = tw.div`w-full p-3 bg-gray-50 dark:bg-[rgb(59_46_46)] shadow-inner`

export const Title = tw.h4`font-semibold text-[14px]`

export const ModeContainer = tw.div`mt-4 flex gap-x-3 md:gap-x-5`

type ModeProps = {
    $type: string;
    $isActive: boolean;
}

export const ModeButton = styled.button<ModeProps>`
${({ $type }) => $type === "light" ? tw`border-orange` : $type === "dark" ? tw`border-black` : tw`border-blue-500`}
${({ $isActive }) => $isActive && tw`bg-[rgb(255,_98,_35)] text-white border-[rgb(255,_98,_35)]!`}
${tw`w-[30%] md:w-[8vw] text-sm md:text-base rounded-sm flex items-center justify-center gap-x-1 md:gap-x-2 rounded-sm border-[1px] transition duration-500 md:py-1.5 py-1`}
`

export const LanguageContainer = tw.div`mt-4 grid gap-3 grid-cols-2 md:grid-cols-3 justify-items-center`

type LanguageProps = {
    $isActive: boolean;
}

export const LanguageButton = styled.button<LanguageProps>`
${({ $isActive }) => $isActive && tw`bg-[rgb(255,_98,_35)] text-white border-[rgb(255,_98,_35)]!`}
${tw`w-full md:w-[9vw] rounded-sm flex items-center justify-start gap-x-2 rounded-sm border-[1px] transition duration-500 px-2 md:px-3 py-1.5`}
`

export const LanguageFlag = tw.img`w-4 h-4 object-contain`