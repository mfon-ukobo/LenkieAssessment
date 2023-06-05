import { Component } from '@angular/core';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { PagedList } from 'projects/core/src/lib/interfaces/paged-list';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { map } from 'rxjs';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
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
