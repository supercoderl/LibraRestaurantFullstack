import { BottomDetail, BottomText, BottomTextA, BottomTextSpan, Box, Button, Content, ContentTop, FooterContainer, Input, InputBox, LinkBoxes, LinkName, LinkNameText, MediaIcon, MediaText, SpanLogo, TitleDetail, TitleImage, TitleLogo } from "./style"
import logo from "../../../public/assets/images/logo/logo-removebg-preview.png";
import FacebookIcon from "../../../public/assets/icons/facebook-icon.svg";
import TwitterIcon from "../../../public/assets/icons/twitter-icon.svg";
import InstagramIcon from "../../../public/assets/icons/instagram-icon.svg";
import LinkedInIcon from "../../../public/assets/icons/linkedin-icon.svg";
import YoutubeIcon from "../../../public/assets/icons/youtube-icon.svg";
import { TFunction } from "i18next";
import React from "react";

export default function Footer({ t }: { t: TFunction<"translation", undefined> }) {
    return (
        <FooterContainer>
            <Content>
                <ContentTop>
                    <TitleDetail>
                        <TitleImage src={logo.src} alt="logo" />
                        <TitleLogo>Libra <SpanLogo>Restaurant</SpanLogo></TitleLogo>
                    </TitleDetail>
                    <MediaIcon>
                        <MediaText href="https://www.facebook.com/quangm4">
                            <FacebookIcon width={24} height={24} />
                        </MediaText>
                        <MediaText href="#">
                            <TwitterIcon width={24} height={24} />
                        </MediaText>
                        <MediaText href="#">
                            <InstagramIcon width={24} height={24} />
                        </MediaText>
                        <MediaText href="https://www.linkedin.com/in/supercoderle">
                            <LinkedInIcon width={24} height={24} />
                        </MediaText>
                        <MediaText href="#">
                            <YoutubeIcon width={24} height={24} />
                        </MediaText>
                    </MediaIcon>
                </ContentTop>
                <LinkBoxes>
                    <Box>
                        <LinkName>{t("company")}</LinkName>
                        <li><LinkNameText href="/">{t("home")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("contact")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("intro")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("hire")}</LinkNameText></li>
                    </Box>
                    <Box>
                        <LinkName>{t("service")}</LinkName>
                        <li><LinkNameText href="#">{t("caretaker")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("order-online")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("web-design")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("instruction")}</LinkNameText></li>
                    </Box>
                    <Box>
                        <LinkName>{t("account")}</LinkName>
                        <li><LinkNameText href="/login">{t("login")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("profile")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("options")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("pay")}</LinkNameText></li>
                    </Box>
                    <Box>
                        <LinkName>{t("course")}</LinkName>
                        <li><LinkNameText href="#">{t("cooking")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("serve")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("receptionist")}</LinkNameText></li>
                        <li><LinkNameText href="#">{t("technique")}</LinkNameText></li>
                    </Box>
                    <InputBox>
                        <LinkName>{t("register-now")}</LinkName>
                        <li><Input placeholder={t("input-email")} /></li>
                        <li><Button>{t("subcribe")}</Button></li>
                    </InputBox>
                </LinkBoxes>
            </Content>
            <BottomDetail>
                <BottomText>
                    <BottomTextSpan>Copyright © 2024 <BottomTextA href="#">Libra Restaurant.</BottomTextA>All rights reserved</BottomTextSpan>
                    <BottomTextSpan>
                        <BottomTextA href="#">{t("privacy")}</BottomTextA>
                        <BottomTextA href="#">{t("terms")}</BottomTextA>
                    </BottomTextSpan>
                </BottomText>
            </BottomDetail>
        </FooterContainer>
    )
}