import { createSlice } from '@reduxjs/toolkit'
import { _get } from 'utils/storage'

const initialState = {
  id: null,
  isChanged: false,
  capacity: 0,
  customerId: null,
  customerPhone: null,
  status: -1,
  storeId: null,
  tableNumber: -1,
}

const reservationSlice = createSlice({
  name: 'reservation',
  initialState,
  reducers: {
    updateReservation: (state, action) => {
      const {
        reservationId,
        isChanged,
        capacity,
        status,
        storeId,
        tableNumber,
        customerId,
        customerPhone,
      } = action.payload
      state.isChanged = isChanged
      state.id = reservationId
      state.capacity = capacity
      state.status = status
      state.storeId = storeId
      state.tableNumber = tableNumber
      state.customerId = customerId
      state.customerPhone = customerPhone
    },
    updateReservationStatus: (state, action) => {
      const { status, tableNumber } = action.payload
      return {
        ...state,
        status,
        tableNumber,
      }
    },
    updateReservationCustomer: (state, action) => {
      const { customerName, customerPhone } = action.payload
      return {
        ...state,
        customerName,
        customerPhone,
      }
    },
    clearReservation: state => {
      state.id = null
      state.isChanged = false
      state.capacity = 0
      state.customerId = null
      state.customerPhone = null
      state.status = -1
      state.storeId = null
      state.tableNumber = -1
    },
  },
})

export const {
  updateReservation,
  updateReservationStatus,
  updateReservationCustomer,
  clearReservation,
} = reservationSlice.actions

export default reservationSlice.reducer
