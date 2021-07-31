import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Customer } from '../models/customer.model';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}

  search(filter: string) {
    const params = new HttpParams().set('q', filter);
    return this.http.get<Customer[]>(`${environment.apiUrl}/customers`, {
      params,
    });
  }
}
