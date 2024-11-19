import { PromoCard } from "@/components/discount/card";
import { DiscountTargetType } from "@/enums";
import { postDiscountSelection, putDiscountSelection } from "@/redux/slices/discount-slice";
import { useStoreDispatch, useStoreSelector } from "@/redux/store";
import { Discount } from "@/type/Discount";
import { DiscountType } from "@/type/DiscountType";
import Item from "@/type/Item";
import { Col, Modal, Row } from "antd"
import { TFunction } from "i18next";
import { useEffect, useState } from "react";

type DiscountSelectProps = {
    isOpen: boolean;
    item?: Item | null;
    t: TFunction<"translation", undefined>
    handleCancel: () => void;
    discountTypes: DiscountType[];
}

export const DiscountSelect: React.FC<DiscountSelectProps> = ({ isOpen, handleCancel, t, item, discountTypes }) => {
    const dispatch = useStoreDispatch();
    const [selectedCounpon, setSelectedCounpon] = useState<number | null>(null);
    const { loading } = useStoreSelector(state => state.mainDiscountSlice);

    const handleSelect = (id: number) => {
        id === selectedCounpon ? setSelectedCounpon(null) : setSelectedCounpon(id);
    }

    const onSubmit = async () => {
        if (selectedCounpon) {
            let body: any = {
                discountTypeId: selectedCounpon,
                itemId: item?.itemId,
                discountTargetType: DiscountTargetType.Food
            }
            if (item?.discount) {
                body.discountId = item?.discount?.discountId;
                dispatch(putDiscountSelection(body as Discount));
            }
            else {
                dispatch(postDiscountSelection(body as Discount));
            }
        }
    }

    useEffect(() => {
        setSelectedCounpon(item?.discount?.discountTypeId || null);
    }, [item]);

    return (
        <Modal title={"Chọn mã giảm giá"} open={isOpen} okButtonProps={{ loading: loading, disabled: loading }} onOk={onSubmit} onCancel={handleCancel}>
            <Row style={{ paddingBlock: 10 }} gutter={[24, 24]}>
                {
                    discountTypes.map((item, index) => (
                        <Col span={12} key={index}>
                            <PromoCard item={item} handleSelect={handleSelect} selectedItem={selectedCounpon} />
                        </Col>
                    ))
                }
            </Row>
        </Modal>
    )
}