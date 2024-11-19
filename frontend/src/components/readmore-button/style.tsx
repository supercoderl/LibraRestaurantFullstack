import tw from "twin.macro";

export const Button = tw.button`bg-main mt-5 text-white p-[0.35em] pl-[1.2em] text-[17px] font-semibold rounded-md border-0 tracking-wider flex items-center shadow-readmore overflow-hidden relative h-[2.8em] pr-[3.3em] cursor-pointer`;

export const Icon = tw.div`bg-white ml-[1em] absolute flex items-center justify-center h-[2.2em] w-[2.2em] rounded-md shadow-readmoreIcon right-[0.3em] transition-all	 duration-300 group-hover:w-readmoreIcon group-active:scale-95`;

export const Svg = tw.svg`w-[1.1em] transform transition-transform duration-300 text-main group-hover:translate-x-readmoreSvg `