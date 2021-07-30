using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Resources;

namespace Api.Core
{
    public interface IInvoiceService
    {
         Task<ICollection<InvoiceSummaryResource>> getSummary();
         Task<Invoice> create(NewInvoiceResource newInvoice);
    }
}