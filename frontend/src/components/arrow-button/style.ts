import tw from "twin.macro";

export const ButtonDiv = tw.div`
flex
items-center
gap-5
`

export const ButtonDivLeft = tw.div`
flex
justify-evenly
items-center
rounded-full
w-12
h-12
bg-green-400
hover:cursor-pointer
hover:bg-main
transition-all
duration-500
`

export const ButtonDivRight = tw.div`
flex
justify-evenly
items-center
bg-orange
rounded-full
w-12
h-12
hover:cursor-pointer
hover:bg-main
transition-all
duration-500
`