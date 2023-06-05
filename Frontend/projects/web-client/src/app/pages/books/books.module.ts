import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BooksRoutingModule } from './books-routing.module';
import { BooksComponent } from './books.component';
import { SharedModule } from '../../shared/shared.module';
import { BookDetailsComponent } from './book-details/book-details.component';
import { FormsModule } from '@angular/forms';
import { CreateBookComponent } from './create-book/create-book.component';


@NgModule({
  declarations: [
    BooksComponent,
    BookDetailsComponent,
    CreateBookComponent
  ],
  imports: [
    CommonModule,
    BooksRoutingModule,
    SharedModule,
    FormsModule
  ]
})
export class BooksModule { }
