using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace Api.Resources
{
    public class AddInvoiceResource
    {
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public ICollection<AddInvoiceDetailResource> Detail { get; set; }

        public AddInvoiceResource()
        {
            Detail = new Collection<AddInvoiceDetailResource>();
        }
    }
}