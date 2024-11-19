
import { PopupContainer, Select } from "./style"
import { languageResources } from "@/utils/i18next";
import { languages } from "@/model/languages";
import { useRouter } from "next/router";

export const LanguageSelector = () => {
    const router = useRouter();

    const changeLocale = (locale: string) => {
        const { pathname, asPath, query } = router;
        router.push({ pathname, query }, asPath, { locale });
    }

    return (
        <PopupContainer>
            <Select id="countries" onChange={(e) => changeLocale(e.target.value)}>
                {
                    Object.keys(languageResources).map((item: string, index: number) =>
                        <option key={index} value={languages[item]?.code}>{languages[item]?.name}</option>
                    )
                }
            </Select>
        </PopupContainer>
    )
}