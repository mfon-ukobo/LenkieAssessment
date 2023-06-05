import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from 'projects/core/src/lib/services/auth.service';

@Component({
  selector: 'app-base-layout',
  templateUrl: './base-layout.component.html',
  styleUrls: ['./base-layout.component.scss']
})
export class BaseLayoutComponent {
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
