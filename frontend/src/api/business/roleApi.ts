import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { Role } from "@/type/Role";

const CLASS_ITEM_NAME = 'Role'

export function roles(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function role(id?: number): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionRole(role: Role, action: string): Promise<SingleResponse | undefined> {
    if (action === "create") {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, role)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, role)
}

export function assign(body: any): Promise<SingleResponse | undefined> {
    return apiPost<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/assign`,
        params: null
    }, body);
}