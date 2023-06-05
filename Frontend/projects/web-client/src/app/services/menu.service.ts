import { Injectable } from '@angular/core';
import { AuthService } from 'projects/core/src/lib/services/auth.service';
import { MENU } from '../layout/menu';
import { MenuItem } from '../layout/menu-item';

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  constructor(private auth: AuthService) {}

  menu: MenuItem[] = MENU;

  getUserMenu(): MenuItem[] {
    let userMenu: MenuItem[] = [];
    for (let item of this.menu) {
      if (!item.permissions) {
        userMenu.push(item);
        continue;
      }

      let itemPermissions = item.permissions;
      if (this.auth.hasPermissions(itemPermissions))  {
        userMenu.push(item);
      }
    }

    return userMenu;
  }
}
