import { BookStatus } from "../enums/book-status";
import { Author } from "./author";

export interface Book {
  title: string;
  description: string;
  imageUrl: string;
  author: Author;
  status: BookStatus;
}
