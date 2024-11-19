export type Response = {
    data?: ListRep;
    detailedErrors: any[];
    errors?: any | null;
    success: boolean;
}

export type SingleResponse = {
    data?: any;
    detailedErrors: any[];
    errors?: any | null;
    success: boolean;
}

export type ListRep = {
    items: Array<any>;
    count: number;
    loading?: boolean;
    page: number;
    pageSize: number;
}