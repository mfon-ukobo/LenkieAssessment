import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import {
  User,
  UserManager,
  UserManagerSettings,
  WebStorageStateStore,
} from 'oidc-client';
import { ApiConfig } from 'projects/web-client/src/api.config';
import { BehaviorSubject, Subject } from 'rxjs';

const CLIENT_TOKEN_NAME = 'X-Client-Access-Token';
const USER_TOKEN_NAME = 'X-User-Access-Token';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private userManager!: UserManager;
  user!: User;
  private loginChanged$ = new Subject<boolean>();

  private get idpSettings(): UserManagerSettings {
    return {
      authority: ApiConfig.authority,
      client_id: ApiConfig.clientId,
      client_secret: ApiConfig.clientSecret,
      redirect_uri: `${ApiConfig.clientRoot}/signin-callback`,
      scope: 'openid profile libraryApi',
      response_type: 'code',
      post_logout_redirect_uri: `${ApiConfig.clientRoot}/signout-callback`,
      userStore: new WebStorageStateStore({ store: window.localStorage }),
      stateStore: new WebStorageStateStore({ store: window.localStorage }),
      monitorSession: false
    };
  }

  constructor() {
    this.userManager = new UserManager(this.idpSettings);
  }

  loginChanged = this.loginChanged$.asObservable();

  login() {
    this.userManager.signinRedirect();
  }

  finishLogin(): Promise<User> {
    return this.userManager.signinRedirectCallback().then((user) => {
      this.user = user;
      return user;
    });
  }

  isAuthenticated = (): Promise<boolean> => {
    return this.userManager.getUser().then((user) => {
      this.user = user as User;
      return this.checkUser(user);
    });
  };

  private checkUser = (user: User | null): boolean => {
    return !!user && !user.expired;
  };
}
