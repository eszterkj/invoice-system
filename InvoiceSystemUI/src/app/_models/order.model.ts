import { Customer } from "./customer.model";
import { Product } from "./product.model";

export class Order {
  id!: number;
  customerId!: number;
  customer?: Customer;
  orderDate!: Date;
  items!: OrderItem[];
  total!: number;
}

export class OrderItem {
  id!: number;
  orderId!: number;
  productId!: number;
  product?: Product;
  quantity!: number;
  unitPrice!: number;
  discount!: number;
}

export interface CreateOrderDto {
  customerId: number;
  items: CreateOrderItemDto[];
}

export interface CreateOrderItemDto {
  productId: number;
  quantity: number;
}
