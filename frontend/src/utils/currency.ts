export const formatCurrency = (amount: number): string => {
    return amount.toLocaleString('vi-VN') + ' ₫';
}

export const convertVNDToUSD = (vndAmount: number) => {
    const exchangeRate = 25; // Tỷ giá hối đoái giả định: 1 USD = 25,060 VND
    const usdAmount = vndAmount / exchangeRate;
    return usdAmount.toFixed(0); // Làm tròn đến 2 chữ số thập phân
}