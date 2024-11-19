import React, { useEffect, useState } from "react"
import { AddReviewTitle, Avatar, CommentBody, CommentItem, CommentLabel, CommentList, CommentRatingContainer, CommentText, FormCommentGroup, FormGroup, FormRating, FormRatingList, Input, Label, Name, ReviewForm, SubmitButton, Tab, TabContainer, TabContent, TabLink, TabLinks, TabTitle, TextArea } from "./style"
import InstructionIcon from "../../../public/assets/icons/instruction-icon.svg";
import CommentIcon from "../../../public/assets/icons/comment-icon.svg";
import Item from "@/type/Item";
import StarFillIcon from "../../../public/assets/icons/star-fill-icon.svg";
import StarIcon from "../../../public/assets/icons/star-icon.svg";
import { toast } from "react-toastify";
import { useStoreSelector } from "@/redux/store";
import { fetchReviewData, postComment } from "@/redux/slices/review-slice";
import { Review } from "@/type/Review";
import { Spinner } from "@/components/loading/spinner";
import useWindowDimensions from "@/hooks/use-window-dimensions";
import { TFunction } from "i18next";

type DetailTabProps = {
    item: Item;
    dispatch: any;
    t: TFunction<"translation", undefined>;
}

export const DetailTabs: React.FC<DetailTabProps> = ({ item, dispatch, t }) => {
    const [state, setState] = useState(1);
    const [reviewInfo, setReviewInfo] = useState({
        customerName: '',
        customerEmail: '',
        rating: 0,
        comment: ''
    });
    const { loading, isSuccess, reviews } = useStoreSelector(state => state.mainReviewSlice);
    const [selectedRating, setSelectedRating] = useState(0);
    const [hoveredRating, setHoveredRating] = useState(0);
    const { width } = useWindowDimensions();

    const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
        e.preventDefault();

        if (selectedRating === 0) {
            toast(t("please-choose-rating"), { type: "warning" });
            return;
        }

        const body = {
            reviewId: 0,
            itemId: item.itemId,
            customerName: reviewInfo.customerName,
            customerEmail: reviewInfo.customerEmail === '' ? null : reviewInfo.customerEmail,
            rating: selectedRating,
            comment: reviewInfo.comment,
            isVerifiedPurchase: false
        };

        dispatch(postComment(body as Review));
    }

    const handleMouseEnter = (index: number) => {
        setHoveredRating(index + 1);
    };

    const handleMouseLeave = () => {
        setHoveredRating(0);
    };

    const handleClick = (index: number) => {
        setSelectedRating(index + 1);
    };

    useEffect(() => {
        dispatch(fetchReviewData(item.itemId));
    }, []);

    useEffect(() => {
        if (isSuccess) {
            toast(t("thanks-for-your-rating"), { type: "success" });
            setReviewInfo({
                customerName: '',
                customerEmail: '',
                rating: 0,
                comment: ''
            });
            setSelectedRating(0);
            dispatch(fetchReviewData(item.itemId));
        }
    }, [isSuccess]);

    return (
        <TabContainer>
            <Tab>
                <TabLinks>
                    <TabLink $isActive={state === 1} onClick={() => setState(1)}>
                        <InstructionIcon width={18} height={18} fill={state === 1 ? "rgb(209 33 33)" : "#ccc"} />
                        {width > 767 && t("instruction")}</TabLink>
                    <TabLink $isActive={state === 2} onClick={() => setState(2)}>
                        <CommentIcon width={18} height={18} fill={state === 2 ? "rgb(209 33 33)" : "#ccc"} />
                        {width > 767 && t("review")}</TabLink>
                </TabLinks>

                <TabContent $isActive={state === 1}>
                    <TabTitle>{t("instruction")}</TabTitle>
                    <p>{item.instruction}</p>
                </TabContent>

                <TabContent $isActive={state === 2}>
                    <TabTitle>{t("review")}</TabTitle>
                    <div className="comments-area" id="comments">
                        <CommentList>
                            {
                                reviews.map((review, index) => (
                                    <CommentItem key={index}>
                                        <CommentBody>
                                            <div className="comment-author vcard">
                                                <Avatar src="https://st5.depositphotos.com/54433710/71298/v/450/depositphotos_712980420-stock-illustration-man-vector-flat-color-icon.jpg" alt="/" />
                                                <Name>{review.customerName}</Name>
                                            </div>
                                            <CommentRatingContainer>
                                                {[1, 2, 3, 4, 5].map((star) => {
                                                    const isFilled = star <= review.rating;

                                                    return isFilled ? (
                                                        <StarFillIcon
                                                            key={star}
                                                            width={18}
                                                            height={18}
                                                            fill="orange"
                                                        />
                                                    ) : (
                                                        <StarIcon
                                                            key={star}
                                                            width={18}
                                                            height={18}
                                                            fill="orange"
                                                        />
                                                    );
                                                })}
                                            </CommentRatingContainer>
                                            <CommentText>{review.comment}</CommentText>
                                        </CommentBody>
                                    </CommentItem>
                                ))
                            }
                        </CommentList>
                    </div>
                    <div>
                        <AddReviewTitle id="reply-title">{t("write-review")}</AddReviewTitle>
                        <ReviewForm id="commentform" onSubmit={handleSubmit}>
                            <FormGroup>
                                <Input
                                    type="text"
                                    name="name"
                                    className="peer"
                                    id="name"
                                    value={reviewInfo.customerName}
                                    onChange={e => setReviewInfo(prev => ({ ...prev, customerName: e.target.value }))}
                                    required />
                                <Label htmlFor="name">{t("customer-name")}</Label>
                            </FormGroup>
                            <FormGroup>
                                <Input
                                    type="email"
                                    name="email"
                                    className="peer"
                                    id="email"
                                    value={reviewInfo.customerEmail}
                                    onChange={e => setReviewInfo(prev => ({ ...prev, customerEmail: e.target.value }))}
                                    required
                                />
                                <Label htmlFor="email">{t("email")}</Label>
                            </FormGroup>
                            <FormCommentGroup>
                                <TextArea
                                    rows={10}
                                    name="comment"
                                    className="peer"
                                    id="comment"
                                    required
                                    value={reviewInfo.comment}
                                    onChange={e => setReviewInfo(prev => ({ ...prev, comment: e.target.value }))}
                                ></TextArea>
                                <CommentLabel htmlFor="comment">{t("input-review")}</CommentLabel>
                            </FormCommentGroup>
                            <FormRating>
                                <FormRatingList>
                                    {[...Array(5)].map((_, index) => (
                                        <div
                                            key={index}
                                            onMouseEnter={() => handleMouseEnter(index)}
                                            onMouseLeave={handleMouseLeave}
                                            onClick={() => handleClick(index)}
                                            style={{ cursor: 'pointer' }}
                                        >
                                            {
                                                hoveredRating > index || selectedRating > index ?
                                                    <StarFillIcon
                                                        width={40}
                                                        height={40}
                                                        fill='orange'
                                                    />
                                                    :
                                                    <StarIcon
                                                        width={40}
                                                        height={40}
                                                        fill='orange'
                                                    />
                                            }
                                        </div>
                                    ))}
                                </FormRatingList>
                            </FormRating>
                            <FormCommentGroup>
                                <SubmitButton
                                    disabled={loading}
                                    type="submit"
                                    id="submit"
                                >
                                    {loading && <Spinner width={16} />}
                                    {t("send")}
                                </SubmitButton>
                            </FormCommentGroup>
                        </ReviewForm>
                    </div>
                </TabContent>
            </Tab>
        </TabContainer>
    )
}