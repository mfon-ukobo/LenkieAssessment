import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInCallbackComponent } from './sign-in-callback.component';

const routes: Routes = [{ path: '', component: SignInCallbackComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SignInCallbackRoutingModule { }
