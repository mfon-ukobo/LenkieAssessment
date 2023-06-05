import { Component } from '@angular/core';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-check-out',
  templateUrl: './check-out.component.html',
  styleUrls: ['./check-out.component.scss']
})
export class CheckOutComponent {
  book!: Book;
  today = new Date();
  returnDate: any;

  constructor(private route: ActivatedRoute, private bookService: BookService, private notificationService: NotificationService) {

  }

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.book = data['book'];
    });
  }

  checkOut() {
    this.bookService.checkOut({
      bookId: this.book.id,
      checkInDate: this.returnDate
    }).subscribe(() => {
      this.notificationService.success("Book Checked Out")
    });
  }
}
