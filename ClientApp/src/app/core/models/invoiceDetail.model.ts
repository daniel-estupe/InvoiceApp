import { Product } from './product.model';

export interface InvoiceDetail {
  id?: number;
  amount: number;
  unitPrice: number;
  subtotal: number;
  product?: Product;
}
