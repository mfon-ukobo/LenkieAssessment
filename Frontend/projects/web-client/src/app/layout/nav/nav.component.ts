import { Component } from '@angular/core';
import { AuthService } from 'projects/core/src/lib/services/auth.service';
import { MENU } from '../menu';
import { MenuService } from '../../services/menu.service';
import { MenuItem } from '../menu-item';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {

  isLoggedIn!: boolean;
  menu:MenuItem[] = [];

  constructor(private auth: AuthService, private menuService: MenuService) {

  }

  ngOnInit() {
    this.auth.isAuthenticated()
      .then(val => this.isLoggedIn = val);

    this.menu = this.menuService.getUserMenu();
  }

  signOut() {
    this.auth.signOut();
  }
}
