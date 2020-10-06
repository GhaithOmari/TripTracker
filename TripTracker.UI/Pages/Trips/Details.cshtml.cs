using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TripTracker.BackService.Models;
using TripTracker.UI.Data;
using TripTracker.UI.Services;

namespace TripTracker.UI.Pages.Trips
{
	public class DetailsModel : PageModel
	{
		private readonly IApiClient _client;

		public DetailsModel(IApiClient client)
		{
			_client = client;
		}

		public Trip Trip { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			Trip = await _client.GetTripAsync(id.Value);

			if (Trip == null)
			{
				return NotFound();
			}
			return Page();
		}
	}
}
