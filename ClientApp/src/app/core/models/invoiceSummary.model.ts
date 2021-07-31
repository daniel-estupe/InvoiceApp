export interface InvoiceSummary {
  id: number;
  correlative: number;
  createdAt: Date;
  billingNo: string;
  customer: string;
  total: number;
}
