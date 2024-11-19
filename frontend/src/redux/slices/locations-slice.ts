import { locations } from '@/api/business/locationApi';
import { City, District, Ward } from '@/type/Location';
import { get, set } from '@/utils/localStorage';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';


export const fetchLocationData = createAsyncThunk(
  'locations/getData',
  async () => {
    try {
      const cities = get("cities");
      const districts = get("districts");
      const wards = get("wards");
      let locationResponse: sliceType = {
        cities: [],
        districts: [],
        wards: [],
        originalDistricts: [],
        originalWards: []
      };

      if (!cities) {
        const res = await locations({ isAll: true }, "city");
        if (res?.success && res?.data) {
          locationResponse = { ...locationResponse, cities: res?.data?.items };
          set("cities", JSON.stringify(res?.data?.items));
        }
      }

      else locationResponse = { ...locationResponse, cities: JSON.parse(cities) };

      if (!districts) {
        const res = await locations({ isAll: true }, "district");
        if (res?.success && res?.data) {
          locationResponse = { ...locationResponse, districts: res?.data?.items };
          set("districts", JSON.stringify(res?.data?.items));
        }
      }

      else locationResponse = { ...locationResponse, districts: JSON.parse(districts) };

      if (!wards) {
        const res = await locations({ isAll: true }, "ward");
        if (res?.success && res?.data) {
          locationResponse = { ...locationResponse, wards: res?.data?.items };
          set("wards", JSON.stringify(res?.data?.items));
        }
      }

      else locationResponse = { ...locationResponse, wards: JSON.parse(wards) };

      return locationResponse;
    } catch (error) {
      console.log(error);
      return {
        cities: [],
        districts: [],
        wards: []
      }
    }
  }
)

type sliceType = {
  cities: City[];
  districts: District[];
  wards: Ward[];
  originalDistricts: District[],
  originalWards: Ward[],
}

const initialState: sliceType = {
  cities: [],
  districts: [],
  wards: [],
  originalDistricts: [], // Lưu trữ toàn bộ districts
  originalWards: [], // Lưu trữ toàn bộ wards
}

const mainLocationSlice = createSlice({
  name: 'main-location',
  initialState: initialState,
  reducers: {
    filterDistrictsAndWards: (state, action: PayloadAction<number>) => {
      // Lọc districts từ dữ liệu gốc
      state.districts = state.originalDistricts.filter(d => d.cityId === action.payload);

      // Lấy danh sách districtId từ các districts đã lọc
      const filteredDistrictIds = state.districts.map(d => d.districtId);

      // Lọc wards từ dữ liệu gốc dựa trên các districtId đã lọc
      state.wards = state.originalWards.filter(w => filteredDistrictIds.includes(w.districtId));
    },
    filterWards: (state, action: PayloadAction<number>) => {
      // Lọc districts từ dữ liệu gốc
      state.wards = state.originalWards.filter(w => w.districtId === action.payload);
    }
  },
  extraReducers: (builder) => {
    builder.addCase(fetchLocationData.fulfilled, (state, action) => {
      state.cities = action.payload.cities;
      state.districts = action.payload.districts;
      state.wards = action.payload.wards;
      state.originalDistricts = action.payload.districts;
      state.originalWards = action.payload.wards;
    })
  },
})

export const { filterDistrictsAndWards, filterWards } = mainLocationSlice.actions;

export default mainLocationSlice.reducer;