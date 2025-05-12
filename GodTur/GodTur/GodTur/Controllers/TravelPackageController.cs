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
    [ApiController]
    [Route("api/[controller]")]
    public class TravelPackageController : ControllerBase
    {
        private readonly OfferResponseContext _context;

        public TravelPackageController(OfferResponseContext context)
        {
            _context = context;
        }

        // POST: TravelPackage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public async Task<IActionResult> Create([FromBody] TravelPackage travelPackage)
        {
            
                _context.Add(travelPackage);
                await _context.SaveChangesAsync();
                return Ok(travelPackage);


        }
    }
}
