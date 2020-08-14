using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPlaces2020.Models;

namespace WebPlaces2020.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacesController : ControllerBase
    {
        private readonly PlaceContext _context;

        public PlacesController(PlaceContext context)
        {
            _context = context;
        }

        // GET: api/Places
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetPlace()
        {
            return await _context.Place.ToListAsync();
        }

        // GET: api/Places/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> GetPlace(long id)
        {
            var place = await _context.Place.FindAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            return place;
        }

        // PUT: api/Places/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlace(long id, Place place)
        {
            if (id != place.Id)
            {
                return BadRequest();
            }

            _context.Entry(place).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Places
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Place>> PostPlace(Place place)
        {
            _context.Place.Add(place);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlace", new { id = place.Id }, place);
        }

        // DELETE: api/Places/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Place>> DeletePlace(long id)
        {
            var place = await _context.Place.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }

            _context.Place.Remove(place);
            await _context.SaveChangesAsync();

            return place;
        }

        private bool PlaceExists(long id)
        {
            return _context.Place.Any(e => e.Id == id);
        }
    }
}
