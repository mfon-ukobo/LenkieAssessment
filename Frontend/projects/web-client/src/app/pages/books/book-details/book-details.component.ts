import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { NotificationService } from '../../../services/notification.service';
import { BookStatus } from 'projects/core/src/lib/enums/book-status';

@Component({
  selector: 'app-book-details',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.scss'],
})
export class BookDetailsComponent {
  book!: Book;
  today = new Date();
  returnDate: any;
  bookStatus = BookStatus;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.book = data['book'];
    });
  }

  reserve() {

  }
}
