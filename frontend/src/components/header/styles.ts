import styled from "styled-components";
import tw from "twin.macro";


export const Container = tw.div`flex w-full gap-10 h-24 bg-primary items-center md:justify-between`


export const RightSideContainer = tw.div`w-full flex-1 relative`

export const LeftSideContainer = tw.div`w-max items-center justify-around hidden md:flex`

export const TitleContainer = tw.div`hover:cursor-pointer`

export const IconContainer = tw.div`flex h-16 w-fit items-center justify-center hover:cursor-pointer gap-4`