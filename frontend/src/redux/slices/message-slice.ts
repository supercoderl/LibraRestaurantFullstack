import { items } from '@/api/business/itemApi';
import { messages, updateStatus } from '@/api/business/messageApi';
import Item from '@/type/Item';
import { Notification } from '@/type/Notification';
import Query from '@/type/Query';
import { createAsyncThunk, createSlice } from '@reduxjs/toolkit';


export const fetchNotifications = createAsyncThunk(
  'messages/getData',
  async (data?: Query) => {
    try {
      const response = await messages(data);
      if (response?.success && response?.data) {
        return {
          notifications: response?.data?.items,
          hasMore: response?.data?.pageSize < response?.data?.count,
          count: response?.data?.count
        }
      }
      return {
        notifications: [],
        hasMore: false,
        count: 0
      };
    } catch (error) {
      console.log(error);
      return {
        notifications: [],
        hasMore: false,
        count: 0
      }
    }
  }
)

export const updateNotificationStatus = createAsyncThunk(
  'messages/updateStatus',
  async (notification: Notification, { dispatch }) => {
    try {
      const updatedNotification = { ...notification, isRead: true };
      const response = await updateStatus(updatedNotification); // Gọi API cập nhật status

      if (response?.success) {
        // Sau khi cập nhật thành công, gọi lại fetchNotifications
        dispatch(fetchNotifications({ type: "order" }));
        return updatedNotification;
      }
    } catch (error) {
      console.error('Failed to update notification status:', error);
    }
  }
);

type sliceType = {
  notifications: Notification[],
  loading: boolean,
  loadingMessageId: number | null;
  unread: number;
  hasMore: boolean;
  size: number;
  count: number;
}

const initialState: sliceType = {
  notifications: [],
  loading: false,
  loadingMessageId: null,
  unread: 0,
  hasMore: true,
  size: 10,
  count: 0
}

const mainNotificationSlice = createSlice({
  name: 'main-notification',
  initialState: initialState,
  reducers: {
    addNewNotification: (state, action) => {
      state.notifications.unshift(action.payload); // Thêm thông báo mới lên đầu
    },
    setLoadingMessageId: (state, action) => {
      state.loadingMessageId = action.payload; // Cập nhật ID thông báo đang loading
    },
    clearLoadingMessageId: (state) => {
      state.loadingMessageId = null; // Xóa trạng thái loading
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchNotifications.pending, (state) => {
        state.loading = true;
      })
      .addCase(fetchNotifications.fulfilled, (state, action) => {
        state.notifications = action.payload.notifications;
        state.unread = action.payload.notifications.filter(x => !x.isRead).length;
        state.loading = false;
        state.size = state.size + 10 >= action.payload.count ? action.payload.count : state.size + 10;
        state.hasMore = action.payload.hasMore;
        state.count = action.payload.count;
      })
      .addCase(fetchNotifications.rejected, (state) => {
        state.loading = false;
      })
      .addCase(updateNotificationStatus.pending, (state, action) => {
        state.loadingMessageId = action.meta.arg.messageId;
      })
      .addCase(updateNotificationStatus.fulfilled, (state) => {
        state.loading = false;
        state.loadingMessageId = null;
      })
      .addCase(updateNotificationStatus.rejected, (state) => {
        state.loading = false;
        state.loadingMessageId = null;
      });
  },

})

export const {
  addNewNotification,
  setLoadingMessageId,
  clearLoadingMessageId, } = mainNotificationSlice.actions;
export default mainNotificationSlice.reducer;