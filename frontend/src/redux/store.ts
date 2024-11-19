import { configureStore } from '@reduxjs/toolkit';
import { combineReducers } from 'redux';
import cartSlice from './slices/cart-slice';
import mainProductSlice, { fetchData } from './slices/products-slice';
import { TypedUseSelectorHook, useDispatch, useSelector } from 'react-redux'
import storage from 'redux-persist/lib/storage';
import { persistReducer } from 'redux-persist';
import emblaSlice from './slices/embla-slice';
import reservationSlice, { getStatus } from './slices/reservation-slice';
import mainPaymentMethodSlice, { fetchDataPaymentMethod } from './slices/paymentMethod-slice';
import mainCategorySlice, { fetchCategoryData } from './slices/categories-slice';
import mainLocationSlice, { fetchLocationData } from './slices/locations-slice';
import mainDashboardSlice, { fetchDataDashboard } from './slices/dashboard-slice';
import mainAuthSlice from './slices/auth-slice';
import mainStoreSlice, { fetchStoreData } from './slices/store-slice';
import mainRoleSlice, { fetchRoleData } from './slices/roles-slice';
import mainEmployeeSlice, { fetchEmployeeData } from './slices/employee-slice';
import mainNotificationSlice, { fetchNotifications } from './slices/message-slice';
import mainDiscountTypeSlice, { fetchDiscountTypeData } from './slices/discountTypes-slice';
import mainDiscountSlice from './slices/discount-slice';
import mainReviewSlice from './slices/review-slice';
import loadingDelayMiddleware from 'middlewares/loading';

const reducers = combineReducers({
  cart: cartSlice,
  mainProductSlice: mainProductSlice,
  emblaStore: emblaSlice,
  reservation: reservationSlice,
  mainPaymentMethodSlice: mainPaymentMethodSlice,
  mainCategorySlice: mainCategorySlice,
  mainLocationSlice: mainLocationSlice,
  mainDashboardSlice: mainDashboardSlice,
  mainAuthSlice: mainAuthSlice,
  mainStoreSlice: mainStoreSlice,
  mainRoleSlice: mainRoleSlice,
  mainEmployeeSlice: mainEmployeeSlice,
  mainNotificationSlice: mainNotificationSlice,
  mainDiscountTypeSlice: mainDiscountTypeSlice,
  mainDiscountSlice: mainDiscountSlice,
  mainReviewSlice: mainReviewSlice
});


const persistConfig = {
  key: 'root',
  storage,
  whitelist: ['cart', 'reservation']
};

const persistedReducer = persistReducer(persistConfig, reducers);

const store = configureStore({
  reducer: persistedReducer,
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({
      serializableCheck: false,
    }).concat(loadingDelayMiddleware),
});

export type RootState = ReturnType<typeof store.getState>

export type AppDispatch = typeof store.dispatch

export const useStoreDispatch = () => useDispatch<AppDispatch>()
export const useStoreSelector: TypedUseSelectorHook<RootState> = useSelector

store.dispatch(fetchData());
store.dispatch(fetchDataPaymentMethod());
store.dispatch(fetchCategoryData());
// store.dispatch(fetchLocationData());
store.dispatch(fetchDataDashboard());
store.dispatch(fetchStoreData());
store.dispatch(fetchRoleData());
store.dispatch(fetchEmployeeData());
store.dispatch(fetchNotifications());
store.dispatch(fetchDiscountTypeData());

export default store