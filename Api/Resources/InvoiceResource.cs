using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.Resources
{
    public class InvoiceResource
    {
        public int Id { get; set; }
        public int Correlative { get; set; }
        public DateTime CreatedAt { get; set; }
        public Customer Customer {get; set; }
        
        public IEnumerable<InvoiceDetailResource> Details { get; set; }

        public InvoiceResource()
        {
            Details = new List<InvoiceDetailResource>();
        }
    }
}