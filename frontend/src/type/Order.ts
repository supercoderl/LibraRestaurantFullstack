import { OrderLine } from "./OrderLine";
import { OrderLog } from "./OrderLog";

export interface Order {
    orderId: string;
    orderNo: string;
    storeId: string;
    paymentMethodId?: number | null;
    paymentTimeId?: number | null;
    servantId?: string | null;
    cashierId?: string | null;
    customerNotes?: string | null;
    reservationId: number;
    priceCalculated: number;
    priceAdjustment?: number | null;
    priceAdjustmentReason?: string | null;
    subtotal: number;
    tax: number;
    total: number;
    latestStatus: number;
    latestStatusUpdate: Date;
    isPaid: boolean;
    isPreparationDelayed: boolean;
    delayedTime?: Date | null;
    isCanceled: boolean;
    canceledTime?: Date | null;
    canceledReason?: string | null;
    isReady: boolean;
    readyTime?: Date | null;
    isCompleted: boolean;
    completedTime?: Date | null;
    storeName?: string | null;
    orderLines: OrderLine[];
    orderLogs: OrderLog[];
    customerName: string | null;
    customerPhone: string | null;
}