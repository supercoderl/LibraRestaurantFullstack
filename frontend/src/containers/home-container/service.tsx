import { FontAwesomeIcon } from "@fortawesome/react-fontawesome"
import { Description, Grid, HeaderTitle, Part1, Part1Title, Part2, Part2Description, Part2Link, RowHeader, RowHeaderCol, ServiceContainer, ServiceHeader, SingleService, TitleSpan } from "./style"
import { fa500px, faAngellist, faAsymmetrik, faCanadianMapleLeaf } from "@fortawesome/free-brands-svg-icons"
import { faAward, faBroadcastTower, faArrowAltCircleRight } from "@fortawesome/free-solid-svg-icons"
import { TFunction } from "i18next"
import React from "react"

type ServiceProps = {
    t: TFunction<"translation", undefined>
}

export const Service: React.FC<ServiceProps> = ({ t }) => {
    return (
        <ServiceContainer>
            <RowHeader>
                <RowHeaderCol>
                    <ServiceHeader>
                        <HeaderTitle>{t("model")} <TitleSpan>{t("service")}</TitleSpan></HeaderTitle>
                        <Description>{t("best-for-cus")}</Description>
                    </ServiceHeader>
                </RowHeaderCol>
            </RowHeader>
            <Grid>
                <SingleService>
                    <Part1>
                        <FontAwesomeIcon icon={fa500px} fade color="red" size="3x" style={{ marginBottom: 25, animationDuration: '5s' }} />
                        <Part1Title>{t("delivery-to-door")}</Part1Title>
                    </Part1>
                    <Part2>
                        <Part2Description>{t("best-delivery")}</Part2Description>
                        <Part2Link href="#"><FontAwesomeIcon icon={faArrowAltCircleRight} size="1x" color="red" style={{ marginRight: 10 }} />{t("see-more")}</Part2Link>
                    </Part2>
                </SingleService>

                <SingleService>
                    <Part1>
                        <FontAwesomeIcon icon={faAngellist} fade color="red" size="3x" style={{ marginBottom: 25, animationDuration: '5s' }} />
                        <Part1Title>{t("always-on-duty")}</Part1Title>
                    </Part1>
                    <Part2>
                        <Part2Description>{t("text-2")}</Part2Description>
                        <Part2Link href="#"><FontAwesomeIcon icon={faArrowAltCircleRight} size="1x" color="red" style={{ marginRight: 10 }} />{t("see-more")}</Part2Link>
                    </Part2>
                </SingleService>

                <SingleService>
                    <Part1>
                        <FontAwesomeIcon icon={faAward} fade color="red" size="3x" style={{ marginBottom: 25, animationDuration: '5s' }} />
                        <Part1Title>{t("food-healthy")}</Part1Title>
                    </Part1>
                    <Part2>
                        <Part2Description>{t("text-3")}</Part2Description>
                        <Part2Link href="#"><FontAwesomeIcon icon={faArrowAltCircleRight} size="1x" color="red" style={{ marginRight: 10 }} />{t("see-more")}</Part2Link>
                    </Part2>
                </SingleService>

                <SingleService>
                    <Part1>
                        <FontAwesomeIcon icon={faAsymmetrik} fade color="red" size="3x" style={{ marginBottom: 25, animationDuration: '5s' }} />
                        <Part1Title>{t("best-member")}</Part1Title>
                    </Part1>
                    <Part2>
                        <Part2Description>{t("text-4")}</Part2Description>
                        <Part2Link href="#"><FontAwesomeIcon icon={faArrowAltCircleRight} size="1x" color="red" style={{ marginRight: 10 }} />{t("see-more")}</Part2Link>
                    </Part2>
                </SingleService>

                <SingleService>
                    <Part1>
                        <FontAwesomeIcon icon={faBroadcastTower} fade color="red" size="3x" style={{ marginBottom: 25, animationDuration: '5s' }} />
                        <Part1Title>{t("global-coverage")}</Part1Title>
                    </Part1>
                    <Part2>
                        <Part2Description>{t("text-5")}</Part2Description>
                        <Part2Link href="#"><FontAwesomeIcon icon={faArrowAltCircleRight} size="1x" color="red" style={{ marginRight: 10 }} />{t("see-more")}</Part2Link>
                    </Part2>
                </SingleService>

                <SingleService>
                    <Part1>
                        <FontAwesomeIcon icon={faCanadianMapleLeaf} fade color="red" size="3x" style={{ marginBottom: 25, animationDuration: '5s' }} />
                        <Part1Title>{t("food-heaven")}</Part1Title>
                    </Part1>
                    <Part2>
                        <Part2Description>{t("text-6")}</Part2Description>
                        <Part2Link href="#"><FontAwesomeIcon icon={faArrowAltCircleRight} size="1x" color="red" style={{ marginRight: 10 }} />{t("see-more")}</Part2Link>
                    </Part2>
                </SingleService>
            </Grid>
        </ServiceContainer>
    )
}