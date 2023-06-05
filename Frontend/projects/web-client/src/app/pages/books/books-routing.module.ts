import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookResolver } from '../../resolvers/book.resolver';
import { CreateBookComponent } from './create-book/create-book.component';
import { PermissionGuard } from '../../guards/permission.guard';

const routes: Routes = [
  { path: '', component: BooksComponent },
  {
    path: 'details/:id',
    component: BookDetailsComponent,
    resolve: { book: BookResolver }
  },
  {
    path: 'create',
    component: CreateBookComponent,
    data: {
      permissions: ['write:books']
    },
    canActivate: [PermissionGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BooksRoutingModule {}
