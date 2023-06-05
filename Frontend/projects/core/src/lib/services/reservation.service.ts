import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Reservation } from '../interfaces/reservation';
import { ApiConfig } from 'projects/web-client/src/api.config';
import { PagedList } from '../interfaces/paged-list';

@Injectable({
  providedIn: 'root',
})
export class ReservationService {

  private baseUrl = `${ApiConfig.baseUrl}/reservations`;

  constructor(private http: HttpClient) {}

  createReservation(model: any): Observable<Reservation> {
    return this.http.post<any>(this.baseUrl, model);
  }

  getReservations(model: any): Observable<PagedList<Reservation>> {
    return this.http.get<any>(this.baseUrl);
  }
}
