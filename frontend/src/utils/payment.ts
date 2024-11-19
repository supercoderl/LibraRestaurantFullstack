export const checkVNPay = (entries: IterableIterator<[string, string]>) => {
    for (const [key, value] of entries) {
        if (key.startsWith('vnp') && value) {
            return true;
        }
    }
    return false;
};

export const checkStripe = (entries: IterableIterator<[string, string]>) => {
    for (const [key, value] of entries) {
        if (key.startsWith('stripe') && value) {
            return true;
        }
    }
    return false;
};