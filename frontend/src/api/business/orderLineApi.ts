import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { OrderLine } from "@/type/OrderLine";

const CLASS_ITEM_NAME = 'OrderLine'

export function orderLines(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function orderLine(id?: string): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionListOrderLine(orderLines: OrderLine[], action: string): Promise<SingleResponse | undefined> {
    if(action === "create")
    {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}/list`,
            params: null
        }, orderLines)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/list`,
        params: null
    }, orderLines)
}