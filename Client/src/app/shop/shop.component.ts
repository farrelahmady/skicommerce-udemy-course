import { ShopService } from './shop.service';
import { Component, OnInit } from '@angular/core';
import { Product } from '../shared/models/product';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  products: Product[] = [];

  constructor(private shopService: ShopService) {}
  ngOnInit(): void {
    this.shopService.getProducts().subscribe({
      next: (res) => {
        this.products = res.data;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
