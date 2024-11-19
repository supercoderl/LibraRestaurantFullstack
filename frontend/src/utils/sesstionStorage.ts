export function set(key: string, data: string) {
    sessionStorage.setItem(key, data)
}

export function get(key: string): string | null {
    return sessionStorage.getItem(key)
}