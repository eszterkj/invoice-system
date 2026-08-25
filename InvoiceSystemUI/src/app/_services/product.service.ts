import { Observable } from 'rxjs';
import { Product } from '../_models/product.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiURL = 'http://localhost:5270/api/products';

  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiURL);
  }

  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiURL}/${id}`);
  }

  createProduct(){}
}
