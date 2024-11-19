import React from "react";
import styled from "styled-components";
import tw from "twin.macro";


type titleProps ={
  $isBigger?:boolean;
  children?: React.ReactNode;
}

export const Title = styled.p<titleProps>`${tw`text-2xl text-primary font-bold`};
${({$isBigger})=> $isBigger ? tw`text-3xl` : tw`text-2xl`};`

export const TitleLogo = tw.h1`font-bold text-3xl cursor-pointer`;
export const SpanLogo = tw.span`text-main`;
