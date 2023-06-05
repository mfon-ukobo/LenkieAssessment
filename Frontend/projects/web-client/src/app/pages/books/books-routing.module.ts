import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BooksComponent } from './books.component';
import { BookDetailsComponent } from './book-details/book-details.component';
import { BookResolver } from '../../resolvers/book.resolver';
import { CreateBookComponent } from './create-book/create-book.component';
import { PermissionGuard } from '../../guards/permission.guard';
import { UserPermissions } from 'projects/core/src/lib/enums/permissions';
import { EditBookComponent } from './edit-book/edit-book.component';

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
      permissions: [UserPermissions.writeBooks]
    },
    canActivate: [PermissionGuard]
  },
  {
    path: 'edit/:id',
    component: EditBookComponent,
    resolve: { book: BookResolver }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class BooksRoutingModule {}
