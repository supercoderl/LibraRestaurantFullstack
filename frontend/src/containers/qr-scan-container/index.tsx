import Header from "@/components/header";
import { BodyContainer, CenterContainer, Container, ContinueButton, ContinueSvg, ContinueText, TitleContainer } from "./style";
import { Scanner } from "@yudiel/react-qr-scanner";
import { Title } from "@/components/title";
import { useRouter } from "next/navigation";
import { Status } from "@/enums";
import { useEffect, useState } from "react";
import { ModalSection } from "@/components/modal";
import { getStatus, updateReservationAsync, updateReservationOccupied } from "@/redux/slices/reservation-slice";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import Step1 from "./step1";
import Step2 from "./step2";
import { toast } from "react-toastify";
import { useTranslation } from "next-i18next";
import { useSignalR } from "@/context/signalRProvider";

export default function QRScanContainer() {
    const router = useRouter();
    const [show, setShow] = useState(false);
    const dispatch = useStoreDispatch();
    const { status, isChanged, tableNumber, customerPhone, id } = useStoreSelector(state => state.reservation);
    const [jsonValue, setJsonValue] = useState<any>(null);
    const [step, setStep] = useState(1);
    const [pause, setPause] = useState(false);
    const { t } = useTranslation();
    const { joinTableGroup } = useSignalR();

    // Function to move to the next step
    const handleNextStep = () => {
        setStep(step + 1);
    };

    // Function to move to the previous step
    const handlePreviousStep = () => {
        setStep(step - 1);
    };

    const onScan = (scannedValue: any) => {
        setJsonValue(scannedValue);
        if (scannedValue?.tableNumber && scannedValue?.storeId) {
            dispatch(getStatus({ tableNumber: Number(scannedValue.tableNumber), storeId: scannedValue.storeId }));
        }
        setPause(true);
    };

    const onSubmit = async (customerName: string, customerPhone: string) => {
        setPause(false);
        setShow(false);
        setStep(1);
        if (status === Status.Available) {
            dispatch(updateReservationAsync({
                reservationId: id,
                isChanged: tableNumber !== -1 && tableNumber !== jsonValue?.tableNumber,
                capacity: jsonValue?.capacity,
                storeId: jsonValue?.storeId,
                tableNumber: jsonValue?.tableNumber,
                status: Status.Occupied,
                customerName,
                customerPhone
            }));
        }
        else if (status === Status.Occupied) {
            dispatch(updateReservationOccupied({
                reservationId: id,
                isChanged: tableNumber !== -1 && tableNumber !== jsonValue?.tableNumber,
                capacity: jsonValue?.capacity,
                storeId: jsonValue?.storeId,
                tableNumber: jsonValue?.tableNumber,
            }))
        }
        await joinTableGroup(`${jsonValue?.storeId}-${jsonValue?.tableNumber}`);
        toast(t("book-successful"), { type: "success" });
        router.push("/");
    }

    const onClose = () => {
        setPause(false);
        setShow(false);
        setStep(1);
    }

    useEffect(() => {
        if (jsonValue && status !== -1) {
            setShow(true);
        }
    }, [status, jsonValue, router]);

    return (
        <>
            <Container>
                <BodyContainer>
                    <CenterContainer>
                        <Header t={t} />
                        <TitleContainer>
                            <Title>{t("scan-to-order")}</Title>
                        </TitleContainer>
                        <Scanner
                            onScan={(result) => onScan(JSON.parse(result[0].rawValue))}
                            paused={pause}
                            styles={{
                                container: { width: 350, height: 350 }
                            }}
                        />
                        {
                            pause &&
                            <ContinueButton className="group" onClick={() => setPause(false)}>
                                <ContinueSvg width="24px" height="24px" fill="white" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M3 4v5h2V5h4V3H4a1 1 0 0 0-1 1zm18 5V4a1 1 0 0 0-1-1h-5v2h4v4h2zm-2 10h-4v2h5a1 1 0 0 0 1-1v-5h-2v4zM9 21v-2H5v-4H3v5a1 1 0 0 0 1 1h5zM2 11h20v2H2z" /></ContinueSvg>
                                <ContinueText>{t("continue-scan")}</ContinueText>
                            </ContinueButton>
                        }
                    </CenterContainer>
                </BodyContainer>
            </Container>
            <ModalSection onOpenChange={() => { }} open={show}>
                {step === 1 && <Step1 t={t} onNext={handleNextStep} status={status} isChanged={isChanged} tableNumber={tableNumber} onClose={onClose} />}
                {step === 2 && <Step2 t={t} onPrevious={handlePreviousStep} status={status} onSubmit={onSubmit} customerPhone={customerPhone || ""} />}
            </ModalSection>
        </>
    );
}