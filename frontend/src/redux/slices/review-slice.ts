import { actionReview, reviews } from "@/api/business/reviewApi";
import { Review } from "@/type/Review";
import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

export const fetchReviewData = createAsyncThunk(
    'reviews/getData',
    async (itemId: number, { rejectWithValue }) => {
        try {
            const response = await reviews({ itemId });

            if (response?.success && response?.data) {
                return {
                    reviews: response?.data?.items,  // Or any other default value
                }
            }
            return {
                reviews: [],
            };
        } catch (error: any) {
            return rejectWithValue(error?.response?.data);
        }
    }
)

export const postComment = createAsyncThunk(
    'reviews/comment',
    async (review: Review, { rejectWithValue }) => {
        try {
            const response = await actionReview(review, "create");

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
    reviews: Review[];
    loading: boolean;
    isSuccess: boolean;
}

const initialState: sliceType = {
    reviews: [],
    loading: false,
    isSuccess: false
}

const mainReviewSlice = createSlice({
    name: 'main-review',
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder
            .addCase(fetchReviewData.pending, (state) => {
                state.loading = true;
            })
            .addCase(fetchReviewData.fulfilled, (state, action) => {
                state.reviews = action.payload.reviews;
                state.loading = false;
            })
            .addCase(fetchReviewData.rejected, (state) => {
                state.loading = false;
            });
        builder
            .addCase(postComment.pending, (state) => {
                state.loading = true;
            })
            .addCase(postComment.fulfilled, (state, action) => {
                state.isSuccess = action.payload;
                state.loading = false;
            })
            .addCase(postComment.rejected, (state) => {
                state.loading = false;
            });
    },

})

export default mainReviewSlice.reducer;