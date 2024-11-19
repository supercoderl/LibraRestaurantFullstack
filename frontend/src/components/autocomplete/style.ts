import styled from "styled-components";
import tw from "twin.macro";

type ContainerProps = {
    $isOpen: boolean;
}

export const Container = styled.ul<ContainerProps>`
${({ $isOpen }) => !$isOpen && tw`hidden`}
${tw`w-full absolute border-[1px] z-10 bg-primary rounded-md mt-2`}
`

export const LoadingContainer = tw.div`w-full flex justify-center items-center p-4`

export const ItemContainer = tw.div`px-4 py-2 w-full hover:bg-gray-100 cursor-pointer transition duration-300 flex items-center gap-x-2`

export const Text = tw.p``