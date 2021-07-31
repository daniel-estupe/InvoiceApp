using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Resources;

namespace Api.Core
{
    public interface IInvoiceRepository
    {
        Task create(Invoice invoice);
        Task<ICollection<InvoiceSummaryResource>> getAllSummarized();
        Task<InvoiceResource> getById(int id);
        Task<int> getCurrentCorrelative();
        Task<InvoiceSummaryResource> getSummary(int invoiceId);
    }
}