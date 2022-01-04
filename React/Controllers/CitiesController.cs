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
    [Authorize(Roles ="Admin")]
    public class CitiesController : DBController
    {
	public CitiesController(DatabaseDbContext context) : base(context)
	{

	}

	public IActionResult Index()
	{
	    CitiesViewModel citiesViewModel = new CitiesViewModel(this, DBContext);

	    return View(citiesViewModel);
	}

	[HttpPost]
	public IActionResult AddCity(AddCityInputModel cityData)
	{
	    CitiesViewModel citiesViewModel = new CitiesViewModel(this, DBContext);

	    citiesViewModel.AddCity(cityData);

	    return RedirectToAction("Index");
	}

	public IActionResult DeleteCity(int id)
	{
	    CitiesViewModel citiesViewModel = new CitiesViewModel(this, DBContext);
	    citiesViewModel.RemoveCityFromDB(id);

	    return RedirectToAction("Index");
	}

    }
}
