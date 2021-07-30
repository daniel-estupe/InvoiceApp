using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Api.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(8, ErrorMessage = "Ingrese un NIT válido.")]
        public string BillingNo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre del cliente no debe exceder de 100 caracteres.")]
        public string Name { get; set; }
    }
}