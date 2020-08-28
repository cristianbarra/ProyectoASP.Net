using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Usuarios.Models;
using Usuarios.Areas.Usuario.Models;
using Usuarios.Librery;
using Usuarios.Areas.Principal.Controllers;

namespace Usuarios.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //IServiceProvider _serviceProvider;

        private static LoginModel _model = null;
        private SignInManager<IdentityUser> _signInManager;
        private LUsuario _usuario;

        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IServiceProvider serviceProvider)
        {
            //_logger = logger;
            //_serviceProvider = serviceProvider;
            _usuario = new LUsuario(userManager,signInManager,roleManager);
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            //throw new Exception("This is some exception!!!");
            //await CreateRolesAsync(_serviceProvider);
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction(nameof(PrincipalController.Principal), "Principal");
            if (_model == null)
                return View();
            else
                return View(_model);
        }
        [HttpPost]
        public async Task<IActionResult> IndexAsync(LoginModel model)
        {
            //throw new Exception("This is some exception!!!");
            //await CreateRolesAsync(_serviceProvider);
          
            if(ModelState.IsValid)
            {
                var result = await _usuario.UserLogingAsync(model);
                if (result.Succeeded)
                    return Redirect("/Principal/Principal");
                else
                {
                    model.ErrorMassage = "Correo o contraseña invalidos.";
                    _model = model;
                    return Redirect("/");
                }
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            ErrorViewModel error = null;
            if (statusCode != null)
            {
                error = new ErrorViewModel
                {
                    RequestId = Convert.ToString(statusCode),
                    ErrorMensaje = "Se produjo un error al procesar su solicitud",
                };
            }
            else
            {
                var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                if(exceptionFeature != null)
                {
                    error = new ErrorViewModel
                    {
                        RequestId = "500",
                        ErrorMensaje = exceptionFeature.Error.Message,
                    };
                }
            }
            return View(error);
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            String[] rolesName = { "Admin", "User" };
            foreach(var item in rolesName)
            {
                var roleExist = await roleManager.RoleExistsAsync(item);
                if (!roleExist)
                    await roleManager.CreateAsync(new IdentityRole(item));
            }
        }
    }
}
