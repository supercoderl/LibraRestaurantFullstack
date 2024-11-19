export default interface Dashboard {
    orderCount: number;
    paymentAmount: number;
    customer: {
      customerCountInThisMonth: number;
      customerCountInLastMonth: number;
      percentage: number;
      top5Customers: {
        customerName: string | null;
        customerPhone: string | null;
      }[]
    };
    top5Items: {
      title: string;
      quantity: number;
      price: number;
    }[];
  }
  