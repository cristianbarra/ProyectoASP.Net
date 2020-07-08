using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Usuarios.Areas.Usuario.Pages.Account
{
    public class RegisterModel : PageModel
    { 
        [BindProperty]
        public InputModel Input { get; set; }
        public void OnGet()
        {
           
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                ModelState.AddModelError("Input.Email", "Se ha generado un error en el servidor");
            }
            var data = Input;
            return Page();
        }
        
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

            //Propiedad para confirmar la contraseña
            [Required(ErrorMessage = "El campo confirm password es obligatorio")]
            [DataType(DataType.Password)]
            [Compare("Password",ErrorMessage = "The paswword and confirmation password do not match.")]
            public string ConfirmPasword {get; set;}
            //[Required]
            public string ErrorMassage { get; set; }
        }
    }
}
