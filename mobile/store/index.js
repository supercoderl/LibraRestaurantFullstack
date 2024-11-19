import AsyncStorage from '@react-native-async-storage/async-storage'
import { configureStore } from '@reduxjs/toolkit'
import { setupListeners } from '@reduxjs/toolkit/query'
import loadingDelayMiddleware from 'middlewares/loading'
import { persistReducer } from 'redux-persist'

//? Reducers
import { thunk } from 'redux-thunk'
import apiSlice from 'services/api'

import cartReducer from './slices/cart.slice'
import discountTypeReducer from './slices/discountType.slice'
import filtersReducer from './slices/filters.slice'
import itemReducer from './slices/item.slice'
import reservationReducer from './slices/reservation.slice'
import userReducer from './slices/user.slice'

const persistConfig = {
  key: 'root',
  version: 1,
  storage: AsyncStorage,
}

const cartPersistedReducer = persistReducer(persistConfig, cartReducer)
const userPersistedReducer = persistReducer(persistConfig, userReducer)
const reservationPersistedReducer = persistReducer(persistConfig, reservationReducer)
const discountTypePersisterdReducer = persistReducer(persistConfig, discountTypeReducer)
const itemPersistedReducer = persistReducer(persistConfig, itemReducer)

//? Actions
export * from './slices/user.slice'
export * from './slices/cart.slice'
export * from './slices/filters.slice'

export const store = configureStore({
  reducer: {
    user: userPersistedReducer,
    cart: cartPersistedReducer,
    filters: filtersReducer,
    reservation: reservationPersistedReducer,
    discountType: discountTypePersisterdReducer,
    item: itemPersistedReducer,
    [apiSlice.reducerPath]: apiSlice.reducer,
  },
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware({
      immutableCheck: false,
      serializableCheck: false,
    })
      .concat(thunk)
      .concat(apiSlice.middleware)
      .concat(loadingDelayMiddleware),
})

setupListeners(store.dispatch)

store.dispatch(
  apiSlice.endpoints.getItems.initiate({ categoryId: -1, includeDeleted: false, pageSize: 100 })
)
