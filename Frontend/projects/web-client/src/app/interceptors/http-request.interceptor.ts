import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable, from, switchMap } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'projects/core/src/lib/services/auth.service';
import { ApiConfig } from '../../api.config';

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(req.url.startsWith(ApiConfig.baseUrl)){
      return from(this.authService.getAccessToken())
        .pipe(switchMap(token => {
          const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
          const authRequest = req.clone({ headers });
          return next.handle(authRequest);
        }));
    }
    else {
      return next.handle(req);
    }
  }
}
