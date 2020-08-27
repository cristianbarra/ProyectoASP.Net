using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Usuarios.Areas.Usuario.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> _userManager;
        private static InputModel _input = null;
        private SignInManager<IdentityUser> _signInManager;
        public RegisterModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public void OnGet()
        {
           if(_input != null)
            {
                Input = _input;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
        
            if(await RegisterUserAsync())
            {
                return Redirect("/Principal/Principal?area=Principal");
            }
            else
            {
                return Redirect("/Usuario/Register");
            }
            //else
            //{
            //    ModelState.AddModelError("Input.Email", "Se ha generado un error en el servidor");
            //}
            //var data = Input;
            //return Page();
        }

        private async Task<bool> RegisterUserAsync()
        {
            var run = false;
            if (ModelState.IsValid)
            {
                var userList = _userManager.Users.Where(u => u.Email.Equals(Input.Email)).ToList();
                if (userList.Count.Equals(0))
                {
                    var user = new IdentityUser
                    {
                        UserName = Input.Email,
                        Email = Input.Email
                    };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        run = true;
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            Input = new InputModel
                            {
                                ErrorMassage = item.Description,
                                Email = Input.Email,
                        };
                        }
                        _input = Input;
                        run = false;
                    }
                }
                else
                {
                    Input = new InputModel
                    {
                        ErrorMassage = $"El {Input.Email} ya esta registrado",
                        Email = Input.Email,
                    };
                    _input = Input;
                    run = false;
                }
            }
            return run;
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
