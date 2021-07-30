using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Api.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]
        public int Correlative { get; set; }

        [Required]
        [ColumnAttribute(TypeName = "Date")]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer {get; set; }
        public ICollection<InvoiceDetail> Details { get; set; }
    }
}