import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';
import { Invoice } from '../models/invoice.model';
import { InvoiceSummary } from '../models/invoiceSummary.model';
import { NewInvoice } from '../models/newInvoice.model';

@Injectable({
  providedIn: 'root',
})
export class InvoiceService {
  constructor(private http: HttpClient) {}

  deleteById(id: number) {
    return this.http.delete(`${environment.apiUrl}/invoices/${id}`);
  }

  getById(id: string) {
    return this.http.get<Invoice>(`${environment.apiUrl}/invoices/${id}`);
  }

  getSummary() {
    return this.http.get<InvoiceSummary[]>(`${environment.apiUrl}/invoices`);
  }

  create(invoice: NewInvoice) {
    return this.http.post<InvoiceSummary>(`${environment.apiUrl}/invoices`, invoice);
  }

  update(id: number, invoice: NewInvoice) {
    return this.http.put(`${environment.apiUrl}/invoices/${id}`, invoice);
  }
}
