import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Pagination } from '../shared/models/pagination';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:8000/api';

  constructor(private http: HttpClient) {}

  getProducts(brandId?: number, typeId?: number, sort?: string) {
    let params = new HttpParams();

    if (brandId) params = params.append('brandId', brandId);
    if (typeId) params = params.append('typeId', typeId);
    if (sort) params = params.append('sort', sort);

    console.log(params);

    return this.http.get<Pagination<Product[]>>(`${this.baseUrl}/product`, {
      params: params,
    });
  }

  getBrands() {
    return this.http.get<Brand[]>(`${this.baseUrl}/product/brand`);
  }

  getTypes() {
    return this.http.get<Type[]>(`${this.baseUrl}/product/type`);
  }
}
