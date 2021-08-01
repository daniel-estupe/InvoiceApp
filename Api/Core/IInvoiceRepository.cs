using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Resources;

namespace Api.Core
{
    public interface IInvoiceRepository
    {
        Task create(Invoice invoice);
        Task<bool> invoiceExists(int id);
        Task<Invoice> deleteById(int id);
        Task<ICollection<InvoiceSummaryResource>> getAllSummarized();
        Task<InvoiceResource> getById(int id);
        Task<int> getCurrentCorrelative();
        Task<ICollection<InvoiceDetail>> getDetails(int id);
        Task<Invoice> update(int id, AddInvoiceResource invoice);
        // Task removeDetail(InvoiceDetail detail);
        // Task updateDetail(InvoiceDetail detail);
        Task<InvoiceSummaryResource> getSummary(int invoiceId);
    }
}