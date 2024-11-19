export interface Employee {
    id: string;
    storeId?: string | null;
    email: string;
    firstName: string;
    lastName: string;
    password: string;
    mobile: string;
    status: number;
    registeredAt: Date;
    lastLogginDate?: Date | null;
    fullName: string;
    storeName?: string | null;
    roleIds?: number[];
}