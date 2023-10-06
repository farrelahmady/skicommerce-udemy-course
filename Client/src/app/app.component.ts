import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'SkiCommerce';
  products: IProduct[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http
      .get<IPagination<IProduct[]>>('https://localhost:8000/api/product')
      .subscribe({
        next: (res) => {
          this.products = res.data;
        },
        error: (err) => {
          console.log(err);
        },
        complete() {
          console.log('complete');
        },
      });
  }
}
