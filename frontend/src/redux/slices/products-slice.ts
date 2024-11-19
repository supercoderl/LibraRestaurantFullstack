import { deleteItem, itemBySlug, items } from '@/api/business/itemApi';
import Item from '@/type/Item';
import Query from '@/type/Query';
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';


export const fetchData = createAsyncThunk(
  'items/getData',
  async (data?: Query) => {
    try {
      const response = await items(data);

      if (response?.success && response?.data) {
        return {
          items: response?.data?.items,
        }
      }
      return {
        items: []
      };
    } catch (error) {
      console.log(error);
      return {
        items: []
      }
    }
  }
)

export const getItemAsync = createAsyncThunk(
  'items/getItem',
  async (slug: string, { rejectWithValue }) => {
    try {
      const response = await itemBySlug(slug);

      if (response?.success) {
        return response?.data;
      }
      return null;
    } catch (error: any) {
      return rejectWithValue(error.response.data);
    }
  }
)

export const deleteItemAsync = createAsyncThunk(
  'items/deleteItem',
  async (id: number, { rejectWithValue }) => {
    try {
      const response = await deleteItem(id);

      if (response?.success) {
        return true;
      }
      return false;
    } catch (error: any) {
      return rejectWithValue(error.response.data);
    }
  }
)

type sliceType = {
  items: Item[],
  loading: boolean,
  item: Item | null
}

const initialState: sliceType = {
  items: [],
  loading: false,
  item: null
}

const mainProductSlice = createSlice({
  name: 'main-product',
  initialState: initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchData.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchData.fulfilled, (state, action) => {
        state.items = action.payload.items;
        state.loading = false;
      })
      .addCase(fetchData.rejected, (state) => {
        state.loading = false;
      });
    builder
      .addCase(deleteItemAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(deleteItemAsync.fulfilled, (state, action) => {
        state.loading = false;
      })
      .addCase(deleteItemAsync.rejected, (state) => {
        state.loading = false;
      });
    builder
      .addCase(getItemAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(getItemAsync.fulfilled, (state, action) => {
        state.loading = false;
        state.item = action.payload;
      })
      .addCase(getItemAsync.rejected, (state) => {
        state.loading = false;
      });
  },

})

export default mainProductSlice.reducer;