import { discountTypeByCode, discountTypes } from '@/api/business/discountTypeApi';
import { DiscountType } from '@/type/DiscountType';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../store';
import { toast } from 'react-toastify';
import Item from '@/type/Item';


export const fetchDiscountTypeData = createAsyncThunk(
    'discountTypes/getData',
    async () => {
        try {
            const response = await discountTypes();

            if (response?.success && response?.data) {
                return {
                    discountTypes: response?.data?.items,  // Or any other default value
                }
            }
            return {
                discountTypes: [],
            };
        } catch (error) {
            console.log(error);
            return {
                discountTypes: [],
            }
        }
    }
)

const calculateTotalOrderValue = (items: { item: Item; quantityOrder: number; }[]) => {
    return items.reduce((total, item) => total + item.item.price * item.quantityOrder, 0);
};

export const fetchDiscountTypeByCodeData = createAsyncThunk(
    'discountType/getByCode',
    async (code: string, { getState }) => {
        try {
            if (code === "") {
                return null;
            }

            const response = await discountTypeByCode(code);

            const state = getState() as RootState;
            const itemInCarts = state.cart.itemsInCart;
            const totalOrderValue = calculateTotalOrderValue(itemInCarts);

            if (response?.success && response?.data) {
                const discountType = response.data;
                if (discountType.minItemQuantity > itemInCarts.length) {
                    toast(`Yêu cầu tối thiểu ${discountType.minItemQuantity} món để áp dụng.`, { type: "error" });
                    return null;
                }

                else if (discountType.minOrderValue > totalOrderValue) {
                    toast(`Tổng thanh toán phải ít nhất ${discountType.minOrderValue} để áp dụng.`, { type: "error" });
                    return null;
                }

                return discountType;
            }
            return null;
        } catch (error) {
            console.log(error);
            return null;
        }
    }
)

type sliceType = {
    discountTypes: DiscountType[],
    discountType: DiscountType | null,
    loading: boolean
}

const initialState: sliceType = {
    discountTypes: [],
    discountType: null,
    loading: false
}

const mainDiscountTypeSlice = createSlice({
    name: 'main-discountType',
    initialState: initialState,
    reducers: {
        updateDiscount: (state, action) => {
            state.discountType = action.payload
        }
    },
    extraReducers: (builder) => {
        builder.addCase(fetchDiscountTypeData.fulfilled, (state, action) => {
            state.discountTypes = action.payload.discountTypes;
        });
        builder
            .addCase(fetchDiscountTypeByCodeData.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchDiscountTypeByCodeData.fulfilled, (state, action) => {
                state.loading = false;
                state.discountType = action.payload;
            })
            .addCase(fetchDiscountTypeData.rejected, (state) => {
                state.loading = false;
            })
    },

})

export const { updateDiscount } = mainDiscountTypeSlice.actions;

export default mainDiscountTypeSlice.reducer;