using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Api.Core;
using Api.Resources;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceService service;

        public InvoicesController(IInvoiceService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<InvoiceSummaryResource>> Get() 
        {
            return await service.getSummary();
        }

        [HttpPost]
        public async Task<ActionResult<Invoice>> Create(NewInvoiceResource newInvoice) 
        {
            return await service.create(newInvoice);
        }
    }
}