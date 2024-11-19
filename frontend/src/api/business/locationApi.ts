import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";

const CLASS_ITEM_NAME = 'Address'

export function locations(data?: Query, type?: string): Promise<Response | undefined> {
    if (data) {
        data.class = type;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}/${type}?includeDeleted=false`,
        params: data
    })
}

export function location(id?: number, type?: string): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${type}/${id}`
    })
}

export function actionLocation(data: any, action: string, type: string): Promise<SingleResponse | undefined> {
    if(action === "create")
    {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}/${type}`,
            params: null
        }, data)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${type}`,
        params: null
    }, data)
}