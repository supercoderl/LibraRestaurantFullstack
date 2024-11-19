import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import Category from "@/type/Category";

const CLASS_ITEM_NAME = 'Category'

export function categories(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function category(id?: number): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionCategory(category: Category, action: string): Promise<SingleResponse | undefined> {
    if (action === "create") {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, category)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, category)
}

export function deleteCategory(id: number): Promise<SingleResponse | undefined> {
    return apiDelete<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}