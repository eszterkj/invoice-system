import { Observable } from 'rxjs';
import { CreateOrderDto, Order } from '../_models/order.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  private apiURL = 'http://localhost:5270/api/orders';

  constructor(private http: HttpClient) {}

  selectedOrderId: number | null = null;

  getOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(this.apiURL);
  }

  getOrder(id: number): Observable<Order> {
    return this.http.get<Order>(`${this.apiURL}/${id}`);
  }

  createOrder(orderDTO: CreateOrderDto){
    return this.http.post<Order>(this.apiURL,orderDTO);
  }
}
