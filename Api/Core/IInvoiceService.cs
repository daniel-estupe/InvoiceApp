using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Resources;

namespace Api.Core
{
    public interface IInvoiceService
    {
        Task<Invoice> update(int id, AddInvoiceResource invoice);
        Task<InvoiceResource> getById(int id);
         Task<ICollection<InvoiceSummaryResource>> getSummary();
         Task<InvoiceSummaryResource> create(AddInvoiceResource newInvoice);
         Task<Invoice> deleteById(int id);
    }
}