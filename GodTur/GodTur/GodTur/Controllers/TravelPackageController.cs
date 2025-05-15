using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GodTur.Models;
using GodTur.Models.Context;
using Shared;
using GodTur.Services;

namespace GodTur.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelPackageController : ControllerBase
    {
		//private readonly OfferResponseContext _context;

		//public TravelPackageController(OfferResponseContext context)
		//{
		//    _context = context;
		//}
		private readonly TravelPackageService _travelPackageService;

		public TravelPackageController(TravelPackageService travelPackageService)
		{
			_travelPackageService = travelPackageService;
		}

		[HttpPost, Route("Create")]
		public async Task<ActionResult<TravelPackage>> CreateTravelPackage([FromBody] TravelPackageDTO dto)
		{
			var createdPackage = await _travelPackageService.CreateTravelPackageAsync(dto);
			if (createdPackage == null)
				return BadRequest("Unable to create travel package. Check that airports and country exist.");

			return Ok(createdPackage);
		}
		// GET: api/TravelPackage
		//[HttpGet, Route("Get")]
		//      public async Task<ActionResult<IEnumerable<TravelPackage>>> GetTravelPackages()
		//      {
		//          return await _context.TravelPackages.ToListAsync();
		//      }

		// GET: api/TravelPackage/5
		//[HttpGet("{id}")]
		//public async Task<ActionResult<TravelPackage>> GetTravelPackage(int id)
		//{
		//    var travelPackage = await _context.TravelPackages.FindAsync(id);

		//    if (travelPackage == null)
		//    {
		//        return NotFound();
		//    }

		//    return travelPackage;
		//}

		// PUT: api/TravelPackage/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//[HttpPut("{id}")]
		//public async Task<IActionResult> PutTravelPackage(int id, TravelPackage travelPackage)
		//{
		//    if (id != travelPackage.TravelPackageId)
		//    {
		//        return BadRequest();
		//    }

		//    _context.Entry(travelPackage).State = EntityState.Modified;

		//    try
		//    {
		//        await _context.SaveChangesAsync();
		//    }
		//    catch (DbUpdateConcurrencyException)
		//    {
		//        if (!TravelPackageExists(id))
		//        {
		//            return NotFound();
		//        }
		//        else
		//        {
		//            throw;
		//        }
		//    }

		//    return NoContent();
		//}

		// POST: api/TravelPackage
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		//    [HttpPost, Route("Create")]
		//    public async Task<ActionResult<TravelPackage>> PostTravelPackage([FromBody]TravelPackageDTO travelPackageDTO)
		//    {
		//        TravelPackageDTO packageParam = travelPackageDTO;
		//        TravelPackage travelPackage = CreateTravelPackage(packageParam);
		//        _context.TravelPackages.Add(travelPackage);
		//        await _context.SaveChangesAsync();

		//        return CreatedAtAction("GetTravelPackage", new { id = travelPackage.TravelPackageId }, travelPackage);
		//    }

		//    private TravelPackage CreateTravelPackage(TravelPackageDTO travelPackageDTO)
		//    {
		//        var travelPackage = new TravelPackage
		//        {
		//            PackageHotel = new Hotel
		//            {
		//	Name = travelPackageDTO.PackageHotel.HotelName,
		//	CheckInDate = travelPackageDTO.PackageHotel.CheckInDate,
		//	CheckOutDate = travelPackageDTO.PackageHotel.CheckOutDate,
		//	StayPrice = travelPackageDTO.PackageHotel.Price
		//},
		//            OutboundFlight = new Flight
		//            {
		//	DepartingAt = travelPackageDTO.OutboundFlight.DepartureDate ?? DateTime.MinValue,
		//	ArrivingAt = travelPackageDTO.OutboundFlight.ReturnDate ?? DateTime.MinValue,
		//	FlightPrice = (decimal)(travelPackageDTO.OutboundFlight.Price ?? 0),
		//	FlightNumber = travelPackageDTO.OutboundFlight.FlightNumber
		//},
		//            ReturnFlight = new Flight
		//            {
		//	DepartingAt = travelPackageDTO.ReturnFlight.DepartureDate ?? DateTime.MinValue,
		//	ArrivingAt = travelPackageDTO.ReturnFlight.ReturnDate ?? DateTime.MinValue,
		//	FlightPrice = (decimal)(travelPackageDTO.ReturnFlight.Price ?? 0),
		//	FlightNumber = travelPackageDTO.ReturnFlight.FlightNumber
		//},
		//        };
		//        return travelPackage;
		//    }

		// DELETE: api/TravelPackage/5
		//[HttpDelete("{id}")]
		//      public async Task<IActionResult> DeleteTravelPackage(int id)
		//      {
		//          var travelPackage = await _context.TravelPackages.FindAsync(id);
		//          if (travelPackage == null)
		//          {
		//              return NotFound();
		//          }

		//          _context.TravelPackages.Remove(travelPackage);
		//          await _context.SaveChangesAsync();

		//          return NoContent();
		//      }

		//      private bool TravelPackageExists(int id)
		//      {
		//          return _context.TravelPackages.Any(e => e.TravelPackageId == id);
		//      }
	}
}
