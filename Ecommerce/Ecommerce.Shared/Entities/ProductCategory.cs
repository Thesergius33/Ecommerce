using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        [Display(Name = "Categoría de Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        // Propiedades de navegación
        public ICollection<Category> Categories { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

        // Propiedades calculadas
        [Display(Name = "Total Categorías")]
        public int CategoriesNumber => Categories?.Count ?? 0;

        [Display(Name = "Total Productos")]
        public int ProductsNumber => Products?.Count ?? 0;
    }
}