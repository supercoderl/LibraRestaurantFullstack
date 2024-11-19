import styled from "styled-components";
import tw from "twin.macro";

export const Container = tw.div`relative z-30`

const openClassNames = {
    right: tw`translate-x-0`,
    left: tw`translate-x-0`,
    top: tw`translate-y-0`,
    bottom: tw`translate-y-0`
};

const closeClassNames = {
    right: tw`translate-x-full`,
    left: tw`-translate-x-full`,
    top: tw`-translate-y-full`,
    bottom: tw`translate-y-full`
};

const classNames = {
    right: tw`inset-y-1/2 right-0 -translate-y-1/2`,
    left: tw`inset-y-0 left-0`,
    top: tw`inset-x-0 top-0`,
    bottom: tw`inset-x-0 bottom-0`
};

type BackgroundProps = {
    $open: boolean;
}

export const OuterBackground = styled.div<BackgroundProps>`
${({ $open }) => $open ? tw`opacity-100 duration-500 ease-in-out visible` : tw`opacity-0 duration-500 ease-in-out invisible`}
${tw`fixed inset-0 bg-gray-500 bg-opacity-75 transition-all`}
`

export const InnerBackground = styled.div<BackgroundProps>`
${({ $open }) => $open && tw`fixed inset-0 overflow-hidden`}
`

export const InnerContainer = tw.div`absolute inset-0 overflow-hidden`

type MoreClassProps = {
    $side: "top" | "left" | "bottom" | "right";
    $open: boolean;
}

export const Body = styled.div<MoreClassProps>`
  ${tw`pointer-events-none fixed md:w-auto w-[90%] h-fit`}
  ${({ $side }) => classNames[$side]} 
`;

export const Wrapper = styled.div<MoreClassProps>`
  ${tw`pointer-events-auto relative w-full h-full transform transition ease-in-out duration-500`}
  ${({ $side, $open }) => $open ? openClassNames[$side] : closeClassNames[$side]}
  `

export const ContentContainer = tw.div`flex flex-col h-full bg-primary p-5 shadow-xl bg-blue-400 rounded-md`