using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Usuarios.Areas.Usuario.Models
{
    public class LoginModel
    {
        [BindProperty]
        public InputModel Input { get; set; }
        [TempData]
        public string ErrorMassage { get; set; }

        public class InputModel
        {
            //Propiedad para poner el email
            [Required(ErrorMessage = "El campo email es obligatorio")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            //Propiedad para poner la contraseña
            [Required(ErrorMessage = "El campo password es obligatorio")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            [StringLength(100, ErrorMessage = "El numero de caracteres de {0} debe ser al menos {2}", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
