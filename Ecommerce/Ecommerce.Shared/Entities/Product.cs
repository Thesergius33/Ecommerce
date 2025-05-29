using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Shared.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Producto")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }  // Cambiado de double a decimal para valores monetarios

        // Relación con ProductCategory
        public ProductCategory ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        // Relación con ProductImage
        public ProductImage ProductImage { get; set; }
        public int ProductImageId { get; set; }

        // Relación CORREGIDA con Category
        public Category Category { get; set; }  // Cambiado de object a Category
        public int CategoryId { get; set; }     // Cambiado de object a int
    }
}