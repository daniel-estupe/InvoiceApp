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
    }
}