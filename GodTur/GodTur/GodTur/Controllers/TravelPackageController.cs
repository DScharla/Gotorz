using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GodTur.Models;
using GodTur.Models.Context;

namespace GodTur.Controllers
{
    public class TravelPackageController : Controller
    {
        private readonly OfferResponseContext _context;

        public TravelPackageController(OfferResponseContext context)
        {
            _context = context;
        }

        // GET: TravelPackage
        public async Task<IActionResult> Index()
        {
            var offerResponseContext = _context.TravelPackages.Include(t => t.OutboundFlight).Include(t => t.PackageHotel).Include(t => t.ReturnFlight);
            return View(await offerResponseContext.ToListAsync());
        }

        // GET: TravelPackage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackages
                .Include(t => t.OutboundFlight)
                .Include(t => t.PackageHotel)
                .Include(t => t.ReturnFlight)
                .FirstOrDefaultAsync(m => m.TravelPackageId == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackage/Create
        public IActionResult Create()
        {
            ViewData["OutboundFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            ViewData["PackageHotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId");
            ViewData["ReturnFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId");
            return View();
        }

        // POST: TravelPackage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TravelPackageId,OutboundFlightId,ReturnFlightId,PackageHotelId,PackagePrice")] TravelPackage travelPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(travelPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OutboundFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", travelPackage.OutboundFlightId);
            ViewData["PackageHotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", travelPackage.PackageHotelId);
            ViewData["ReturnFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", travelPackage.ReturnFlightId);
            return View(travelPackage);
        }

        // GET: TravelPackage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackages.FindAsync(id);
            if (travelPackage == null)
            {
                return NotFound();
            }
            ViewData["OutboundFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", travelPackage.OutboundFlightId);
            ViewData["PackageHotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", travelPackage.PackageHotelId);
            ViewData["ReturnFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", travelPackage.ReturnFlightId);
            return View(travelPackage);
        }

        // POST: TravelPackage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TravelPackageId,OutboundFlightId,ReturnFlightId,PackageHotelId,PackagePrice")] TravelPackage travelPackage)
        {
            if (id != travelPackage.TravelPackageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackageExists(travelPackage.TravelPackageId))
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
            ViewData["OutboundFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", travelPackage.OutboundFlightId);
            ViewData["PackageHotelId"] = new SelectList(_context.Hotels, "HotelId", "HotelId", travelPackage.PackageHotelId);
            ViewData["ReturnFlightId"] = new SelectList(_context.Flights, "FlightId", "FlightId", travelPackage.ReturnFlightId);
            return View(travelPackage);
        }

        // GET: TravelPackage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _context.TravelPackages
                .Include(t => t.OutboundFlight)
                .Include(t => t.PackageHotel)
                .Include(t => t.ReturnFlight)
                .FirstOrDefaultAsync(m => m.TravelPackageId == id);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelPackage = await _context.TravelPackages.FindAsync(id);
            if (travelPackage != null)
            {
                _context.TravelPackages.Remove(travelPackage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelPackageExists(int id)
        {
            return _context.TravelPackages.Any(e => e.TravelPackageId == id);
        }
    }
}
