import { Component } from '@angular/core';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import {
  FormArray,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { Observable } from 'rxjs';

import { ProductService } from '../_services/product.service';
import { CustomerService } from '../_services/customer.service';
import { OrderService } from '../_services/order.service';

import { Product } from '../_models/product.model';
import { Customer } from '../_models/customer.model';
import { CreateOrderDto, Order } from '../_models/order.model';

@Component({
  selector: 'app-homepage',
  imports: [NgFor, NgIf, AsyncPipe, ReactiveFormsModule, FormsModule],
  templateUrl: './homepage.html',
  styleUrl: './homepage.scss',
})
export class Homepage {
  constructor(
    private productService: ProductService,
    private customerService: CustomerService,
    private orderService: OrderService,
  ) {}

  products$!: Observable<Product[]>;
  customers$!: Observable<Customer[]>;
  orders$!: Observable<Order[]>;

  selectedOrderId: number | null = null;

  orderForm = new FormGroup({
    customerId: new FormControl<number | null>(null, Validators.required),

    items: new FormArray([this.createOrderItem()]),
  });

  ngOnInit(): void {
    this.products$ = this.productService.getProducts();
    this.customers$ = this.customerService.getCustomers();
    this.orders$ = this.orderService.getOrders();
  }

  createOrderItem(): FormGroup {
    return new FormGroup({
      productId: new FormControl<number | null>(null, Validators.required),

      quantity: new FormControl<number>(1, [Validators.required, Validators.min(1)]),
    });
  }

  get items(): FormArray {
    return this.orderForm.controls.items;
  }

  addItem(): void {
    this.items.push(this.createOrderItem());
  }

  removeItem(index: number): void {
    if (this.items.length > 1) {
      this.items.removeAt(index);
    }
  }

  createOrder(): void {
    if (this.orderForm.invalid) {
      return;
    }

    const dto: CreateOrderDto = {
      customerId: this.orderForm.value.customerId!,
      items: this.items.value.map(
        (item: { productId: number | null; quantity: number | null }) => ({
          productId: item.productId!,
          quantity: item.quantity!,
        }),
      ),
    };

    this.orderService.createOrder(dto).subscribe({
      next: (order) => {
        console.log('Order created:', order);

        this.orders$ = this.orderService.getOrders();

        this.orderForm.reset({
          customerId: null,
        });

        this.items.clear();
        this.items.push(this.createOrderItem());
      },

      error: (error) => {
        console.error(error);
      },
    });
  }

  getInvoice() {
    if (this.selectedOrderId === null) {
      return;
    }

    window.open(`http://localhost:5270/api/orders/${this.selectedOrderId}/invoice`, '_blank');
  }

  showCustomers = false;
  showProducts = false;
  showOrders = false;

  listCustomers(): void {
    this.showCustomers = !this.showCustomers;
  }

  listProducts(): void {
    this.showProducts = !this.showProducts;
  }

  listOrders(): void {
    this.showOrders = !this.showOrders;
  }
}
