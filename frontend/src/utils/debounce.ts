export function debounce<T extends (...args: any[]) => void>(func: T, delay: number): T {
    let timeoutId: ReturnType<typeof setTimeout> | null = null;

    return function (...args: Parameters<T>) {
        if (timeoutId) {
            clearTimeout(timeoutId);
        }
        timeoutId = setTimeout(() => func(...args), delay);
    } as T;
}