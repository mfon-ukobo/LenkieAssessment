import { Component, Input } from '@angular/core';
import { BookStatus } from 'projects/core/src/lib/enums/book-status';
import { Book } from 'projects/core/src/lib/interfaces/book';
import { ReservationService } from 'projects/core/src/lib/services/reservation.service';
import { NotificationService } from '../../../services/notification.service';

@Component({
  selector: 'app-book-card',
  templateUrl: './book-card.component.html',
  styleUrls: ['./book-card.component.scss']
})
export class BookCardComponent {
  @Input() book!: Book;
  bookStatus = BookStatus;

  constructor(private reservationService: ReservationService, private notificationService: NotificationService) {

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
