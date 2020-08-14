using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPlaces2020.Client.Context;
using WebPlaces2020.Client.Models;

namespace WebPlaces2020.Client.Controllers
{
    public class Places2Controller : Controller
    {
        private readonly PlaceContext _context;

        public Places2Controller(PlaceContext context)
        {
            _context = context;
        }

        // GET: Places2
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlaceItems.ToListAsync());
        }

        // GET: Places2/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.PlaceItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Places2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Places2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom_PLA,Vat_PLA,EmailPro_PLA,FreeText_PLA,Logo_PLA,AddrPostalC_PLA,AddrCity_PLA,AddrCountry_PLA,AddrStreet_PLA,AddrPostBox_PLA,Phone_PLA,EmailPlace_PLA,Site_PLA,Instagram_PLA,Facebook_PLA,Linkedin_PLA,Hours_PLA,Picture_PLA")] Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Places2/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.PlaceItems.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            return View(place);
        }

        // POST: Places2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nom_PLA,Vat_PLA,EmailPro_PLA,FreeText_PLA,Logo_PLA,AddrPostalC_PLA,AddrCity_PLA,AddrCountry_PLA,AddrStreet_PLA,AddrPostBox_PLA,Phone_PLA,EmailPlace_PLA,Site_PLA,Instagram_PLA,Facebook_PLA,Linkedin_PLA,Hours_PLA,Picture_PLA")] Place place)
        {
            if (id != place.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(place);
        }

        // GET: Places2/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.PlaceItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var place = await _context.PlaceItems.FindAsync(id);
            _context.PlaceItems.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(long id)
        {
            return _context.PlaceItems.Any(e => e.Id == id);
        }
    }
}
