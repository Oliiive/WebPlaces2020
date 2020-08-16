using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebPlaces2020.CLI.Controllers
{
    public class IdtAccountController : Controller
    {
     
        public IdtAccountController()
        {


        }
        
        public IActionResult Subscription()
        {
            return View();
        }
    }
}
