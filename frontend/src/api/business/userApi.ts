import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { Employee } from "@/type/Employee";

const CLASS_ITEM_NAME = 'Employee'

export function authenticate(data: { email: string, password: string }): Promise<SingleResponse | undefined> {
    return apiPost<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/login`,
        params: null
    }, data);
}

export function socialAuthenticate(data: { provider: string, idToken: string }): Promise<SingleResponse | undefined> {
    return apiPost<SingleResponse>({
        url: `/Social/${data.provider}`,
        params: null
    }, data);
}

export function logout(data: { refreshToken: string }): Promise<SingleResponse | undefined> {
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/logout`,
        params: null
    }, data);
}

export function currentUser(): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/me`,
        params: null
    });
}

export function employees(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function employee(id?: string | null): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function actionEmployee(employee: Employee, action: string): Promise<SingleResponse | undefined> {
    if (action === "create") {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, employee)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, employee)
}