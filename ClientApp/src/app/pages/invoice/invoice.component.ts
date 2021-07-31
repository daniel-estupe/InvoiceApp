import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2'

import { Invoice } from 'src/app/core/models/invoice.model';
import { InvoiceService } from 'src/app/core/services/invoice.service';

@Component({
  selector: 'app-invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent {
  invoice?: Invoice;
  loading = false;

  constructor(private route: ActivatedRoute, private router: Router, private invoiceService: InvoiceService) {
    const invoiceId: string = this.route.snapshot.params['id'];
    this.loading = true;
    this.invoiceService.getById(invoiceId).subscribe((res) => {
      this.loading = false;
      this.invoice = res;
    }, (error: HttpErrorResponse) => {
      this.loading = false;
    })
  }

  onDelete() {
    Swal.fire({
      title: 'Confirme para eliminar la factura',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#dc3545',
      cancelButtonColor: '#6c757d',
      confirmButtonText: 'Confirmo',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        this.deleteInvoice();
      }
    })
  }

  deleteInvoice() {
    this.invoiceService.deleteById(this.invoice?.id!).subscribe(() => {
      Swal.fire(
        'Hecho!',
        'La factura se ha eliminado.',
        'success'
      )
      this.router.navigate(['/']);
    })
  }
}
