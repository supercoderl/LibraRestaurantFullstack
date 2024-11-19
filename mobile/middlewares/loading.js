const loadingDelayMiddleware = store => next => async action => {
  if (action.type.endsWith('/fulfilled')) {
    // Thêm độ trễ 1500ms trước khi tiếp tục
    await new Promise(resolve => setTimeout(resolve, 1500))
  }

  // Cho phép action tiếp tục
  return next(action)
}

export default loadingDelayMiddleware
