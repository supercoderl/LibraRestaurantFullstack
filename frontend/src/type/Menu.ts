export default interface Menu {
    menuId: number;
    storeId: string;
    name: string;
    description?: string | null;
    isActive: boolean;
    storeName?: string | null;
}