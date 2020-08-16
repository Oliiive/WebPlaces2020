using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPlaces2020.CLI.Context;

namespace WebPlaces2020.CLI.Controllers
{
    public class PlacesController : Controller
    {

        private readonly PlaceContext _context;
        public IActionResult Index()
        {
            return View();
        }

        public PlacesController (PlaceContext context)
        {

            _context = context;

        }

        public IActionResult GetAllPlaces()
        {

            var lstPlaces = _context.PlaceItems.ToList();
            return View(lstPlaces);

        }
    }
}
