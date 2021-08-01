import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

import { Customer } from 'src/app/core/models/customer.model';
import { Invoice } from 'src/app/core/models/invoice.model';
import { InvoiceDetail } from 'src/app/core/models/invoiceDetail.model';
import { NewInvoice } from 'src/app/core/models/newInvoice.model';
import { NewInvoiceDetail } from 'src/app/core/models/newInvoiceDetail.model';
import { Product } from 'src/app/core/models/product.model';

import { CustomerService } from 'src/app/core/services/customer.service';
import { InvoiceService } from 'src/app/core/services/invoice.service';
import { ProductService } from 'src/app/core/services/product.service';

@Component({
  selector: 'app-edit-invoice',
  templateUrl: './edit-invoice.component.html',
  styleUrls: ['./edit-invoice.component.scss'],
})
export class EditInvoiceComponent implements OnInit {
  invoice?: Invoice;
  products: Product[] = [];
  invoiceForm: FormGroup;
  invoiceDetailForm: FormGroup;
  selectedCustomer?: Customer;
  selectedProduct?: Product;
  selectedDetail: InvoiceDetail[] = [];
  invoiceDetailToEdit?: InvoiceDetail;
  saveLoading = false;
  editEnabled = false;

  constructor(
    private formBuilder: FormBuilder,
    private activatedRoute: ActivatedRoute,
    private customerService: CustomerService,
    private invoiceService: InvoiceService,
    private productService: ProductService,
    private router: Router
  ) {
    this.invoiceForm = this.createInvoiceForm();
    this.invoiceDetailForm = this.createInvoiceDetailForm();

    const id: string = this.activatedRoute.snapshot.params['id'];
    this.invoiceService.getById(id).subscribe((res) => {
      this.invoice = res;
      this.invoiceForm.patchValue({
        createdAt: res.createdAt,
        customer: `${res.customer.name} - ${res.customer.billingNo}`,
      });
      this.selectedDetail = res.details;
      this.selectedCustomer = res.customer;
    });
  }

  ngOnInit() {
    this.getProducts();
  }

  get subtotal() {
    const unitPrice: number = this.invoiceDetailForm.get('unitPrice')?.value;
    const amount: number = this.invoiceDetailForm.get('amount')?.value;
    if (!unitPrice || !amount) return 0;
    return unitPrice * amount;
  }

  onEditHandler(item: InvoiceDetail) {
    this.editEnabled = true;
    this.invoiceDetailToEdit = item;
    this.selectedProduct = item.product;
    this.invoiceDetailForm.patchValue({
      productId: item.product?.id,
      description: item.product?.description,
      unitPrice: item.unitPrice,
      amount: item.amount,
    });
  }

  onDeleteHandler(item: InvoiceDetail) {
    const details = [...this.selectedDetail];
    const index = details.indexOf(item);
    details.splice(index, 1);
    this.selectedDetail = [...details];
  }

  onSaveInvoice() {
    if (!this.validateOnSave()) {
      Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'El formulario no es válido!',
        confirmButtonColor: '#6c757d'
      });
      return;
    }
    const createdAt: Date = this.invoiceForm.get('createdAt')?.value;
    const customerId: number = this.selectedCustomer?.id!;

    const newDetail: NewInvoiceDetail[] = this.selectedDetail.map(
      ({ id, amount, product }: InvoiceDetail) => {
        const detail: NewInvoiceDetail = {
          id,
          amount,
          productId: product?.id!,
        };
        return detail;
      }
    );
    const invoice: NewInvoice = {
      createdAt,
      customerId,
      detail: newDetail,
    };

    this.saveLoading = true;
    this.invoiceService.update(this.invoice?.id!, invoice).subscribe(() => {
      this.saveLoading = false;
      Swal.fire('Hecho!', 'La factura se ha actualizado.', 'success');
      this.router.navigate(['invoices', this.invoice?.id]);
    }, () => {
      this.saveLoading = false;
    });
  }

  onSearchCustomer() {
    const { customer } = this.invoiceForm.value;
    this.customerService.search(customer).subscribe((res) => {
      if (res.length === 1) {
        this.assignCustomer(res[0]);
      } else {
        this.selectedCustomer = undefined;
      }
    });
  }

  onAddDetail() {
    if (!this.validateDetail()) return;
    const unitPrice: number = this.invoiceDetailForm.get('unitPrice')?.value;
    const amount: number = this.invoiceDetailForm.get('amount')?.value;
    const id = this.invoiceDetailToEdit?.id;

    const invoiceDetail: InvoiceDetail = {
      id,
      amount: amount,
      unitPrice: unitPrice,
      subtotal: amount * unitPrice,
      product: this.selectedProduct,
    };

    if (this.editEnabled) {
      const detail = [...this.selectedDetail];
      const index = detail.indexOf(this.invoiceDetailToEdit!);
      detail[index] = invoiceDetail;
      this.selectedDetail = [...detail];
    } else {
      this.selectedDetail = [...this.selectedDetail, invoiceDetail];
    }

    this.clearStateOnEdit();
  }

  onCancelEdition() {
    this.clearStateOnEdit();
  }

  onSelectProduct() {
    const productId: number = this.invoiceDetailForm.get('productId')?.value;
    this.selectedProduct = this.products.find((p) => p.id === productId);
    this.invoiceDetailForm.patchValue({
      description: this.selectedProduct?.description,
      unitPrice: this.selectedProduct?.unitPrice,
    });
  }

  private clearStateOnEdit() {
    this.invoiceDetailToEdit = undefined;
    this.editEnabled = false;
    this.invoiceDetailForm = this.createInvoiceDetailForm();
  }

  private assignCustomer(customer: Customer) {
    this.selectedCustomer = customer;
    this.invoiceForm
      .get('customer')
      ?.setValue(`${customer.name} - ${customer.billingNo}`);
  }

  private validateOnSave(): boolean {
    if (this.selectedDetail.length === 0) return false;
    if (!this.selectedCustomer) return false;
    if (this.invoiceForm.invalid) return false;
    return true;
  }

  private validateDetail(): boolean {
    if (this.selectedProduct === null) return false;
    return true;
  }

  private createInvoiceForm() {
    return this.formBuilder.group({
      createdAt: [new Date(), Validators.required],
      customer: [''],
    });
  }

  private createInvoiceDetailForm() {
    return this.formBuilder.group({
      productId: [''],
      description: [{ value: '', disabled: true }],
      unitPrice: [{ value: '', disabled: true }],
      amount: [1],
    });
  }

  private getProducts() {
    this.productService.get().subscribe((res) => {
      this.products = res;
    });
  }
}
