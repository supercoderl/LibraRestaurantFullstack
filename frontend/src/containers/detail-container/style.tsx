import styled from "styled-components"
import tw from "twin.macro"

type TabProps = {
    $isActive: boolean
}

export const Container = tw.div`flex w-full justify-between`

export const BodyContainer = tw.div`flex flex-col w-full items-center `

export const CenterContainer = tw.div`flex flex-col w-11/12 h-full pb-10 items-center justify-center`

export const TabContainer = tw.div`w-full bg-primary rounded-sm overflow-hidden relative`

export const Tab = tw.div`p-[10px_20px_15px_20px]`

export const TabLinks = tw.div`flex border-b-[1px] border-[#f0f0f0]`

export const TabLink = styled.button<TabProps>`
${({ $isActive }) => $isActive ? tw`text-main after:w-full after:left-0` : tw`text-[#ccc] after:w-0 after:left-1/2`}
${tw`flex items-center gap-x-1 font-semibold px-[30px] py-[15px] cursor-pointer relative transition duration-300 after:content-[''] after:absolute  after:h-[3px] after:bottom-[-1px] after:bg-main after:duration-500`}
`

export const TabContent = styled.div<TabProps>`
${({ $isActive }) => $isActive ? tw`block` : tw`hidden`}
${tw`animate-fadeInUp p-[5px_10px_15px_10px]`}
`

export const TabTitle = tw.h2`text-2xl font-bold my-2`

export const CommentList = tw.div``

export const CommentItem = tw.div``

export const CommentBody = tw.div`relative min-h-[95px] border-b border-[#2222221a] md:pl-[100px] pl-[75px] md:pb-[30px] pb-[15px] md:mb-[30px] mb-5`

export const Avatar = tw.img`md:h-[80px] h-[60px] md:w-[80px] w-[60px] rounded-full left-0 absolute`

export const Name = tw.cite`not-italic text-base font-bold mb-2 block`

export const CommentRatingContainer = tw.div`mb-[10px] text-sm flex items-center gap-x-1`

export const CommentText = tw.p`italic text-gray-500`

export const AddReviewTitle = tw.h3`xl:mb-[30px] mb-5 pb-3 relative text-2xl font-bold`

export const ReviewForm = tw.form`mx-[-10px]`

export const FormGroup = tw.div`mb-5 px-[10px] sm:w-[50%] w-full relative`

export const Input = tw.input`h-16 py-[15px] bg-primary px-5 w-full text-[15px] rounded-[6px] border-2 border-[#c4c4c6] focus:bg-primary duration-500 focus:outline-none focus:border-[#1a73e8]`

export const Label = tw.label`text-[#a4a4a6] absolute left-8 pointer-events-none -translate-y-1/2 top-1/2 transition-all peer-focus:-translate-y-2.5 peer-focus:text-primary peer-focus:top-0 peer-focus:scale-90 peer-focus:bg-primary peer-focus:px-[0.5rem] peer-valid:-translate-y-2.5 peer-valid:top-0 peer-valid:scale-90 peer-valid:bg-primary peer-valid:px-[0.5rem] peer-valid:text-primary`

export const FormRating = tw.div`flex px-[10px] mb-10`

export const FormRatingList = tw.div`flex gap-x-2`

export const FormCommentGroup = tw.div`mb-5 px-[10px] w-full relative`

export const TextArea = tw.textarea`h-[120px] py-[15px] bg-primary border-[#c4c4c6] px-5 w-full text-[15px] rounded-[6px] border-2 focus:border-primary focus:bg-primary duration-500`

export const CommentLabel = tw.label`text-[#a4a4a6] absolute left-8 pointer-events-none top-4 transition-all peer-focus:-translate-y-2.5 peer-focus:top-0 peer-focus:scale-90 peer-focus:bg-primary peer-focus:px-[0.5rem] peer-focus:text-primary peer-valid:-translate-y-2.5 peer-valid:top-0 peer-valid:scale-90 peer-valid:bg-primary peer-valid:px-[0.5rem] peer-valid:text-primary`

export const SubmitButton = tw.button`flex items-center justify-center gap-1.5 w-full md:w-1/5 dark:bg-white dark:text-black py-4 dark:hover:bg-main dark:hover:text-white hover:bg-main hover:text-white border border-main font-medium text-base font-medium leading-4 text-gray-800 duration-500 transition`