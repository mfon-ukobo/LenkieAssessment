import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'projects/core/src/lib/services/auth.service';

@Component({
  selector: 'app-sign-in-callback',
  templateUrl: './sign-in-callback.component.html',
  styleUrls: ['./sign-in-callback.component.scss']
})
export class SignInCallbackComponent {

  constructor(private auth: AuthService, private router: Router) {

  }

  ngOnInit() {
    this.auth.finishLogin()
    .then(() => this.router.navigate(['/'], {replaceUrl: true}));
  }
}
