using System;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Shared.Entities
{
    public class Country
    {

        public int Id { get; set; }


        [Display(Name ="Pais")]
        [Required(ErrorMessage = "El Campo {0} Es Obligatorio ")]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
