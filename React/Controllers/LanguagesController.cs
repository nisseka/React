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
    public class LanguagesController : DBController
    {
	public LanguagesController(DatabaseDbContext context) : base(context)
	{

	}

	public IActionResult Index()
	{
	    LanguagesViewModel languageViewModel = new LanguagesViewModel(this, DBContext);

	    return View(languageViewModel);
	}

	[HttpPost]
	public IActionResult AddLanguage(AddLanguageInputModel personData)
	{
	    LanguagesViewModel languageViewModel = new LanguagesViewModel(this, DBContext);

	    languageViewModel.AddLanguage(personData);

	    return RedirectToAction("Index");
	}

	public IActionResult DeleteLanguage(int id)
	{
	    LanguagesViewModel languageViewModel = new LanguagesViewModel(this, DBContext);
	    languageViewModel.RemoveLanguageFromDB(id);

	    return RedirectToAction("Index");
	}

    }
}
