export interface Store {
  storeId: string;
  name: string;
  cityId: number;
  districtId: number;
  wardId: number;
  isActive: boolean;
  taxCode?: string | null;
  address: string;
  gpsLocation?: string | null;
  postalCode?: string | null;
  phone?: string | null;
  fax?: string | null;
  email?: string | null;
  website?: string | null;
  logo?: string | null;
  bankBranch?: string | null;
  bankCode?: string | null;
  bankAccount?: string | null;
}