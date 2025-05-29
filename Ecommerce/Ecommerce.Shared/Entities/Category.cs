using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        // Relación con ProductCategory
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        // Propiedad de navegación inversa para Products
        public ICollection<Product> Products { get; set; } // ← Esta es la propiedad faltante
    }
}