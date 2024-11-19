import { roles } from '@/api/business/roleApi';
import { Role } from '@/type/Role';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';


export const fetchRoleData = createAsyncThunk(
    'roles/getData',
    async () => {
        try {
            const response = await roles();

            if (response?.success && response?.data) {
                return {
                    roles: response?.data?.items,  // Or any other default value
                }
            }
            return {
                roles: [],
            };
        } catch (error) {
            console.log(error);
            return {
                roles: [],
            }
        }
    }
)

type sliceType = {
    roles: Role[]
}

const initialState: sliceType = {
    roles: [],
}

const mainRoleSlice = createSlice({
    name: 'main-role',
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchRoleData.fulfilled, (state, action) => {
            state.roles = action.payload.roles;
        })
    },

})

export default mainRoleSlice.reducer;