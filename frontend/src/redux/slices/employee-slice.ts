import { actionEmployee, employees } from '@/api/business/userApi';
import { Employee } from '@/type/Employee';
import { createAsyncThunk, createSlice, Draft, PayloadAction } from '@reduxjs/toolkit';


export const fetchEmployeeData = createAsyncThunk(
    'employees/getData',
    async () => {
        try {
            const response = await employees();

            if (response?.success && response?.data) {
                return {
                    employees: response?.data?.items,  // Or any other default value
                }
            }
            return {
                employees: [],
            };
        } catch (error) {
            console.log(error);
            return {
                employees: [],
            }
        }
    }
)

export const updateEmployeeData = createAsyncThunk(
    'employee/update',
    async (data: Employee) => {
        try {
            const response = await actionEmployee(data, "update");

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
    employees: Employee[];
    loading: boolean;
}

const initialState: sliceType = {
    employees: [],
    loading: false
}

const mainEmployeeSlice = createSlice({
    name: 'main-employee',
    initialState: initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(fetchEmployeeData.fulfilled, (state, action) => {
            state.employees = action.payload.employees;
        });
        builder
            .addCase(updateEmployeeData.pending, (state) => {
                state.loading = true;
            })
            .addCase(updateEmployeeData.fulfilled, (state, action) => {
                state.loading = false;
            })
            .addCase(updateEmployeeData.rejected, (state) => {
                state.loading = false;
            });
    },

})

export default mainEmployeeSlice.reducer;