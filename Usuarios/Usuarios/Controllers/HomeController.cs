﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Usuarios.Models;
using Usuarios.Areas.Usuario.Models;

namespace Usuarios.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        //IServiceProvider _serviceProvider;

        private static LoginModel _model;

        public HomeController(IServiceProvider serviceProvider)
        {
            //_logger = logger;
            //_serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            //throw new Exception("This is some exception!!!");
            //await CreateRolesAsync(_serviceProvider);
            return View();
        }
        public IActionResult Index(LoginModel model)
        {
            //throw new Exception("This is some exception!!!");
            //await CreateRolesAsync(_serviceProvider);
            return View();
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
