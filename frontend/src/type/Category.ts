export default interface Category {
  categoryId: number;
  name: string
  description?: string | null;
  isActive: boolean;
  picture?: string | null;
  itemNumber: number;
}
