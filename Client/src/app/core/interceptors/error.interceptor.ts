import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err: HttpErrorResponse) => {
        if (err) {
          switch (err.status) {
            case 404:
              this.router.navigateByUrl('/not-found');
              break;

            case 400:
              if (err.error.errors) {
                throw err.error;
              } else {
                this.router.navigateByUrl('/server-error');
              }
              break;

            default:
              this.router.navigateByUrl('/server-error');
              break;
          }
        }

        if (!environment.production) {
          console.log(err);
        }

        this.toastr.error(err.error.message, err.status.toString() + 'Error');

        return throwError(() => new Error(err.message));
      })
    );
  }
}
