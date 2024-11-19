import { Employee } from "@/type/Employee"

export const keys = {
  KEY_TOKEN: 'token',
  KEY_CURRENT_USER: 'current_user',
  KEY_REFRESH_TOKEN: 'refresh_token'
}

export function set(key: string, data: string) {
  if (typeof window !== 'undefined' && window.localStorage) {
    window.localStorage.setItem(key, data)
  }
}

export function get(key: string): string | null {
  if (typeof window !== 'undefined' && window.localStorage) {
    return window.localStorage.getItem(key)
  }
  return null;
}

export function remove(key: string) {
  if (typeof window !== 'undefined' && window.localStorage) {
    return window.localStorage.removeItem(key);
  }
  return;
}

export function clear() {
  if (typeof window !== 'undefined' && window.localStorage) {
    return window.localStorage.clear();
  }
  return;
}

export function getUser(): Employee | undefined {
  if (typeof window !== 'undefined' && window.localStorage) {
    const userString = window.localStorage.getItem(keys.KEY_CURRENT_USER)
    if (userString) {
      return JSON.parse(userString)
    }
    return undefined
  }
  return undefined
}

export function setUser(user: Employee) {
  if (typeof window !== 'undefined' && window.localStorage) {
    window.localStorage.setItem(keys.KEY_CURRENT_USER, JSON.stringify(user))
  }
}
