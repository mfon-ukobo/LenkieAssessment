import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SignInCallbackRoutingModule } from './sign-in-callback-routing.module';
import { SignInCallbackComponent } from './sign-in-callback.component';


@NgModule({
  declarations: [
    SignInCallbackComponent
  ],
  imports: [
    CommonModule,
    SignInCallbackRoutingModule
  ]
})
export class SignInCallbackModule { }
