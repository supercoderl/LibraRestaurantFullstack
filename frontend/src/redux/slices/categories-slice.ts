import { categories, deleteCategory } from '@/api/business/categoryApi';
import Category from '@/type/Category';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';


export const fetchCategoryData = createAsyncThunk(
  'categories/getData',
  async () => {
    try {
      const response = await categories();

      if (response?.success && response?.data) {
        return {
          categories: response?.data?.items,  // Or any other default value
        }
      }
      return {
        categories: [],
      };
    } catch (error) {
      console.log(error);
      return {
        categories: [],
      }
    }
  }
)

export const deleteCategoryAsync = createAsyncThunk(
  'categories/deleteCategory',
  async (id: number, { rejectWithValue }) => {
    try {
      const response = await deleteCategory(id);

      if (response?.success) {
        return true;
      }
      return false;
    } catch (error: any) {
      return rejectWithValue(error?.response?.data);
    }
  }
)

type sliceType = {
  categories: Category[]
  currentCategory: number;
  loading: boolean;
}

const initialState: sliceType = {
  categories: [],
  currentCategory: 1,
  loading: false
}

const mainCategorySlice = createSlice({
  name: 'main-category',
  initialState: initialState,
  reducers: {
    setCurrentCategory: (state: Draft<typeof initialState>, action: PayloadAction<number>) => {
      state.currentCategory = action.payload
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchCategoryData.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchCategoryData.fulfilled, (state, action) => {
        state.categories = action.payload.categories;
        state.loading = false;
      })
      .addCase(fetchCategoryData.rejected, (state) => {
        state.loading = false;
      });
    builder
      .addCase(deleteCategoryAsync.pending, (state) => {
        state.loading = true;
      })
      .addCase(deleteCategoryAsync.fulfilled, (state, action) => {
        state.loading = false;
      })
      .addCase(deleteCategoryAsync.rejected, (state) => {
        state.loading = false;
      });
  },

})
export const { setCurrentCategory } = mainCategorySlice.actions;


export default mainCategorySlice.reducer;