import { Container, CTA, CustomCheck, CustomCheckInput, CustomCheckLabel, FormGroup, FormInput, FormLabel, GoogleButton, GoogleSvg, Link, LoginForm, LoginFormInner, Logo, Onboarding, Seperator, SeperatorText, SingleRow, SlideContent, SlideDescription, SlideImg, SlideText, SwiperContainer, Title } from "./style"
import { Pagination } from 'swiper/modules';
import logo from "../../../public/assets/images/logo/logo-removebg-preview.png";
import { Swiper, SwiperSlide } from 'swiper/react';
import food1 from "../../../public/assets/banner/food1.png";
import food2 from "../../../public/assets/banner/food2.png";
import food3 from "../../../public/assets/banner/food3.png";
import React, { FormEvent, useEffect, useState } from "react";
import { useRouter } from "next/navigation";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { login, loginBySocial, setMessage, validateUser } from "@/redux/slices/auth-slice";
import { Spinner } from "@/components/loading/spinner";
import { get } from "@/utils/localStorage";
import { TFunction } from "i18next";
import { toast } from "react-toastify";
import { useGoogleLogin } from "@react-oauth/google";

type LoginProps = {
    t: TFunction<"translation", undefined>
}

export const LoginContainer: React.FC<LoginProps> = ({ t }) => {
    const [loginInput, setLoginInput] = useState({
        email: "admin@gmail.com",
        password: "Password123!"
    });

    const router = useRouter()
    const dispatch = useStoreDispatch();

    // Get Data from Redux Store
    const isAuthenticated = useStoreSelector(state => state.mainAuthSlice.isAuthenticated)
    const isAuthenticating = useStoreSelector(state => state.mainAuthSlice.isAuthenticating)
    const actionMessage = useStoreSelector(state => state?.mainAuthSlice?.message ?? { type: null, message: null })

    // On Load Validate User by Token
    useEffect(() => {
        const token = get('token')
        if (token) dispatch(validateUser(token));
    }, [])

    // On Load Redirect User if Authenticated
    useEffect(() => {
        if (isAuthenticated && !isAuthenticating) {
            router.push('/management/dashboard');
            setTimeout(() => {
                toast(t("login-success"), { type: "success" });
            }, 800);
        }
    }, [isAuthenticated, isAuthenticating])

    const onSubmit = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (loginInput.email === "" || loginInput.password === "") {
            toast(t("please-input-login"), { type: "warning" });
            return;
        }

        dispatch(login(loginInput));

        setTimeout(() => {
            if (actionMessage?.type === "error") {
                toast(actionMessage?.message, { type: "error" })
                setMessage({ type: "", message: "" })
            }
        }, 500);
    }

    const googleLogin = useGoogleLogin({
        onSuccess: codeResponse => dispatch(loginBySocial({ provider: "google", idToken: codeResponse.access_token })),
    });

    return (
        <>
            <Container>
                <LoginForm onSubmit={(e) => onSubmit(e)}>
                    <LoginFormInner>
                        <div className="logo">
                            <Logo src={logo.src} alt="logo" />
                        </div>
                        <Title>{t("login")}</Title>
                        <p className="body-text">{t("admin-page")}</p>

                        <GoogleButton href="#" onClick={() => googleLogin()}>
                            <span className="google-icon"><GoogleSvg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
                                <path d="M113.47 309.408L95.648 375.94l-65.139 1.378C11.042 341.211 0 299.9 0 256c0-42.451 10.324-82.483 28.624-117.732h.014L86.63 148.9l25.404 57.644c-5.317 15.501-8.215 32.141-8.215 49.456.002 18.792 3.406 36.797 9.651 53.408z" fill="#fbbb00" />
                                <path d="M507.527 208.176C510.467 223.662 512 239.655 512 256c0 18.328-1.927 36.206-5.598 53.451-12.462 58.683-45.025 109.925-90.134 146.187l-.014-.014-73.044-3.727-10.338-64.535c29.932-17.554 53.324-45.025 65.646-77.911h-136.89V208.176h245.899z" fill="#518ef8" />
                                <path d="M416.253 455.624l.014.014C372.396 490.901 316.666 512 256 512c-97.491 0-182.252-54.491-225.491-134.681l82.961-67.91c21.619 57.698 77.278 98.771 142.53 98.771 28.047 0 54.323-7.582 76.87-20.818l83.383 68.262z" fill="#28b446" />
                                <path d="M419.404 58.936l-82.933 67.896C313.136 112.246 285.552 103.82 256 103.82c-66.729 0-123.429 42.957-143.965 102.724l-83.397-68.276h-.014C71.23 56.123 157.06 0 256 0c62.115 0 119.068 22.126 163.404 58.936z" fill="#f14336" />
                            </GoogleSvg></span>
                            <span>{t("login-google")}</span>
                        </GoogleButton>

                        <Seperator>
                            <SeperatorText>{t("or-use-account")}</SeperatorText>
                        </Seperator>

                        <FormGroup>
                            <FormLabel htmlFor="email">{t("email")} <span className="required-star">*</span></FormLabel>
                            <FormInput
                                type="text"
                                placeholder="email@employee.com"
                                id="email"
                                value={loginInput.email}
                                onChange={(e) => setLoginInput(prev => ({ ...prev, email: e.target.value }))}
                            />
                        </FormGroup>
                        <FormGroup>
                            <FormLabel htmlFor="pwd">{t("password")} <span className="required-star">*</span></FormLabel>
                            <FormInput
                                autoComplete="off"
                                type="password"
                                placeholder={t("at-least-6")}
                                id="pwd"
                                value={loginInput.password}
                                onChange={(e) => setLoginInput(prev => ({ ...prev, password: e.target.value }))}
                            />
                        </FormGroup>

                        <SingleRow>
                            <CustomCheck>
                                <CustomCheckInput autoComplete="off" type="checkbox" id="remember" />
                                <CustomCheckLabel htmlFor="remember">{t("remember-me")}</CustomCheckLabel>
                            </CustomCheck>

                            <Link href="#">{t("forgot-password")}</Link>
                        </SingleRow>

                        <CTA type="submit">
                            {isAuthenticating && <Spinner width="1.2vw" color="white" />}
                            {t("login")}
                        </CTA>
                    </LoginFormInner>
                </LoginForm>
                <Onboarding>
                    <SwiperContainer>
                        <Swiper
                            style={{ height: "100%" }}
                            modules={[Pagination]}
                            spaceBetween={50}
                            slidesPerView={1}
                            pagination={{ clickable: true }}
                        >
                            <SwiperSlide>
                                <div className="slide-image">
                                    <SlideImg src={food1.src} loading="lazy" alt="" />
                                </div>
                                <SlideContent>
                                    <SlideText>Turn your ideas into reality.</SlideText>
                                    <SlideDescription>Consistent quality and eperience across all platform and devices</SlideDescription>
                                </SlideContent>
                            </SwiperSlide>
                            <SwiperSlide>
                                <div className="slide-image">
                                    <SlideImg src={food2.src} loading="lazy" alt="" />
                                </div>
                                <SlideContent>
                                    <SlideText>Turn your ideas into reality.</SlideText>
                                    <SlideDescription>Consistent quality and eperience across all platform and devices</SlideDescription>
                                </SlideContent>
                            </SwiperSlide>

                            <SwiperSlide>
                                <div className="slide-image">
                                    <SlideImg src={food3.src} loading="lazy" alt="" />
                                </div>
                                <SlideContent>
                                    <SlideText>Turn your ideas into reality.</SlideText>
                                    <SlideDescription>Consistent quality and eperience across all platform and devices</SlideDescription>
                                </SlideContent>
                            </SwiperSlide>
                        </Swiper>
                        <div className="swiper-pagination"></div>
                    </SwiperContainer>
                </Onboarding>
            </Container>
        </>
    )
}