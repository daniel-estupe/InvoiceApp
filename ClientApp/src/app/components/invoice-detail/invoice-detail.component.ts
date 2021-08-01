import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { InvoiceDetail } from 'src/app/core/models/invoiceDetail.model';

@Component({
  selector: 'app-invoice-detail',
  templateUrl: './invoice-detail.component.html',
  styleUrls: ['./invoice-detail.component.scss'],
})
export class InvoiceDetailComponent implements OnChanges {
  @Input('detail') dataSource: InvoiceDetail[] = [];
  @Input('editable') editable: boolean = false;
  @Output('onEdit') onEditEmitter = new EventEmitter<InvoiceDetail>();
  @Output('onDelete') onDeleteEmitter = new EventEmitter<InvoiceDetail>();

  displayedColumns = ['product', 'unitPrice', 'amount', 'subtotal'];

  ngOnChanges(changes: SimpleChanges) {
    if(changes.editable && changes.editable.currentValue) {
      this.displayedColumns = [...this.displayedColumns, 'options'];
    }
  }

  getColumnClass(columnName: string): string {
    return `column-${columnName}${(this.editable) ? '-editable' : ''}`;
  }

  onEdit(item: InvoiceDetail) {
    this.onEditEmitter.emit(item);
  }

  onDelete(item: InvoiceDetail) {
    this.onDeleteEmitter.emit(item);
  }
}
