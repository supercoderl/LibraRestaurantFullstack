import { Middleware } from "@reduxjs/toolkit";

const loadingDelayMiddleware: Middleware = (store) => (next) => async (action: any) => {
    if (action.type.endsWith('/fulfilled')) {
        // Thêm độ trễ 500ms trước khi tiếp tục
        await new Promise((resolve) => setTimeout(resolve, 1500));
    }

    // Cho phép action tiếp tục
    return next(action);
};

export default loadingDelayMiddleware;