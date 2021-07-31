using System.Collections.ObjectModel;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Core;
using Api.Data;
using Api.Models;
using Api.Resources;

namespace Api.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly InvoiceContext context;
        private readonly IInvoiceRepository repository;

        public InvoiceService(InvoiceContext context, IInvoiceRepository repository)
        {
            this.context = context;
            this.repository = repository;
        }

        public async Task<InvoiceSummaryResource> create(NewInvoiceResource newInvoice)
        {
            var details = new Collection<InvoiceDetail>();
            foreach (var item in newInvoice.Detail)
            {
                var product = await context.Products.FindAsync(item.ProductId);
                details.Add(new InvoiceDetail(product, item.Amount));
            }

            var newCorrelative = (await repository.getCurrentCorrelative()) + 1;
            Invoice invoice = new Invoice()
            {
                Correlative = newCorrelative,
                CreatedAt = newInvoice.CreatedAt,
                CustomerId = newInvoice.CustomerId,
                Details = details
            };
            await repository.create(invoice);
            return await repository.getSummary(invoice.Id);
        }

        public async Task<InvoiceResource> getById(int id)
        {
            return await repository.getById(id);
        }

        public async Task<ICollection<InvoiceSummaryResource>> getSummary()
        {
            return await repository.getAllSummarized();
        }
    }
}