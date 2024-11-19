import { ButtonSetting, Container, Icon, LanguageButton, LanguageContainer, LanguageFlag, ModeButton, ModeContainer, Tab, TabContainer, Title } from "./style"
import SettingIcon from "../../../public/assets/icons/setting-icon.svg";
import React, { useContext, useEffect, useState } from "react";
import Drawer from "../drawer";
import { languageResources } from "@/utils/i18next";
import { languages } from "@/model/languages";
import { useRouter } from "next/router";
import { stringToColor } from "@/utils/colors";
import { ThemeContext } from "@/theme/theme-provider";
import { themes } from "@/model/themes";
import { useTranslation } from "react-i18next";
import { Language } from "@/type/Language";

export const Settings = () => {
    const [open, setOpen] = useState(false);
    const themeContext = useContext(ThemeContext);
    const router = useRouter();
    const { i18n, t } = useTranslation();

    const changeLocale = (locale?: string) => {
        if (locale) {
            const { pathname, asPath, query } = router;
            router.push({ pathname, query }, asPath, { locale });
        }
    }

    const handleToggleTheme = (theme: string) => {
        // Only toggle if the selected theme is different from the current theme
        if (themeContext.theme !== theme) {
            themeContext.toggleTheme(); // Pass the new theme value
        }
    };

    return (
        <Container>
            <ButtonSetting onClick={() => setOpen(true)}>
                <Icon><SettingIcon width={22} height={22} /></Icon>
            </ButtonSetting>

            <Drawer open={open} setOpen={setOpen} side="right">
                <TabContainer>
                    <Tab>
                        <Title>{t("mode")}</Title>
                        <ModeContainer>
                            {
                                themes(t).map((item, index) => (
                                    <ModeButton
                                        key={index}
                                        $isActive={themeContext.theme === item.value}
                                        $type={item.value}
                                        onClick={() => handleToggleTheme(item.value)}
                                    >
                                        {item.icon ? <item.icon /> : null}
                                        {item.label}
                                    </ModeButton>
                                ))
                            }
                        </ModeContainer>
                    </Tab>

                    <Tab>
                        <Title>{t("language")}</Title>
                        <LanguageContainer>
                            {
                                Object.keys(languageResources).map((item: string, index: number) => (
                                    <RenderLanguageButton
                                        key={index}
                                        setOpen={setOpen}
                                        changeLocale={changeLocale}
                                        item={languages[item]}
                                        language={i18n.language}
                                    />
                                ))
                            }
                        </LanguageContainer>
                    </Tab>
                </TabContainer>
            </Drawer>
        </Container>
    )
}

type LanguageButtonProps = {
    changeLocale: (locale?: string) => void;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
    item: Language | null;
    language?: string;
}

const RenderLanguageButton: React.FC<LanguageButtonProps> = ({ changeLocale, setOpen, item, language }) => {
    const [active, setActive] = useState(false);

    useEffect(() => {
        if (item && item.code && language) {
            setActive(item.code === language);
        }
    }, [item, language]);

    return (
        <LanguageButton
            onClick={() => {
                changeLocale(item?.code);
                setOpen(false);
            }}
            $isActive={active}
            style={{ borderColor: stringToColor(item?.name ?? "") }}
        >
            <LanguageFlag src={item?.flag} alt={item?.name} />
            {item?.name}
        </LanguageButton>
    )
}