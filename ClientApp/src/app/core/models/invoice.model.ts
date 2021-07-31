import { Customer } from './customer.model';
import { InvoiceDetail } from './invoiceDetail.model';

export interface Invoice {
  id: number;
  correlative: number;
  createdAt: Date;
  customer: Customer;
  details: InvoiceDetail[];
}
