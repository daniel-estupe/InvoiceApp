import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { InvoiceComponent } from './pages/invoice/invoice.component';
import { NewInvoiceComponent } from './pages/new-invoice/new-invoice.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/invoices',
    pathMatch: 'full',
  },
  {
    path: 'invoices',
    component: HomeComponent,
  },
  {
    path: 'invoices/:id',
    component: InvoiceComponent,
  },
  {
    path: 'new-invoice',
    component: NewInvoiceComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
