﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categoría")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}
