import { stores } from '@/api/business/storeApi';
import { Store } from '@/type/Store';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';


export const fetchStoreData = createAsyncThunk(
    'stores/getData',
    async () => {
        try {
            const response = await stores();

            if (response?.success && response?.data) {
                return {
                    stores: response?.data?.items,  // Or any other default value
                }
            }
            return {
                stores: [],
            };
        } catch (error) {
            console.log(error);
            return {
                stores: [],
            }
        }
    }
)

type sliceType = {
    stores: Store[]
}

const initialState: sliceType = {
    stores: [],
}

const mainStoreSlice = createSlice({
    name: 'main-store',
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchStoreData.fulfilled, (state, action) => {
            state.stores = action.payload.stores;
        })
    },

})

export default mainStoreSlice.reducer;