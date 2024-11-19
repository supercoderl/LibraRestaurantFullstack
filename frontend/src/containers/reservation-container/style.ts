import React from "react";
import styled from "styled-components";
import tw from "twin.macro"

type ActionProps = {
    isOutlined?: boolean;
    children?: React.ReactNode;
    className?: string;
}

export const Container = tw.div`flex w-full justify-between`

export const BodyContainer = tw.div`flex flex-col w-full items-center `

export const CenterContainer = tw.div`flex flex-col w-11/12 h-full pb-10 items-center justify-center`

export const ContentContainer = tw.div`md:grid md:grid-cols-4 w-full`;

export const OrderContainer = tw.div`mt-4 hidden md:flex col-span-1 flex-col gap-8`;

export const ModalHeader = tw.h3`font-bold text-xl`;

export const ModalBody = tw.div`w-full md:w-[400px] py-3`

export const ModalBodyStatus = tw.i`text-secondary`;

export const ModalBodyFormContainer = tw.form`flex flex-col gap-3 justify-center items-center pt-5`

export const ModalBodyFormGroup = tw.div`flex flex-col items-baseline w-full`;

export const ModalBodyFormLabel = tw.label`font-[600] my-[5px]`

export const ModalBodyFormInput = tw.input`outline-0 border-2 border-[#E99F4C] w-full py-[12px] px-[10px] rounded-[4px] text-[15px] focus:translate-y-modalFocusInput shadow-modalInput focus:shadow-modalFocusInput`;

export const ModalActionContainer = tw.div`mt-5 flex items-center justify-between gap-2 w-full`

export const ModalActionButton = styled.button<ActionProps>`
${({ isOutlined }) => !isOutlined && tw`bg-main`};
${tw`flex justify-center items-center py-[10px] gap-1 w-[50%] rounded-[5px] border-2 border-main cursor-pointer transition duration-500 hover:bg-transparent active:scale-95`}
`

export const ModalActionText = styled.p<ActionProps>`
${({ isOutlined }) => isOutlined ? tw`text-main` : tw`text-white`}
${tw`font-[700] text-[1em] transition duration-500 group-hover:text-main`}
`;

export const ModalActionSvg = styled.svg<ActionProps>`
${({ isOutlined }) => isOutlined ? tw`fill-main` : tw`fill-white`}
${tw`transition duration-500  group-hover:fill-main`}
`