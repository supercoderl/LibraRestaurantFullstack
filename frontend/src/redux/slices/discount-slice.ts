import { actionDiscount, deleteDiscount } from '@/api/business/discountApi';
import { Discount } from '@/type/Discount';
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';


export const postDiscountSelection = createAsyncThunk(
    'discount/postSelectedCounpon',
    async (data: Discount) => {
        try {
            const response = await actionDiscount(data, 'create');

            if (response?.success) {
                return true;
            }
            return false;
        } catch (error) {
            console.log(error);
            return false;
        }
    }
)

export const putDiscountSelection = createAsyncThunk(
    'discount/putSelectedCounpon',
    async (data: Discount) => {
        try {
            const response = await actionDiscount(data, 'update');

            if (response?.success) {
                return true;
            }
            return false;
        } catch (error) {
            console.log(error);
            return false;
        }
    }
)

export const deleteDiscountSelection = createAsyncThunk(
    'discount/deleteSelectedCounpon',
    async (id: number) => {
        try {
            const response = await deleteDiscount(id);

            if (response?.success) {
                return true;
            }
            return false;
        } catch (error) {
            console.log(error);
            return false;
        }
    }
)

type sliceType = {
    loading: boolean
}

const initialState: sliceType = {
    loading: false
}

const mainDiscountSlice = createSlice({
    name: 'main-discount',
    initialState: initialState,
    reducers: {

    },
    extraReducers: (builder) => {
        builder
            .addCase(postDiscountSelection.pending, (state) => {
                state.loading = true;
            })
            .addCase(postDiscountSelection.fulfilled, (state, action) => {
                state.loading = false;
            })
            .addCase(postDiscountSelection.rejected, (state) => {
                state.loading = false;
            });
        builder
            .addCase(putDiscountSelection.pending, (state) => {
                state.loading = true;
            })
            .addCase(putDiscountSelection.fulfilled, (state, action) => {
                state.loading = false;
            })
            .addCase(putDiscountSelection.rejected, (state) => {
                state.loading = false;
            });
        builder
            .addCase(deleteDiscountSelection.pending, (state) => {
                state.loading = true;
            })
            .addCase(deleteDiscountSelection.fulfilled, (state, action) => {
                state.loading = false;
            })
            .addCase(deleteDiscountSelection.rejected, (state) => {
                state.loading = false;
            });
    },

})

export default mainDiscountSlice.reducer;