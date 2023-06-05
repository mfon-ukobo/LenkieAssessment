import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { BookService } from 'projects/core/src/lib/services/book.service';
import { NotificationService } from '../../../services/notification.service';
import { BookStatus } from 'projects/core/src/lib/enums/book-status';
import { ReservationService } from 'projects/core/src/lib/services/reservation.service';

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

  constructor(private route: ActivatedRoute, private reservationService: ReservationService, private notificationService: NotificationService) {}

  ngOnInit() {
    this.route.data.subscribe((data) => {
      this.book = data['book'];
    });
  }

  reserve() {
    this.reservationService.createReservation({
      bookId: this.book.id
    }).subscribe(data => {
      this.book.status = this.bookStatus.reserved;
      this.notificationService.success(`Reservation for ${this.book.title} created`);
    });
  }
}
