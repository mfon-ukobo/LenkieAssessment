import { Component } from '@angular/core';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { PagedList } from 'projects/core/src/lib/interfaces/paged-list';
import { BookService } from 'projects/core/src/lib/services/book.service';

@Component({
  selector: 'app-books',
  templateUrl: './books.component.html',
  styleUrls: ['./books.component.scss']
})
export class BooksComponent {
  constructor(private bookService: BookService) {
    this.getBooks();
  }

  pagedBooks: PagedList<Book> = {} as PagedList<Book>;

  getBooks(query: any = null) {
    this.bookService.getBooks(query)
      .subscribe((data) => {
        this.pagedBooks = data;
      });
  }
}
