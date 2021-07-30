using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Ingrese un código válido.")]
        public string Code { get; set; }

        [Required]
        [StringLength(225, ErrorMessage = "La descripción del producto no debe exceder de 100 caracteres.")]
        public string Description { get; set; }

        [Required]
        public float UnitPrice { get; set; }
    }
}