import { Component, Input } from '@angular/core';
import { BookStatus } from 'projects/core/src/lib/enums/book-status';
import { Book } from 'projects/core/src/lib/interfaces/book';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent {
  @Input() book!: Book;
  bookStatus = BookStatus;
}
