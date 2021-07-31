import { Component, OnInit } from '@angular/core';
import { InvoiceSummary } from 'src/app/core/models/invoiceSummary.model';
import { InvoiceService } from 'src/app/core/services/invoice.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnInit {
  displayedColumns: string[] = [
    'correlative',
    'createdAt',
    'billingNo',
    'customer',
    'total',
    'options',
  ];
  dataSource: InvoiceSummary[] = [];

  constructor(private invoiceService: InvoiceService) {}

  ngOnInit() {
    this.invoiceService.getSummary().subscribe((res) => {
      this.dataSource = res;
    });
  }
}
