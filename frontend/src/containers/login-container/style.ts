import tw from "twin.macro";

export const Container = tw.div`flex max-w-[1200px] rounded-md my-4 bg-white m-auto w-full min-w-[320px] md:shadow-[rgba(0,_0,_0,_0.35)_0px_5px_15px]`;

export const Svg = tw.svg`h-[40px] w-[40px] bg-[#5138ee]`;

export const LoginForm = tw.form`w-1/2 pb-10 flex flex-col items-center flex-1`;

export const LoginFormInner = tw.div`w-[80%] md:w-[65%] flex md:block flex-col items-center justify-center`;

export const Logo = tw.img`w-24 h-24 object-contain`

export const Title = tw.h1`text-[2.4rem] font-[600] my-2`

export const GoogleSvg = tw.svg`h-[20px] flex mr-[10px]`;

export const GoogleButton = tw.a`flex w-full items-center justify-center py-2 rounded-3xl text-[#333] border-[1px] border-[#d6d6d6] mt-8 mb-[20px]`

export const Seperator = tw.div`text-center relative md:mt-[30px] mb-[20px] text-[#969696] after:content-[''] after:absolute after:w-full after:h-[1px] after:bg-[#d6d6d6] after:left-0 after:top-2/4 z-0`

export const SeperatorText = tw.span`bg-white z-10 relative px-[10px]`

export const FormGroup = tw.div`w-full relative flex flex-col mb-[20px]`

export const FormLabel = tw.label`font-[500] text-[#333] mb-[10px]`

export const FormInput = tw.input`py-[13px] px-[20px] border-[1px] border-[#d6d6d6] rounded-[50px] font-[600] text-[#333] transition duration-300 focus:outline-none focus:border-[#5138ee]`

export const SingleRow = tw.div`w-full relative flex mb-[20px] justify-between pt-[5px]`

export const CustomCheck = tw.div`flex items-center justify-center`

export const CustomCheckInput = tw.input`h-5 w-5 m-0 p-0 transition duration-300 opacity-100 appearance-none border-2 border-main rounded-[3px] bg-white relative mr-[10px] cursor-pointer checked:bg-main checked:before:content-[''] checked:before:absolute checked:before:h-[2px] checked:before:bg-white checked:before:w-[8px] checked:before:top-[9px] checked:before:left-[1px] checked:before:rotate-[44deg] checked:after:content-[''] checked:after:absolute checked:after:h-[2px] checked:after:bg-white checked:after:w-[12px] checked:after:top-[6px] checked:after:left-[4.8px] checked:after:rotate-[-55deg] focus:outline-none`

export const CustomCheckLabel = tw.label`text-[#333] font-[500] cursor-pointer`

export const Link = tw.a`text-main font-[700] no-underline hover:underline`

export const CTA = tw.button`text-white flex items-center justify-center gap-2 w-full no-underline border-[1px] border-main py-2 rounded-3xl bg-main hover:bg-transparent hover:text-main transition duration-300`

export const Onboarding = tw.div`hidden md:block flex-1 bg-[rgb(245_245_245)] w-1/2`;

export const SwiperContainer = tw.div`w-full h-full mx-auto`

export const SlideImg = tw.img`w-full h-[25vw] mb-10`

export const SlideContent = tw.div`w-[60%] self-center`

export const SlideText = tw.h2`text-[22px] font-[500] mb-[15px]`

export const SlideDescription = tw.p`text-[16px] font-[300]`
