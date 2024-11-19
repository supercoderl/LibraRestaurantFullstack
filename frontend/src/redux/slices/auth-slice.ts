import { authenticate, currentUser, logout, socialAuthenticate } from "@/api/business/userApi"
import { clear, get, keys, remove, set } from "@/utils/localStorage";
import { createAsyncThunk, createSlice, PayloadAction } from "@reduxjs/toolkit"
import axios from "axios";

export const login = createAsyncThunk('auth/login', async (data: { email: string, password: string }, { dispatch }) => {
    try {
        const res = await authenticate(data);
        // If Error or Token Doesn't Exist
        if (res?.data && res?.data !== "") {
            const token = res.data.accessToken;
            set(keys.KEY_TOKEN, token);

            const refreshToken = res.data.refreshToken;
            set(keys.KEY_REFRESH_TOKEN, refreshToken);

            // Validate User By Token
            dispatch(validateUser(token))
        }
    } catch (err: unknown) {
        if (axios.isAxiosError(err)) {
            // Đây là lỗi từ Axios, bạn có thể truy cập vào `err.response.data.message`
            dispatch(setMessage({ type: "error", message: err?.response?.data?.message }));
        } else if (err instanceof Error) {
            // Đây là một lỗi JavaScript thông thường
            dispatch(setMessage({ type: "error", message: err.message }));
        } else {
            // Lỗi khác không xác định
            dispatch(setMessage({ type: "error", message: "An unknown error occurred" }));
        }
    }
})

// Validate User By Token
export const validateUser = createAsyncThunk('auth/validateUser', async (token: any, { dispatch }) => {
    try {

        // If Token Doesn't Exist
        if (!token) {
            throw new Error('User Not Found')
        }

        const res = await currentUser();

        // If Error or User Doesn't Exist
        if (!res?.data) {
            throw new Error('User Not Found')
        }

        const user = res.data

        // Save `user` to localStorage
        set(keys.KEY_CURRENT_USER, JSON.stringify(user));

        return {
            token,
            user
        };

    } catch (err) {
        console.error(err)
    }
})

// Login by google
export const loginBySocial = createAsyncThunk('auth/loginBySocial', async (data: { provider: string, idToken: string }, { dispatch }) => {
    try {
        const res = await socialAuthenticate(data);

        if (res?.data && res?.data !== "") {
            const token = res.data.accessToken;
            set(keys.KEY_TOKEN, token);

            const refreshToken = res.data.refreshToken;
            set(keys.KEY_REFRESH_TOKEN, refreshToken);

            // Validate User By Token
            dispatch(validateUser(token))
        }

    } catch (err: unknown) {
        if (axios.isAxiosError(err)) {
            // Đây là lỗi từ Axios, bạn có thể truy cập vào `err.response.data.message`
            dispatch(setMessage({ type: "error", message: err?.response?.data?.message }));
        } else if (err instanceof Error) {
            // Đây là một lỗi JavaScript thông thường
            dispatch(setMessage({ type: "error", message: err.message }));
        } else {
            // Lỗi khác không xác định
            dispatch(setMessage({ type: "error", message: "An unknown error occurred" }));
        }
    }
})

// Logout Action
export const handleLogout = createAsyncThunk('auth/logout', async (e, { dispatch, rejectWithValue }) => {
    const refreshToken = get(keys.KEY_REFRESH_TOKEN);

    try {
        // Gọi API để revoke refreshToken
        if (refreshToken) {
            const res = await logout({ refreshToken });
        };

        // Clear localStorage
        clear();

        // Cập nhật Redux Store
        remove(keys.KEY_CURRENT_USER);
        remove(keys.KEY_REFRESH_TOKEN);
        remove(keys.KEY_TOKEN);

    } catch (error: any) {
        console.error('Logout failed:', error);
        return rejectWithValue(error.response?.data || 'Logout failed');
    }
})

interface Message {
    type: string,
    message: string
}

interface authState {
    isAuthenticated: boolean,
    isAuthenticating: boolean,
    token: null | string
    user: object,
    message: Message
}

const initialState: authState = {
    isAuthenticated: false,
    isAuthenticating: false,
    token: null,
    user: {},
    message: {
        type: "",
        message: ""
    },
}

const authSlice = createSlice({
    name: 'main-auth',
    initialState,
    reducers: {
        setIsAuthenticated: (state, action: PayloadAction<boolean>) => {
            state.isAuthenticated = action.payload
        },
        setIsAuthenticating: (state, action: PayloadAction<boolean>) => {
            state.isAuthenticating = action.payload
        },
        setToken: (state, action: PayloadAction<null | string>) => {
            state.token = action.payload
        },
        setUser: (state, action: PayloadAction<object>) => {
            state.user = action.payload
        },
        setMessage: (state, action: PayloadAction<Message>) => {
            state.message = action.payload;
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(login.pending, (state) => {
                state.isAuthenticating = true;
            })
            .addCase(login.fulfilled, (state, action) => {
                state.isAuthenticating = false;
            })
            .addCase(login.rejected, (state) => {
                state.isAuthenticating = false;
                state.isAuthenticated = false;
                state.token = null;
                state.user = {};
            });
        builder
            .addCase(validateUser.pending, (state) => {
                state.isAuthenticating = true;
            })
            .addCase(validateUser.fulfilled, (state, action) => {
                console.log(action);
                state.isAuthenticating = false;
                state.isAuthenticated = true;
                state.token = action.payload?.token;
                state.user = action.payload?.user;
            })
            .addCase(validateUser.rejected, (state) => {
                state.isAuthenticating = false;
                state.isAuthenticated = false;
                state.token = null;
                state.user = {};
            });
        builder
            .addCase(handleLogout.pending, (state) => {
                state.isAuthenticating = true;
            })
            .addCase(handleLogout.fulfilled, (state, action) => {
                state.isAuthenticating = false;
                state.isAuthenticated = false;
                state.token = null;
                state.user = {};
            })
            .addCase(handleLogout.rejected, (state) => {
                state.isAuthenticating = false;
                state.isAuthenticated = false;
                state.token = null;
                state.user = {};
            });
        builder
            .addCase(loginBySocial.pending, (state) => {
                state.isAuthenticating = true;
            })
            .addCase(loginBySocial.fulfilled, (state, action) => {
                state.isAuthenticating = false;
            })
            .addCase(loginBySocial.rejected, (state) => {
                state.isAuthenticating = false;
                state.isAuthenticated = false;
                state.token = null;
                state.user = {};
            });
    },
})

export const { setIsAuthenticated, setIsAuthenticating, setToken, setUser, setMessage } = authSlice.actions
export default authSlice.reducer