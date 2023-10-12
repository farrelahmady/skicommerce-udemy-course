import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop.service';
import { Product } from 'src/app/shared/models/product';
import { ActivatedRoute, Router } from '@angular/router';
import { BreadcrumbService } from 'xng-breadcrumb';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;
  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private bcService: BreadcrumbService
  ) {}

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.params['id']);
    this.loadProduct(id);
  }

  loadProduct(id: number) {
    this.shopService.getProduct(id).subscribe({
      next: (res) => {
        this.product = res;
        this.bcService.set('@productDetails', res.name);
      },
      error: (err) => {
        console.log(err.error.message);

        this.router.navigate(['/shop'], {});
      },
    });
  }
}
