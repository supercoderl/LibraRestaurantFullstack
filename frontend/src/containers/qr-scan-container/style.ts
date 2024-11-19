import styled from "styled-components";
import tw from "twin.macro";

export const Container = tw.div`flex w-full justify-between`;

export const BodyContainer = tw.div`flex flex-col w-full items-center `;

export const CenterContainer = tw.div`flex flex-col w-11/12 h-full pb-10 items-center justify-center`;

export const TitleContainer = tw.div`py-10`;

export const ModalBodyContent = tw.div`flex flex-col items-center justify-center`

export const Text = tw.h2`text-xl font-semibold mt-4`

export const ContinueButton = tw.button`mt-4 flex items-center cursor-pointer rounded-sm font-[500] text-[17px] p-2 pr-3.5 text-white bg-[linear-gradient(to_right,_#219b4e,_#818008,_#5e8e20)] border-0`

export const ContinueSvg = tw.svg`mr-[5px] rotate-[30deg] transition-transform duration-500 ease-[cubic-bezier(0.76,_0,_0.24,_1)] group-hover:translate-x-[5px] group-hover:rotate-[180deg]`

export const ContinueText = tw.span`transition-transform duration-500 ease-[cubic-bezier(0.76,_0,_0.24,_1)] group-hover:translate-x-[5px]`