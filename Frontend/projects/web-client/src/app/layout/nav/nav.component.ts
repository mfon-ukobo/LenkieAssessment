import { Component } from '@angular/core';
import { AuthService } from 'projects/core/src/lib/services/auth.service';
import { MENU } from '../menu';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {

  isLoggedIn!: boolean;
  menu = MENU;

  constructor(private auth: AuthService) {
    auth.isAuthenticated()
      .then(val => this.isLoggedIn = val);
  }

  signOut() {
    this.auth.signOut();
  }
}
