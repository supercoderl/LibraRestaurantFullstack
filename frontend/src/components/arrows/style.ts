import tw from "twin.macro";

export const Container = tw.div`relative w-full flex h-[25vw] md:h-[8vw] justify-center`

export const Text = tw.h3`font-semibold text-lg text-center mt-12! text-[#6A9C89]`

export const Chevron = tw.div`absolute w-[2.1rem] h-[0.48rem] opacity-0 scale-[0.3] before:content-[''] before:absolute before:top-0 before:h-full before:w-1/2 before:bg-[#6A9C89] before:left-0 before:skew-y-[30deg] after:content-[''] after:absolute after:top-0 after:h-full after:w-1/2 after:bg-[#6A9C89] after:right-0 after:w-1/2 after:skew-y-[-30deg]`