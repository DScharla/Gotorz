using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GodTur.Models;
using GodTur.Models.Context;

namespace GodTur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPackageController : ControllerBase
    {
        private readonly OfferResponseContext _context;

        public TravelPackageController(OfferResponseContext context)
        {
            _context = context;
        }

        // GET: api/TravelPackage
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelPackage>>> GetTravelPackages()
        {
            return await _context.TravelPackages.ToListAsync();
        }

        // GET: api/TravelPackage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelPackage>> GetTravelPackage(int id)
        {
            var travelPackage = await _context.TravelPackages.FindAsync(id);

            if (travelPackage == null)
            {
                return NotFound();
            }

            return travelPackage;
        }

        // PUT: api/TravelPackage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTravelPackage(int id, TravelPackage travelPackage)
        {
            if (id != travelPackage.TravelPackageId)
            {
                return BadRequest();
            }

            _context.Entry(travelPackage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelPackageExists(id))
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

        // POST: api/TravelPackage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TravelPackage>> PostTravelPackage(TravelPackage travelPackage)
        {
            _context.TravelPackages.Add(travelPackage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTravelPackage", new { id = travelPackage.TravelPackageId }, travelPackage);
        }

        // DELETE: api/TravelPackage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTravelPackage(int id)
        {
            var travelPackage = await _context.TravelPackages.FindAsync(id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            _context.TravelPackages.Remove(travelPackage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TravelPackageExists(int id)
        {
            return _context.TravelPackages.Any(e => e.TravelPackageId == id);
        }
    }
}
