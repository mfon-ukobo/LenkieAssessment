import { Injectable } from '@angular/core';
import {
  Router, Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BookResolver implements Resolve<Book> {
  constructor(private bookService: BookService) {

  }

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<Book> {
    const bookId = route.params['id'];

    return this.bookService.getBook(bookId);
  }
}
