import styled from "styled-components";
import tw from "twin.macro";

type ToolbarProps = {
    $isRow?: boolean;
}

export const Container = tw.div`bg-white shadow-md text-[#595959] h-[106px]`;

export const TextContainer = tw.div`p-4 text-center justify-center`

export const Text = tw.h3`text-[#22075e] mb-0 font-semibold`;

export const PreviewStateContainer = tw.div`text-[#595959] mb-[5px]`;

export const PreviewContentContainer = tw.div`flex items-center justify-between`

export const Space = tw.div`p-4`

export const PreviewContainer = tw.div`bg-white shadow-md h-[380px]`

export const RecentContainer = tw.div`bg-white shadow-md`

export const PreviewTextContainer = tw.div`p-4`;

export const PreviewText = tw.h3`text-[#22075e] mb-[15px] text-lg font-semibold`

export const PreviewTextProgress = tw.h3`text-[#22075e] mb-[30px] text-lg font-semibold`

export const RecentText = tw.h3`text-[#22075e] mb-[5px] text-lg font-semibold`

export const TableContainer = tw.div`bg-white shadow-md text-[#595959] p-4`

export const ToolbarContainer = styled.div<ToolbarProps>`
${({ $isRow }) => $isRow ? tw`flex-row` : tw`flex-col`}
${tw`bg-white shadow-md p-6 mb-4 flex items-center justify-between`}
`

export const AlignContainer = tw.div`text-[#595959] flex flex-wrap md:flex-nowrap items-center gap-3`;

export const ActionContainer = tw.div`flex items-center justify-center`;

export const HeaderText = tw.h2`text-xl font-semibold`

export const DetailContainer = tw.div`p-5 mx-auto`;

export const DetailWrapper = tw.div`mx-auto flex flex-wrap justify-between`;

export const DetailContent = tw.div`lg:w-[60%] w-full lg:pl-10 lg:py-6 mt-6 lg:mt-0`;

export const DetailTitleText = tw.h1`text-gray-900 text-3xl font-medium mb-1`;

export const DetailPreviewContainer = tw.div`flex mb-4`;

export const DetailPreviewStartContainer = tw.span`flex items-center`;

export const DetailPreviewStartText = tw.span`text-gray-600 ml-3`;

export const DetailPrice = tw.h2`text-main text-3xl mb-3`

export const DetailTitleContainer = tw.h4`font-semibold flex items-center text-lg gap-1`

export const DetailRecipe = tw.p``

export const DetailSummary = tw.p`leading-relaxed text-justify`

export const ProgressContainer = tw.div`mx-auto flex items-center justify-center mb-1`