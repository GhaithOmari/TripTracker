using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripTracker.BackService.Models;
using TripTracker.BackService.Data;


namespace TripTracker.BackService.Controllers
{
	[Produces("application/json")]
	[Route("api/Trips")]
	public class TripsController : Controller
	{
		TripContext _context;
		public TripsController(TripContext context)
		{
			_context = context;
			_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
		}

		// GET api/trips
		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{

			var trips = await _context.Trips
					.AsNoTracking()
					.ToListAsync();
			return Ok(trips);
		}

		// GET api/trips/5
		[HttpGet("{id}")]
		public Trip Get(int id)
		{
			return _context.Trips.Find(id);
		}

		// POST api/trips
		[HttpPost]
		public IActionResult Post([FromBody]Trip value)
		{
			
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			// we can make it like _context.Add(value); and the compiler will understand it normally
			// but we will not take advantage of that for the current senario
			// the good one is when we want to include another object.
			_context.Trips.Add(value);
			_context.SaveChanges();
			return Ok();
		}

		// PUT api/trips/5
		[HttpPut("{id}")]
		public async Task <IActionResult> PutAsync(int id, [FromBody]Trip value)
		{
			// we can make the validation as:
			//if (_context.Trips.Find(id) == null)
			// But this will be heaver for the DB than what we want it to be
			//instead 

			if (!_context.Trips.Any(t => t.Id == id))
			{
				return NotFound();
			}
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			_context.Trips.Update(value);
			await _context.SaveChangesAsync();

			return Ok();

		}

		// DELETE api/trips/5
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var myTrip = _context.Trips.Find(id);

			if (myTrip == null)
			{
				return NotFound();
			}

			_context.Trips.Remove(myTrip);
			_context.SaveChanges();

			return NoContent();
		}
	}
}