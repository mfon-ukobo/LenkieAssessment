import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { ApiConfig } from "projects/web-client/src/api.config";
import { PagedList } from "../interfaces/paged-list";
import { Customer } from "../interfaces/customer";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private baseUrl = `${ApiConfig.baseUrl}/customers`;

  constructor(private http: HttpClient) {

  }

  getCustomers(params: any = {}): Observable<PagedList<Customer>> {
    return this.http.get<any>(this.baseUrl);
  }
}
