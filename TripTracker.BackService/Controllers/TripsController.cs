using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TripTracker.BackService.Models;

namespace TripTracker.BackService.Controllers
{
	[Produces("application/json")]
	[Route("api/Trips")]
	public class TripsController : Controller
	{
		private Repository _repository;
		public TripsController(Repository repository)
		{
			_repository = repository;
		}

		// GET api/trips
		[HttpGet]
		public IEnumerable<Trip> Get()
		{
			return _repository.Get();
		}

		// GET api/trips/5
		[HttpGet("{id}")]
		public Trip Get(int id)
		{
			return _repository.Get(id);
		}

		// POST api/trips
		[HttpPost]
		public void Post([FromBody]Trip value)
		{
			_repository.Add(value);
		}

		// PUT api/trips/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody]Trip value)
		{
			_repository.Update(value);
		}

		// DELETE api/trips/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			_repository.Remove(id);
		}
	}
}