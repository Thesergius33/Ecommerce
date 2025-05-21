using Ecommerce.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Shared.DTOs
{
    public class UserDTO : User
    {
        [DataType(DataType.Password)]
        [Display(Name ="Contraseña")]
        [Required(ErrorMessage ="El campo {0} Es Requerido")]
        [StringLength(20,MinimumLength =6, ErrorMessage ="El campo{0} debe tener {2} y {1} Caracteres")]

        public string Password { get; set; } = null!;

        [Compare("Password", ErrorMessage ="La contraseña de confirmacion debe ser igual")]
        [Display(Name = "Confirmacion contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo {0} Es Requerido")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "El campo{0} debe tener {2} y {1} Caracteres")]

        public string PasswordConfirm { get; set; } = null!;

    }
}                                    
