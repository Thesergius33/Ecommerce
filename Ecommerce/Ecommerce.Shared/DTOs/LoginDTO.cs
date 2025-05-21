using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "El campo {0} Es Requerido")]
        [EmailAddress (ErrorMessage = "Debe ingresar un Email valido")]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} Es Requerido")]
        [MinLength(6, ErrorMessage = "El campo{0} debe tener al menos {6} Caracteres")]
        public string Password { get; set; }

    }
}
