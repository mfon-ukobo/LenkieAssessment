import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', loadChildren: () => import('./pages/home/home.module').then(m => m.HomeModule) },
  { path: 'signin-callback', loadChildren: () => import('./pages/sign-in-callback/sign-in-callback.module').then(m => m.SignInCallbackModule) },
  { path: 'books', loadChildren: () => import('./pages/books/books.module').then(m => m.BooksModule) },
  { path: 'check-out/:id', loadChildren: () => import('./pages/check-out/check-out.module').then(m => m.CheckOutModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
