import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {

  private BASE_URL = "https://localhost:7212/api";
  private AUTH_URL = "https://localhost:7169";

  constructor(private cookieService: CookieService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {

    const clientToken = this.cookieService.get('X-Client-Access-Token');
    const userToken = this.cookieService.get('X-User-Access-Token');

    if (!userToken) {

    }

    const clone = request.clone({
      withCredentials: false
    });

    return next.handle(clone);
  }
}
