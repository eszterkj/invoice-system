import { Observable } from 'rxjs';
import { CreateCustomerDto, Customer } from '../_models/customer.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private apiURL = 'http://localhost:5270/api/customers';

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<Customer[]> {
    return this.http.get<Customer[]>(this.apiURL);
  }

  getCustomer(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiURL}/${id}`);
  }

  createCustomer(customerDTO: CreateCustomerDto){
    return this.http.post<Customer>(this.apiURL, customerDTO);
  }
}
