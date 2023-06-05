import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersComponent } from './users.component';
import { CustomersResolver } from '../../resolvers/customers.resolver';
import { CreateUserComponent } from './create-user/create-user.component';

const routes: Routes = [
  { path: '', component: UsersComponent, resolve: {customers: CustomersResolver} },
  { path: 'create', component: CreateUserComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
