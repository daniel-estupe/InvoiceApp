import { NewInvoiceDetail } from './newInvoiceDetail.model';

export interface NewInvoice {
  createdAt: Date;
  customerId: number;
  detail: NewInvoiceDetail[];
}
