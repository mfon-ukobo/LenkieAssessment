import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiConfig } from 'projects/web-client/src/api.config';
import { Observable } from 'rxjs';
import { PagedList } from '../interfaces/paged-list';
import { Author } from '../interfaces/author';

@Injectable({
  providedIn: 'root',
})
export class AuthorService {
  private baseUrl = `${ApiConfig.baseUrl}/authors`;

  constructor(private http: HttpClient) {}

  getAuthors(): Observable<PagedList<Author>> {
    return this.http.get<any>(this.baseUrl);
  }
}
