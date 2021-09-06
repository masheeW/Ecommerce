import { Product } from "./Product";

export class OrderItem {
    id!: string;
    orderId!: string;
    productId!: string;
    quantity!: number;
    unitPrice!: number;
    totalPrice!: number;
    createdDate!: Date;
    createdBy!: string;
    updatedDate?: any;
    updatedBy?: any;
    product!: Product;
}

export class Order {
    id!: string;
    customer!: string;
    orderNumber: string = "M" + Math.random().toString().substr(2, 4);
    orderStatus?: any;
    orderDate: Date = new Date();
    totalAmount: number = 0;
    totalDiscount: number = 0;
    createdDate!: Date;
    createdBy!: string;
    updatedDate?: any;
    updatedBy?: any;
    orderItems: OrderItem[] = [];

    get subtotal(): number {

        const result = this.orderItems.reduce(
            (tot, val) => {
                return tot + (val.unitPrice * val.quantity);
        }, 0);
        return result;
    
    }
}


