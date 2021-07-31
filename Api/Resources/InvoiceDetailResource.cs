using Api.Models;

namespace Api.Resources
{
    public class InvoiceDetailResource
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public float UnitPrice => (Subtotal==0) ? 0 : Subtotal / Amount;
        public float Subtotal { get; set; }
        public Product Product { get; set; }
    }
}