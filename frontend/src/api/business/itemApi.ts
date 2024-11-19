import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import Item from "@/type/Item";

const CLASS_ITEM_NAME = 'Item'

export function items(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function item(id?: number): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionItem(item: Item, action: string): Promise<SingleResponse | undefined> {
    if(action === "create")
    {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, item)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, item)
}

export function deleteItem(id: number): Promise<SingleResponse | undefined> {
    return apiDelete<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function itemBySlug(slug: string): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/slug/${slug}`
    })
}