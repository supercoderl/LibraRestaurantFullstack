import React, { useEffect, useState } from 'react';
import { ModalActionButton, ModalActionContainer, ModalActionSvg, ModalActionText, ModalBody, ModalBodyFormContainer, ModalBodyFormGroup, ModalBodyFormInput, ModalBodyFormLabel, ModalBodyStatus, ModalHeader } from "../reservation-container/style";
import { Status } from '@/enums';
import occupied from "../../../public/assets/animation/waiter.json";
import reserved from "../../../public/assets/animation/reserved.json";
import cleaning from "../../../public/assets/animation/loading.json";
import outOfService from "../../../public/assets/animation/out.json";
import available from "../../../public/assets/animation/available.json";
import { ModalBodyContent, Text } from './style';
import Lottie from 'lottie-react';
import { Loading } from '@/components/loading';
import { TFunction } from 'i18next';

// Define the props interface for Step1 component
interface Step1Props {
    onNext: () => void; // Function to move to the next step
    status: number;
    isChanged: boolean;
    tableNumber: number;
    onClose: () => void;
    t: TFunction<"translation", undefined>
}

const Step1: React.FC<Step1Props> = ({ onNext, status, onClose, isChanged, tableNumber, t }) => {
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setTimeout(() => setLoading(false), 1000)
    }, []);

    const render = () => {
        switch (status) {
            case Status.Available:
                return {
                    title: isChanged ? `${t("table-change")} ${tableNumber}` : t("table-empty"),
                    animation: available,
                    allowNext: true
                }
            case Status.Occupied:
                return {
                    title: t("table-occupied"),
                    animation: occupied,
                    allowNext: true
                }
            case Status.Reserved:
                return {
                    title: t("table-reserved"),
                    animation: reserved,
                    allowNext: false
                }
            case Status.Cleaning:
                return {
                    title: t("table-cleaning"),
                    animation: cleaning,
                    allowNext: false
                }
            case Status.OutOfService: {
                return {
                    title: t("table-out"),
                    animation: outOfService,
                    allowNext: false
                }
            }
        }
        return {
            title: t("error-occur"),
            animation: outOfService,
            allowNext: false
        }
    }

    return (
        <div>
            <ModalHeader>{t("reservation-info")}</ModalHeader>
            <ModalBody>
                <ModalBodyStatus>{t("reservation-status")}: {Status[status as keyof typeof Status]}</ModalBodyStatus>
                <ModalBodyFormContainer>
                    {
                        loading ?
                            <Loading width="50%" />
                            :
                            <>
                                <ModalBodyContent>
                                    <Lottie autoPlay loop animationData={render().animation} style={{ width: "50% " }} />
                                    <Text>{render().title}</Text>
                                </ModalBodyContent>
                                <ModalActionContainer>
                                    <ModalActionButton style={{ width: !render().allowNext ? '100%' : '50%' }} className="group" type="button" isOutlined onClick={onClose}>
                                        <ModalActionSvg isOutlined width="20px" height="20px" viewBox="0 0 48 48" version="1" xmlns="http://www.w3.org/2000/svg" enable-background="new 0 0 48 48">
                                            <path d="M24,6C14.1,6,6,14.1,6,24s8.1,18,18,18s18-8.1,18-18S33.9,6,24,6z M24,10c3.1,0,6,1.1,8.4,2.8L12.8,32.4 C11.1,30,10,27.1,10,24C10,16.3,16.3,10,24,10z M24,38c-3.1,0-6-1.1-8.4-2.8l19.6-19.6C36.9,18,38,20.9,38,24C38,31.7,31.7,38,24,38 z" />
                                        </ModalActionSvg>
                                        <ModalActionText isOutlined>{t("cancel")}</ModalActionText>
                                    </ModalActionButton>

                                    {
                                        render().allowNext &&
                                        <ModalActionButton className="group" type="submit" onClick={onNext}>
                                            <ModalActionSvg width={20} height={20} version="1.1" id="XMLID_301_" xmlns="http://www.w3.org/2000/svg" xmlnsXlink="http://www.w3.org/1999/xlink"
                                                viewBox="0 0 24 24" xmlSpace="preserve">
                                                <g id="link-next">
                                                    <g>
                                                        <polygon points="13.7,20.7 12.3,19.2 18.6,13 2,13 2,11 18.6,11 12.3,4.8 13.7,3.3 22.4,12 		" />
                                                    </g>
                                                </g>
                                            </ModalActionSvg>
                                            <ModalActionText>{t("continue")}</ModalActionText>
                                        </ModalActionButton>
                                    }
                                </ModalActionContainer>
                            </>
                    }

                </ModalBodyFormContainer>
            </ModalBody>
        </div>
    );
};

export default Step1;