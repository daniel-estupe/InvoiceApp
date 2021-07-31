using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Resources;

namespace Api.Core
{
    public interface IInvoiceService
    {
        Task<InvoiceResource> getById(int id);
         Task<ICollection<InvoiceSummaryResource>> getSummary();
         Task<InvoiceSummaryResource> create(NewInvoiceResource newInvoice);
    }
}