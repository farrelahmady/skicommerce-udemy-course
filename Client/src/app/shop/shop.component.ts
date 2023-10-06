import { ShopService } from './shop.service';
import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';
import { Brand } from '../shared/models/brand';
import { Type } from '../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  shopParam = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical', value: '' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];
  totalCount = 0;

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }

  getProducts() {
    this.shopService.getProducts(this.shopParam).subscribe({
      next: (res) => {
        this.products = res.data;
        this.shopParam.pageNumber = res.pageIndex;
        this.shopParam.pageSize = res.pageSize;
        this.totalCount = res.count;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getBrands() {
    this.shopService.getBrands().subscribe({
      next: (res) => {
        this.brands = [{ id: 0, name: 'All' }, ...res];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  getTypes() {
    this.shopService.getTypes().subscribe({
      next: (res) => {
        this.types = [{ id: 0, name: 'All' }, ...res];
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  onBrandSelected(brandId: number) {
    this.shopParam.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParam.typeId = typeId;
    this.getProducts();
  }

  onSortSelected(event: any) {
    this.shopParam.sort = event.target.value;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParam.pageNumber !== event.page) {
      this.shopParam.pageNumber = event.page;
      this.getProducts();
    }
  }
}
