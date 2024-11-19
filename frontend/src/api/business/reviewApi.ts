import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { Review } from "@/type/Review";

const CLASS_ITEM_NAME = 'Review'

export function reviews(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function review(id?: number): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionReview(review: Review, action: string): Promise<SingleResponse | undefined> {
    if (action === "create") {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, review)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, review)
}

export function deleteReview(id: number): Promise<SingleResponse | undefined> {
    return apiDelete<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}