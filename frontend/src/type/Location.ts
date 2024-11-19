export interface City {
    cityId: number;
    name: string;
    nameEn: string;
    fullname: string;
    fullnameEn: string;
    codeName: string
}

export interface District {
    districtId: number;
    name: string;
    nameEn: string;
    fullname: string;
    fullnameEn: string;
    codeName: string;
    cityId: number;
}

export interface Ward {
    wardId: number;
    name: string;
    nameEn: string;
    fullname: string;
    fullnameEn: string;
    codeName: string;
    districtId: number;
}