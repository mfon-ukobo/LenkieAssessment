import { Book } from "./book";

export interface Reservation {
  book: Book;
  reservationDate: Date;
  reservationEndDate: Date;
}
