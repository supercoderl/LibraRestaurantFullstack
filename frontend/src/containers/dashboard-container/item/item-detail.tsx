import Item from "@/type/Item";
import { Modal, Image, Button, Divider } from "antd";
import { DetailContainer, DetailContent, DetailPreviewContainer, DetailPreviewStartContainer, DetailPreviewStartText, DetailPrice, DetailRecipe, DetailSummary, DetailTitleContainer, DetailTitleText, DetailWrapper } from "../style";
import { formatCurrency } from "@/utils/currency";
import { CommentOutlined, ReconciliationOutlined } from "@ant-design/icons";
import { TFunction } from "i18next";

type ItemDetailProps = {
    isOpen: boolean;
    handleCancel: () => void;
    item?: Item | null;
    t: TFunction<"translation", undefined>
}

const ItemDetail: React.FC<ItemDetailProps> = ({ isOpen, handleCancel, item, t }) => {
    return (
        <Modal title={t("food-infomation")} open={isOpen} onOk={handleCancel} onCancel={handleCancel} width={1000} footer={null}>
            <DetailContainer>
                <DetailWrapper>
                    <Image width="40%" alt="" src={item?.picture || process.env.NEXT_PUBLIC_DUMMY_PICTURE} />
                    <DetailContent>
                        <DetailTitleText>{item?.title}</DetailTitleText>
                        <DetailPreviewContainer>
                            <DetailPreviewStartContainer>
                                <svg fill="orange" stroke="orange" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" width={16} className="text-indigo-500" viewBox="0 0 24 24">
                                    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"></path>
                                </svg>
                                <svg fill="orange" stroke="orange" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" width={16} className="text-indigo-500" viewBox="0 0 24 24">
                                    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"></path>
                                </svg>
                                <svg fill="orange" stroke="orange" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" width={16} className="text-indigo-500" viewBox="0 0 24 24">
                                    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"></path>
                                </svg>
                                <svg fill="orange" stroke="orange" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" width={16} className="text-indigo-500" viewBox="0 0 24 24">
                                    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"></path>
                                </svg>
                                <svg fill="none" stroke="currentColor" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" width={16} className="text-indigo-500" viewBox="0 0 24 24">
                                    <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z"></path>
                                </svg>
                                <DetailPreviewStartText>20 {t("reviews")}</DetailPreviewStartText>
                            </DetailPreviewStartContainer>
                        </DetailPreviewContainer>
                        <DetailPrice>{formatCurrency(item?.price || 0)}</DetailPrice>
                        <Button
                            style={{ marginBottom: 10, width: '60%', borderRadius: 3, background: "rgba(0, 0, 0, 0.3)", color: "white", fontSize: 20, paddingBlock: 25 }}
                            type="primary"
                            disabled
                        >{t("order")}</Button>
                        <DetailTitleContainer>
                            <ReconciliationOutlined />
                            <p>{t("recipe")}:</p>
                        </DetailTitleContainer>
                        <DetailRecipe>{item?.recipe}</DetailRecipe>
                    </DetailContent>
                </DetailWrapper>

                <Divider variant="dashed" style={{ borderColor: 'rgba(0, 0, 0, 0.3)', marginTop: 5, marginBottom: 10 }} />
                <DetailTitleContainer>
                    <CommentOutlined />
                    <p>{t("description")}:</p>
                </DetailTitleContainer>
                <DetailSummary>{item?.summary}</DetailSummary>
            </DetailContainer>
        </Modal>
    )
}

export default ItemDetail;