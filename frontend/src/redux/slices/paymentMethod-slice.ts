import { paymentMethods } from '@/api/business/paymentMethodApi';
import { PaymentMethod } from '@/type/PaymentMethod';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';


export const fetchDataPaymentMethod = createAsyncThunk(
  'paymentMethods/getData',
  async () => {
    try {
      const response = await paymentMethods();
      if (response?.success && response?.data) {
        return {
          paymentMethods: response?.data?.items,
        }
      }
      return {
        paymentMethods: []
      };
    } catch (error) {
      console.log(error);
      return {
        paymentMethods: []
      }
    }
  }
)

type sliceType = {
    paymentMethods: PaymentMethod[]
}

const initialState: sliceType = {
    paymentMethods: []
}

const mainPaymentMethodSlice = createSlice({
  name: 'main-payment-method',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(fetchDataPaymentMethod.fulfilled, (state, action) => {
      state.paymentMethods = action.payload.paymentMethods;
    })
  },

})

export default mainPaymentMethodSlice.reducer;