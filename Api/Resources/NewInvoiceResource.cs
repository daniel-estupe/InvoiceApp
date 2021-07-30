using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;

namespace Api.Resources
{
    public class NewInvoiceResource
    {
        public DateTime CreatedAt { get; set; }
        public int CustomerId { get; set; }
        public ICollection<NewInvoiceDetailResource> Detail { get; set; }

        public NewInvoiceResource()
        {
            Detail = new Collection<NewInvoiceDetailResource>();
        }
    }
}