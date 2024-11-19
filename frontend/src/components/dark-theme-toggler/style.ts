import styled from "styled-components";
import tw from "twin.macro";

type themeType = {
  $isdark: boolean;
}

export const Container = styled.label`
  ${tw`fixed m-0 right-5 bottom-5 cursor-pointer`} 
`

export const SwitchInput = tw.input`
  block bg-[hsl(210,_90%,_70%)] rounded-[0.75em] cursor-pointer
  shadow-switchInput outline-transparent relative w-[3em] h-[1.5em] 
  appearance-none focus-visible:shadow-focusVisible before:content-[''] 
  before:block before:absolute after:content-[''] after:block after:absolute 
  checked:bg-[hsl(290,_90%,_40%)] checked:before:bg-[#121212] 
  checked:after:bg-[hsl(0,_0%,_100%)] checked:after:translate-x-[1.5em]
  before:bg-white before:rounded-[inherit] before:inset-0 before:transition-colors before:duration-300 before:ease-[cubic-bezier(0.76,_0.05,_0.24,_0.95)] 
  after:bg-[hsl(0,_0%,_0%)] after:rounded-full after:shadow-[0.05em_0.05em_0.05em_hsla(223,_90%,_10%,_0.1)] after:top-[0.125em] after:left-[0.125em] after:w-[1.25em] after:h-[1.25em]
  after:transition-colors after:transition-transform after:duration-300 after:ease-[cubic-bezier(0.76,_0.05,_0.24,_0.95)] after:z-10
  `

export const SwitchIcon = styled.svg<themeType>`
  ${({ $isdark }) => $isdark && tw`rotate-[30deg]`}
  ${tw`absolute block top-[0.375em] w-[0.75em] h-[0.75em] duration-300 transition-transform`}
`

export const SwitchPolyline = styled.polyline<themeType>`
  ${({ $isdark }) => $isdark && tw`ease-custom-bezier-out`}
`