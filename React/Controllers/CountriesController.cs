using React.Data;
using React.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : DBController
    {
	public CountriesController(DatabaseDbContext context) : base(context)
	{
	}

	public IActionResult Index()
	{
	    CountriesViewModel countyViewModel = new CountriesViewModel(this, DBContext);

	    return View(countyViewModel);
	}

	[HttpPost]
	public IActionResult AddCountry(AddCountryInputModel countryData)
	{
	    CountriesViewModel countyViewModel = new CountriesViewModel(this, DBContext);

	    countyViewModel.AddCountry(countryData);

	    return RedirectToAction("Index");
	}

	public IActionResult DeleteCountry(int id)
	{
	    CountriesViewModel countyViewModel = new CountriesViewModel(this, DBContext);
	    countyViewModel.RemoveCountryFromDB(id);

	    return RedirectToAction("Index");
	}
    }
}
