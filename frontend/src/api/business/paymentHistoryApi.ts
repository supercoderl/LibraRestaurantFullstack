import { Response, SingleResponse } from "@/type/objectTypes"
import { apiGet, apiPost } from "../api"
import Query from "@/type/Query";

const CLASS_ITEM_NAME = 'PaymentHistory'

export function paymentHistories(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function updatePayment(body: any): Promise<SingleResponse | undefined> {
    return apiPost<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, body)
}