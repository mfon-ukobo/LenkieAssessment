import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BaseLayoutComponent } from './layout/base-layout/base-layout.component';
import { PermissionGuard } from './guards/permission.guard';
import { UserPermissions } from 'projects/core/src/lib/enums/permissions';

const routes: Routes = [
  {
    path: 'signin-callback',
    loadChildren: () =>
      import('./pages/sign-in-callback/sign-in-callback.module').then(
        (m) => m.SignInCallbackModule
      ),
  },
  {
    path: '',
    component: BaseLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./pages/home/home.module').then((m) => m.HomeModule),
      },
      {
        path: 'books',
        loadChildren: () =>
          import('./pages/books/books.module').then((m) => m.BooksModule),
      },
      {
        path: 'check-out/:id',
        data: {
          permissions: [UserPermissions.writeBooks, UserPermissions.writeUsers]
        },
        canActivate: [PermissionGuard],
        loadChildren: () =>
          import('./pages/check-out/check-out.module').then(
            (m) => m.CheckOutModule
          ),
      },
      {
        path: 'users',
        data: {
          permissions: [UserPermissions.readUsers]
        },
        canActivate: [PermissionGuard],
        loadChildren: () =>
          import('./pages/users/users.module').then((m) => m.UsersModule),
      },
    ],
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
