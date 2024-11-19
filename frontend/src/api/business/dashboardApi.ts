import { apiGet } from "../api"
import { SingleResponse } from "@/type/objectTypes";

const CLASS_ITEM_NAME = 'Dashboard'

export function analytic(): Promise<SingleResponse | undefined> {
    return apiGet<SingleResponse>({
        url: `/${CLASS_ITEM_NAME}`,
    })
}