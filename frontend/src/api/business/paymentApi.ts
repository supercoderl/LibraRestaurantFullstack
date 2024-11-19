import { SingleResponse } from "@/type/objectTypes"
import { apiPost } from "../api"

const CLASS_ITEM_NAME = 'Payment'

export function pay(body: any, type: string): Promise<SingleResponse | undefined> {
    return apiPost<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}/${type}`,
        params: null
    }, body)
}