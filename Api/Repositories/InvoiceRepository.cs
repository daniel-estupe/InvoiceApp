using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Api.Models;
using Api.Data;
using Api.Core;
using Api.Resources;
using System.Linq;

namespace Api.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceContext _context;

        public InvoiceRepository(InvoiceContext context)
        {
            _context = context;
        }

        public async Task create(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();
            // return await getSummary(invoice.Id);
        }

        public async Task<Invoice> deleteById(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null) return default;
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }

        public async Task<ICollection<InvoiceSummaryResource>> getAllSummarized() {
            return await _context.Invoices
                .Include(i => i.Details)
                .Include(i => i.Customer)
                .Select(i => new InvoiceSummaryResource 
                {
                    Id = i.Id,
                    Correlative = i.Correlative,
                    CreatedAt = i.CreatedAt,
                    BillingNo = i.Customer.BillingNo,
                    Customer = i.Customer.Name,
                    Total = i.Details.Sum(d => d.Subtotal)
                })
                .ToListAsync();
        }

        public async Task<InvoiceResource> getById(int id)
        {
            return await _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Details)
                    .ThenInclude(d => d.Product)
                .Where(i => i.Id == id)
                .Select(invoice => new InvoiceResource
                {
                    Id = invoice.Id,
                    Correlative = invoice.Correlative,
                    CreatedAt = invoice.CreatedAt,
                    Customer = invoice.Customer,
                    Details = invoice.Details
                        .Select(detail => new InvoiceDetailResource
                        {
                            Id = detail.Id,
                            Amount = detail.Amount,
                            Subtotal = detail.Subtotal,
                            Product = detail.Product
                        })
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> getCurrentCorrelative()
        {
            var invoice = await _context.Invoices
                .OrderByDescending(i => i.Id).FirstOrDefaultAsync();
            
            if (invoice == default)
            {
                return 0;
            }
            else 
            {
                return invoice.Correlative;
            }
        }

        public async Task<ICollection<InvoiceDetail>> getDetails(int id)
        {
            return await _context.InvoiceDetails
                .Where(detail => detail.InvoiceId == id)
                .ToListAsync();
        }

        public async Task<InvoiceSummaryResource> getSummary(int invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.Details)
                .Include(i => i.Customer)
                .Select(i => new InvoiceSummaryResource 
                {
                    Id = i.Id,
                    Correlative = i.Correlative,
                    CreatedAt = i.CreatedAt,
                    BillingNo = i.Customer.BillingNo,
                    Customer = i.Customer.Name,
                    Total = i.Details.Sum(d => d.Subtotal)
                })
                .Where(invoice => invoice.Id == invoiceId)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> invoiceExists(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            return invoice != null;
        }

        public async Task<Invoice> update(int id, AddInvoiceResource invoiceEdited)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null) return null;

            invoice.CreatedAt = invoiceEdited.CreatedAt;
            invoice.CustomerId = invoiceEdited.CustomerId;

            var details = await getDetails(id);

            // eliminar el detalle que no se encuentre la lista actualizada
            foreach (var detail in details)
            {
                var detailSelected = invoiceEdited.Detail.Where(d => d.Id == detail.Id).FirstOrDefault();
                if (detailSelected == default) {
                    _context.InvoiceDetails.Remove(detail);
                }
            }

            // actualizar los detalles que s?? est??n en la lista
            foreach (var detail in invoiceEdited.Detail)
            {
                var detailSelected = details.Where(d => d.Id == detail.Id).FirstOrDefault();
                if (detailSelected != default) {
                    detailSelected.Subtotal = detail.Amount * detailSelected.UnitPrice;
                    detailSelected.Amount = detail.Amount;
                }
            }
            
            // agregar los nuevos detalles
            foreach (var item in invoiceEdited.Detail)
            {
                if (item.Id == default) {
                    var product = await _context.Products.FindAsync(item.ProductId);
                    invoice.Details.Add(new InvoiceDetail(product, item.Amount));
                }
            }
            
            await _context.SaveChangesAsync();
            return invoice;
        }
    }
}