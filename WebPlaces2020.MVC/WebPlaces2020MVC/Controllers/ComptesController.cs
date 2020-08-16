using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPlaces2020.CLI.Models;

namespace WebPlaces2020.CLI.Controllers
{
    public class ComptesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllAccounts()
        {

            var lstAccounts = new List<Compte>();
            return View(lstAccounts);

        }

    }
}
