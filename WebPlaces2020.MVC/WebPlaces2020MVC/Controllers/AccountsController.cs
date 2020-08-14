using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPlaces2020.Client.Models;

namespace WebPlaces2020.Client.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllAccounts()
        {

            var lstAccounts = new List<Account>();
            return View(lstAccounts);

        }

    }
}
