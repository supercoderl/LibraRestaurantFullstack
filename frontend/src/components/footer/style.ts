import tw from "twin.macro";

export const FooterContainer = tw.footer`md:px-20 relative bg-tertiary w-full before:content-[''] before:absolute before:left-0 before:top-[150px] md:before:top-[100px] before:h-[1px] before:w-full before:bg-[#afafb6]`;

export const TitleDetail = tw.div`flex items-center gap-3`

export const TitleLogo = tw.h1`font-bold text-3xl cursor-pointer text-white`;

export const SpanLogo = tw.span`text-[rgb(19_225_250)]`;

export const TitleImage = tw.img`w-10`;

export const Content = tw.div`m-auto px-8 md:px-[40px] pt-[30px] md:pb-[40px]`;

export const ContentTop = tw.div`flex flex-col md:flex-row gap-5 md:gap-0 items-center justify-between mb-[50px]`;

export const MediaIcon = tw.div`flex`;

export const MediaText = tw.a`flex items-center justify-center bg-[#FCFAEE] h-[40px] w-[40px] mx-[8px] rounded-full text-center text-white text-[17px] no-underline transition duration-500 hover:translate-y-1`;

export const LinkBoxes = tw.div`w-full flex flex-wrap md:flex-nowrap justify-between gap-y-5 md:gap-y-0 `;

export const Box = tw.ul`basis-1/2 md:basis-[unset] md:w-[calc(100%_/_5_-_10px)]`;

export const LinkName = tw.li`text-white text-[18px] font-[400] mb-[10px] relative before:content-[''] before:absolute before:left-0 before:bottom-[-2px] before:h-[2px] before:w-[35px] before:bg-white`;

export const LinkItem = tw.li`my-[6px] text-white`

export const LinkNameText = tw.a`text-white text-[14px] font-[400] no-underline opacity-80 transition duration-500 hover:opacity-100 hover:underline`

export const InputBox = tw.ul`w-full md:w-[calc(100%_/_5_-_10px)] md:mr-[55px]`;

export const Input = tw.input`h-[40px] w-full outline-none border-2 border-[#afafb6] bg-tertiary rounded-[4px] px-[15px] text-[15px] text-white mt-[5px] placeholder-[#afafb6]`

export const Button = tw.button`w-full py-2 rounded-sm bg-white text-[#140b5c] text-[18px] border-0 font-[500] my-4 opacity-80 cursor-pointer transition duration-500 hover:opacity-100`

export const BottomDetail = tw.div`w-full bg-tertiary`

export const BottomText = tw.div`m-auto py-[20px] px-[40px] flex flex-col md:flex-row items-center gap-5 md:gap-0 justify-between`

export const BottomTextSpan = tw.span`font-[300] text-white opacity-80 no-underline`

export const BottomTextA = tw.a`font-[300] text-white opacity-80 no-underline hover:opacity-100 hover:underline mr-[10px]`


