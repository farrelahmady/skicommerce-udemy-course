import { ShopParams } from './../shared/models/shopParams';
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

  getProducts(shopParam: ShopParams) {
    let params = new HttpParams();

    if (shopParam.brandId) params = params.append('brandId', shopParam.brandId);
    if (shopParam.typeId) params = params.append('typeId', shopParam.typeId);
    if (shopParam.search) params = params.append('search', shopParam.search);
    params = params.append('sort', shopParam.sort);
    params = params.append('pageSize', shopParam.pageSize);
    params = params.append('pageIndex', shopParam.pageNumber);

    console.log(shopParam);

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
