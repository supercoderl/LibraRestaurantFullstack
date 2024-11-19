import Query from "@/type/Query"
import { apiGet, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { Notification } from "@/type/Notification";

const CLASS_ITEM_NAME = 'Message'

export function messages(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function updateStatus(message: Notification): Promise<SingleResponse | undefined> {
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, message)
}