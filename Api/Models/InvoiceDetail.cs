using Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class InvoiceDetail
    {
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [GreaterThanZero]
        public int Amount { get; set; }

        [Required]
        public float Subtotal { get; set; }

        public Invoice Invoice { get; set; }
        public Product Product { get; set; }

        public InvoiceDetail()
        {
        }

        public InvoiceDetail(Product product, int amount)
        {
            this.Amount = amount;
            this.Product = product;
            this.Subtotal = product.UnitPrice * amount;
        }
    }
}