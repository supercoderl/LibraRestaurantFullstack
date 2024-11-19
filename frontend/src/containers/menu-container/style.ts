import tw from "twin.macro";

export const Section = tw.section`overflow-hidden relative bg-white`

export const Container = tw.div``

export const BodyContainer = tw.div`flex flex-col w-full items-center `

export const CenterContainer = tw.div`flex flex-col w-11/12 h-full pb-10 items-center justify-center`

export const InnerSectionWrapper = tw.div`flex md:mb-[50px]`

export const Row = tw.div`relative grid grid-cols-1 md:grid-cols-2 w-full`

export const FoodColumn = tw.div`w-full px-[15px]`

export const MenuHead = tw.div`mb-[25px]`

export const MenuHeadTitle = tw.h4`font-lobster text-[34px] font-normal text-main`

export const DzShopCard = tw.div`relative overflow-hidden mb-[30px] p-0 shadow-none z-10 transition duration-500 cursor-pointer md:p-4 rounded-md hover:bg-white hover:shadow-[rgba(0,_0,_0,_0.35)_0px_5px_15px]`

export const DzContent = tw.div`flex flex-col w-full`

export const DzHead = tw.div`flex justify-between items-center mb-3`

export const DzHeadTitle = tw.span`lg:text-lg text-base lg:leading-7 font-semibold`

export const DzHeadTitleLink = tw.a`max-w-[280px] text-ellipsis overflow-hidden block whitespace-nowrap duration-500 hover:text-main`

export const DzHeadImageLine = tw.span`block flex-1 h-[1px] mx-[15px] border-[1px] border-dashed border-[#7d7d7d]`

export const DzHeadPrice = tw.span`text-main font-semibold text-xl leading-7`

export const DzBody = tw.p`text-[15px] leading-[21px] font-normal text-gray-600`

export const ImageLeft = tw.img`animate-move absolute top-[30vw] md:left-[-20vw] left-[-40vw] opacity-70`

export const ImageRight = tw.img`animate-move absolute md:right-[-15vw] right-[-40vw] md:top-0 top-[30%] z-0 2xl:w-[600px] md:w-[500px] opacity-70`

export const ImageRight2 = tw.img`animate-move absolute md:w-[30%] w-[60%] right-[-10vw] bottom-0 z-0 opacity-70`