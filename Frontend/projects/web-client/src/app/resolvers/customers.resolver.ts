import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Customer } from 'projects/core/src/lib/interfaces/customer';
import { PagedList } from 'projects/core/src/lib/interfaces/paged-list';
import { UserService } from 'projects/core/src/lib/services/user.service';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomersResolver implements Resolve<PagedList<Customer>> {
  constructor(private userService: UserService) {

  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<PagedList<Customer>> {
    const page = route.queryParams['page'] ?? 1;
    const size = route.queryParams['size'] ?? 20;
    return this.userService.getCustomers({page: page, size: size});
  }
}
