import HttpClient from './iHttpImp'
import { keys, get, set } from '../utils/localStorage'
import { HTTP_METHODS, RequestConfig } from './interface/iHttp'
import { toast } from 'react-toastify';

const isLog = false
const baseUrl = process.env.NEXT_PUBLIC_API_URL || '';

export function getHeaders() {
    let token = get(keys.KEY_TOKEN)

    return {
        'Authorization': 'Bearer ' + token,
        'ngrok-skip-browser-warning': '69420'
    }
}
interface APIParams {
    url: string
    params?: any
    extra?: any
    data?: object
}

function _buildRequestConfig({
    method
}: {
    method: HTTP_METHODS
}): RequestConfig {
    return {
        headers: getHeaders(),
        method
    }
}

async function refreshToken() {
    try {
        const refreshToken = get(keys.KEY_REFRESH_TOKEN);
        const res = await HttpClient.request(
            baseUrl.concat('/Employee/refresh'), // Đường dẫn API refresh token
            {},
            { refreshToken }, // Gửi refreshToken để lấy accessToken mới
            _buildRequestConfig({ method: HTTP_METHODS.POST }) // HTTP POST method
        );

        if (res?.data && res?.data?.success && res?.data?.data?.accessToken) {
            // Lưu lại token mới
            set(keys.KEY_TOKEN, res?.data?.data?.accessToken);
            set(keys.KEY_REFRESH_TOKEN, res?.data?.data?.refreshToken);
            return res?.data?.data?.accessToken;
        } else {
            throw new Error('Failed to refresh token');
        }
    } catch (error) {
        console.error('Error refreshing token:', error);
        throw error;
    }
}

const errorHanding = async (err: any) => {
    isLog && console.log('-------- error --------')
    isLog && console.error(err)
    if (err?.response && err?.response?.data && err?.response?.data?.errors) {
        if (Array.isArray(err?.response?.data?.errors) && err?.response?.data?.errors.length > 0) {
            toast(err?.response?.data?.errors.join(', '), {
                type: "error",
            });
        }
        else {
            const errorMessages = Object.entries(err?.response?.data?.errors)
                .map(([key, messages]: [string, any]) => messages?.join(', ')).join(', ');
            toast(errorMessages, {
                type: "error",
            });
        }
    }
}

function _wrapperResponse<T>(data: any): T {
    isLog && console.log('-------- response --------')
    isLog && console.log(data)
    return data as T
}

export async function apiGet<T>(params: APIParams): Promise<T | undefined> {
    return _api<T>(params, {}, HTTP_METHODS.GET)
}

export async function apiPost<T>(params: APIParams, data: object): Promise<T | undefined> {
    return _api<T>(params, data, HTTP_METHODS.POST)
}

export async function apiDelete<T>(params: APIParams): Promise<T | undefined> {
    return _api<T>(params, {}, HTTP_METHODS.DELETE)
}

export async function apiPut<T>(params: APIParams, data: object): Promise<T | undefined> {
    return _api<T>(params, data, HTTP_METHODS.PUT)
}

async function _api<T>(params: APIParams, data: object, method: HTTP_METHODS) {
    isLog && console.log('-------- request start --------')
    isLog && console.log(params)
    isLog && console.log(method)
    isLog && console.log('-------- request end --------')
    try {
        let res = await HttpClient.request(
            baseUrl.concat(params.url),
            params.params,
            data,
            _buildRequestConfig({ method })
        )
        return _wrapperResponse<T>(res.data)
    } catch (error: any) {
        if (error.response && error.response.status === 401) {
            try {
                await refreshToken();
                // Thực hiện lại request với access token mới
                let res = await HttpClient.request(
                    baseUrl.concat(params.url),
                    params.params,
                    data,
                    _buildRequestConfig({ method })
                )

                return _wrapperResponse<T>(res.data)
            } catch (refreshError) {
                // Xử lý khi không refresh được token (ví dụ: chuyển hướng về trang đăng nhập)
                console.error('Error during token refresh:', refreshError);
                errorHanding(refreshError);
            }
        }
        else
            errorHanding(error)
    }
}
