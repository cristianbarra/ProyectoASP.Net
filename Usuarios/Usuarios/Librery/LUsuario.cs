using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Usuarios.Areas.Usuario.Models;

namespace Usuarios.Librery
{
    public class LUsuario : ListObject
    {
        public LUsuario(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        internal async Task<SignInResult> UserLogingAsync(LoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Input.Email,
                model.Input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {

            }
            return result;
        }
    }
}
