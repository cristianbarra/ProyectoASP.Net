using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Usuarios.Controllers
{
    //[Route("[controller]")]
    public class UsuariosController : Controller
    {
        //[HttpGet]
        //[Route("/Usuarios/Cristian")]
        //[HttpGet("[controller]/[action]/{data:int}")]

        public IActionResult Index(string data)
        {
            //var url = Url.Action("Metodo", "Usuarios",new { age=25,name="Cristian"});
            //return View("Index",data);
            var url = Url.RouteUrl("Cristian", new { age = 25, name = "Cristian" });
            return Redirect(url);
        }
        [HttpGet("[controller]/[action]",Name = "Cristian")]
        public IActionResult Metodo(int age,string name)
        {
            var data = $"Nombre {name} edad {age}";
            return View("Index",data);
        }
    }
}