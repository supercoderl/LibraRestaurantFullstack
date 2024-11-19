import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { PaymentMethod } from "@/type/PaymentMethod";

const CLASS_ITEM_NAME = 'PaymentMethod'

export function paymentMethods(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function paymentMethod(id?: number): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionPaymentMethod(paymentMethod: PaymentMethod, action: string): Promise<SingleResponse | undefined> {
    if(action === "create")
    {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, paymentMethod)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, paymentMethod)
}