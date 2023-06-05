import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckOutRoutingModule } from './check-out-routing.module';
import { CheckOutComponent } from './check-out.component';
import { FormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    CheckOutComponent
  ],
  imports: [
    CommonModule,
    CheckOutRoutingModule,
    FormsModule
  ]
})
export class CheckOutModule { }
