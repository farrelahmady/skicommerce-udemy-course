import { ShopParams } from './../shared/models/shopParams';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/models/product';
import { Pagination } from '../shared/models/pagination';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl = 'https://localhost:8000/api';

  constructor(private http: HttpClient) {}

  /**
   * Retrieves products based on the provided shop parameters.
   *
   * @param {ShopParams} shopParam - The shop parameters object.
   * @returns {Observable<Pagination<Product[]>>} - An observable of the paginated product list.
   */
  getProducts(shopParam: ShopParams): Observable<Pagination<Product[]>> {
    let params = new HttpParams();
    if (shopParam.brandId > 0)
      params = params.append('brandId', shopParam.brandId);
    if (shopParam.typeId > 0)
      params = params.append('typeId', shopParam.typeId);

    if (shopParam.search) params = params.append('search', shopParam.search);

    params = params.append('sort', shopParam.sort);
    params = params.append('pageSize', shopParam.pageSize);
    params = params.append('pageIndex', shopParam.pageNumber);

    console.log('Params:', params.toString());

    return this.http.get<Pagination<Product[]>>(`${this.baseUrl}/product`, {
      params,
    });
  }

  /**
   * Get a product by ID.
   *
   * @param id - The ID of the product.
   * @returns The product with the specified ID.
   */
  getProduct(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/product/${id}`);
  }

  /**
   * Returns an array of brands.
   *
   * @returns An array of `Brand` objects.
   */
  getBrands(): Observable<Brand[]> {
    return this.http.get<Brand[]>(`${this.baseUrl}/product/brand`);
  }

  /**
   * Get the types of products.
   * @returns An observable that emits an array of Type objects.
   */
  getTypes(): Observable<Type[]> {
    return this.http.get<Type[]>(`${this.baseUrl}/product/type`);
  }
}
