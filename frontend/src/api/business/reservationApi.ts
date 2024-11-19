import Query from "@/type/Query"
import { apiGet, apiPost, apiDelete, apiPut } from '../api'
import { Response, SingleResponse } from "@/type/objectTypes";
import { Reservation } from "@/type/Reservation";

const CLASS_ITEM_NAME = 'Reservation'

export function reservations(data?: Query): Promise<Response | undefined> {
    if (data) {
        data.class = CLASS_ITEM_NAME;
        data.count = 1;
    }
    return apiGet<Response>({
        url: `/${CLASS_ITEM_NAME}?includeDeleted=false`,
        params: data
    })
}

export function tableKeys(): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/tables?includeDeleted=false`,
    })
}

export function reservation(id?: number): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${id}`
    })
}

export function reservationByTableAndStore(tableNumber: number, storeId: string): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${tableNumber}/${storeId}`
    })
}

export function reservationTable(tableNumber?: number | null, storeId?: string | null): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${tableNumber}/${storeId}`
    })
}

export function reservationCustomer(reservation: {
    reservationId: number;
    status: number;
    customerName: string;
    customerPhone: string;
}): Promise<SingleResponse | undefined> {
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/customer`
    }, reservation);
}

export function generateCode(reservationId: number): Promise<SingleResponse | undefined> {
    return apiPost<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${reservationId}/code`,
        params: null
    }, { reservationId })
}

export function actionReservation(reservation: Reservation, action: string): Promise<SingleResponse | undefined> {
    if (action === "create") {
        return apiPost<SingleResponse>({
            url: `/${CLASS_ITEM_NAME}`,
            params: null
        }, reservation)
    }
    return apiPut<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
        params: null
    }, reservation)
}

export function deleteReservation(reservationId: number): Promise<SingleResponse | undefined> {
    return apiDelete<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${reservationId}`,
        params: null
    })
}