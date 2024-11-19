import tw from "twin.macro";

export const Section = tw.section` bg-white rounded-[10px] px-5`;

export const Container = tw.div`text-center m-auto text-[#4a4a4a] mb-5`;

export const LogoContainer = tw.div`text-center pb-5 flex items-center justify-center`;

export const Image = tw.img`w-[15vw] object-contain`

export const InfoContainer = tw.div`flex justify-between text-left`

export const InvoiceText = tw.p`font-bold text-primary`

export const List = tw.ul`m-0`

export const InvoiceInfo = tw.div`flex justify-between items-center border-y-[1px] border-red-500 my-5 py-3`

export const Title = tw.p`font-bold text-[18px]`;

export const InvoiceTime = tw.div`text-left`

export const Medium = tw.span`font-semibold`

export const TableContainer = tw.div`py-0.5 w-full`

export const Table = tw.table`border mt-4 w-full rounded-sm`;

export const TableItem = tw.th`p-2 font-normal text-left`

export const Total = tw.td`text-primary font-bold text-left p-2 text-red-500`;

export const PaymentInfoContainer = tw.div`flex justify-between my-5 text-left`;

export const FooterContainer = tw.div`border-t-[1px] border-red-500`;

export const FooterRow = tw.div`flex justify-between mt-3`

export const InfoText = tw.h5`font-bold my-4`

export const LightLink = tw.a`text-gray-500`
