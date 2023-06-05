import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from 'projects/core/src/lib/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'WebClient';
  isAuthenticated!: boolean;

  constructor(private auth: AuthService, private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.auth.isAuthenticated()
    .then((data) => {
      this.isAuthenticated = data;
    });
  }

  signIn() {
    this.auth.login();
  }
}
