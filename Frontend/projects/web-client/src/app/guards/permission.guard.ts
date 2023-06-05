import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthService } from 'projects/core/src/lib/services/auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PermissionGuard implements CanActivate {

  constructor(private auth: AuthService) {

  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

      const userPermissions = this.auth.user.profile['permissions'] as string[];
      const routePermissions = route.data['permissions'];

      if (!routePermissions || !routePermissions.length) {
        return true;
      }

      return this.auth.hasPermissions(routePermissions);
  }

}
