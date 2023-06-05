import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PagedList } from '../interfaces/paged-list';
import { Book } from '../interfaces/book';
import { Observable } from 'rxjs';
import { ApiConfig } from 'projects/web-client/src/api.config';

@Injectable({
  providedIn: 'root',
})
export class BookService {

  private baseUrl = `${ApiConfig.baseUrl}/books`;

  constructor(private http: HttpClient) {}

  getBooks(params: any): Observable<PagedList<Book>> {
    return this.http.get<any>(this.baseUrl);
  }

  getBook(id: number): Observable<Book> {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  createBook(book: any): Observable<Book> {
    return this.http.post<any>(`${this.baseUrl}`, book);
  }

  updateBook(id: number, book: any): Observable<void> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, book);
  }

  checkOut(model: any): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/check-out`, model);
  }

  checkIn(model: any): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/check-in`, model);
  }
}
