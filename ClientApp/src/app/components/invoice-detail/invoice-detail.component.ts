import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { InvoiceDetail } from 'src/app/core/models/invoiceDetail.model';

@Component({
  selector: 'app-invoice-detail',
  templateUrl: './invoice-detail.component.html',
  styleUrls: ['./invoice-detail.component.scss'],
})
export class InvoiceDetailComponent {
  @Input('detail') dataSource: InvoiceDetail[] = [];
  displayedColumns = ['product', 'unitPrice', 'amount', 'subtotal'];

}
