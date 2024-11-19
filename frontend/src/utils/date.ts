import { TFunction } from "i18next";
import moment from "moment";

export const dateFormatter = (date: Date, format?: string) => {
    return date ? moment(date).format(format || "DD-MM-YYYY") : "N/A";
}

export function formatDate(date: Date, t: TFunction<"translation", undefined>) {
    const daysOfWeek = t('daysOfWeek', { returnObjects: true }) as string[];
    const monthsOfYear = t('monthsOfYear', { returnObjects: true }) as string[];

    const dayOfWeek = daysOfWeek[date.getDay()];
    const day = date.getDate();
    const month = monthsOfYear[date.getMonth()];
    const year = date.getFullYear();

    // Định dạng ngày dựa trên ngôn ngữ
    return t('dateFormat', { dayOfWeek, day, month, year });
}

export function generateOrderNo(tableNumber: number) {
    const now = new Date();
    const day = String(now.getDate()).padStart(2, '0');
    const month = String(now.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const year = now.getFullYear();
    const hours = String(now.getHours()).padStart(2, '0');
    const minutes = String(now.getMinutes()).padStart(2, '0');
    const seconds = String(now.getSeconds()).padStart(2, '0');

    const orderNo = `${day}${month}${year}${hours}${minutes}${seconds}${String(tableNumber).padStart(2, '0')}`;

    return orderNo;
}

export function checkTimeDifference(inputDate: any, minutesToCheck: number) {
    const currentTime = new Date(); // Lấy thời gian hiện tại
    const timeDifference = Number(currentTime) - Number(new Date(inputDate)); // Tính sự chênh lệch giữa 2 thời gian (milliseconds)

    // So sánh chênh lệch có lớn hơn 10 phút (600000 milliseconds)
    if (timeDifference > minutesToCheck * 60 * 1000) {
        return true; // Đã lớn hơn số phút cần kiểm tra
    } else {
        return false; // Chưa đến số phút cần kiểm tra
    }
}