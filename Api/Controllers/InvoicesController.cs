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

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceResource>> GetById(int id) 
        {
            var invoice = await service.getById(id);
            if (invoice == default) 
                return NotFound();
            return invoice;
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceSummaryResource>> Create(NewInvoiceResource newInvoice) 
        {
            var invoice = await service.create(newInvoice);
            return CreatedAtAction("GetById", new {id = invoice.Id}, invoice);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var invoice = await service.deleteById(id);
            if (invoice == default) return NotFound();
            return Ok();
        }
    }
}