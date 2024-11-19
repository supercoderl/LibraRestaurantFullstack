import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { Store } from "@/type/Store";

const CLASS_ITEM_NAME = 'Store'

export function stores(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function store(id?: string | null): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionStore(store: Store, action: string): Promise<SingleResponse | undefined> {
    if(action === "create")
    {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, store)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, store)
}