export const Status = {
  Available: 0,
  Reserved: 1,
  Occupied: 2,
  Cleaning: 3,
  OutOfService: 4,
  0: 'Có sẵn',
  1: 'Đã đặt trước',
  2: 'Đang phục vụ',
  3: 'Đang dọn dẹp',
  4: 'Không phục vụ',
}

export const OrderStatus = {
  Draft: 0,
  Confirmed: 1,
  InPreperation: 2,
  Ready: 3,
  Completed: 4,
  Canceled: 5,
  Delayed: 6,
  Paid: 7,
  Failed: 8,
  Refunded: 9,
}

export const PaymentStatus = {
  Success: 0,
  Fail: 1,
}

export const OrderStatusText = {
  0: 'Nháp',
  1: 'Xác nhận',
  2: 'Chuẩn bị',
  3: 'Sẵn sàng',
  4: 'Hoàn thành',
  5: 'Bị hủy',
  6: 'Bị hoãn',
  7: 'Đã thanh toán',
  8: 'Lỗi',
  9: 'Hoàn tiền',
}

export const CustomerLikeStatus = {
  NotSet: 0,
  Liked: 1,
  Disliked: 2,
}

export const PaymentMethod = {
  Paypal: 3,
  VNPay: 4,
  Stripe: 5,
  PayOS: 6,
}

export const UserStatus = {
  Active: 0,
  InActive: 1,
}

export const DiscountStatus = {
  NotYet: 0,
  Active: 1,
  Expired: 2,
  Unknow: 3,
}
