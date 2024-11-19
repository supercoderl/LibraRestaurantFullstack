import AsyncStorage from '@react-native-async-storage/async-storage'
import Toast from 'react-native-toast-message'

export async function _set(key, value) {
  try {
    await AsyncStorage.setItem(key, JSON.stringify(value))
  } catch (error) {
    return Toast.show({
      type: 'error',
      text1: 'Lỗi khi lưu tài nguyên!',
      text2: 'Chi tiết: ' + error?.message,
    })
  }
}

export async function _get(key) {
  try {
    const value = await AsyncStorage.getItem(key)
    if (!value) return null
    return JSON.parse(value)
  } catch (error) {
    return Toast.show({
      type: 'error',
      text1: 'Lỗi khi tải tài nguyên!',
      text2: 'Chi tiết: ' + error?.message,
    })
  }
}

export async function _remove(key) {
  try {
    return await AsyncStorage.removeItem(key)
  } catch (error) {
    return Toast.show({
      type: 'error',
      text1: 'Lỗi khi xóa tài nguyên!',
      text2: 'Chi tiết: ' + error?.message,
    })
  }
}
