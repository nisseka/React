using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using React.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using React.Data;
using Microsoft.AspNetCore.Authorization;

namespace React.Controllers
{
    [Authorize]
    public class HomeController : DBController
    {
	public HomeController(DatabaseDbContext context) : base(context)
	{
	}

	[AllowAnonymous]
	public IActionResult Index()
	{
	    return View();
	}

	public IActionResult People(string searchFor, bool caseSensitive)
	{
	    PeopleViewModel peopleViewModel = new PeopleViewModel(this,DBContext);

	    peopleViewModel.SearchFor = searchFor;
	    peopleViewModel.CaseSensitiveSearch = caseSensitive;

	    peopleViewModel.PrepareView();

	    return View(peopleViewModel);
	}

	public IActionResult EditPerson(int id)
	{
	    PeopleViewModel peopleViewModel = new PeopleViewModel(this, DBContext);

	    peopleViewModel.PrepareView();

	    Person person = peopleViewModel.FindPersonByID(id);

	    return View(person);
	}

	[HttpPost]
	public IActionResult AddPerson(AddPersonInputModel personData)
	{
	    DBPerson person = null;

	    if (ModelState.IsValid)
	    {
		person = new DBPerson(personData);

		DBContext.People.Add(person);
		DBContext.SaveChanges();

		int[] languageIDList = personData.Languages;
		if (languageIDList != null)
		{
		    PersonLanguage pl;

		    foreach (var languageID in languageIDList)
		    {
			pl = new PersonLanguage();
			pl.PersonId = person.ID;
			pl.LanguageId = languageID;
			DBContext.PersonLanguages.Add(pl);
		    }
		    DBContext.SaveChanges();
		}
	    }

	    return RedirectToAction("People");
	}

	[HttpPost]
	public IActionResult EditPerson(EditPersonInputModel personData)
	{
	    if (ModelState.IsValid)
	    {
		DBContext.PersonLanguages.ToList();         // Read PersonLanguages table

		int id = personData.Id;
		var person = DBContext.People.Find(id);

		if (person != null)
		{
		    person.Name = personData.Name;
		    person.PhoneNumber = personData.PhoneNumber;
		    person.CityId = personData.CityId;

		    PersonLanguage pl;
		    int[] languageIDList = personData.Languages;

		    if (languageIDList != null)
		    {
			if (person.Languages != null)
			{
			    person.Languages.Clear();

			    foreach (var languageID in languageIDList)
			    {
				pl = new PersonLanguage();
				pl.PersonId = id;
				pl.LanguageId = languageID;
				person.Languages.Add(pl);
			    }
			}
			else
			{       // Person doesn't have any languages..
			    foreach (var languageID in languageIDList)
			    {
				pl = new PersonLanguage();
				pl.PersonId = id;
				pl.LanguageId = languageID;
				DBContext.PersonLanguages.Add(pl);
			    }
			}
		    }

		    DBContext.People.Update(person);
		    DBContext.SaveChanges();
		}
	    }

	    return RedirectToAction("People");
	}

	public IActionResult DeletePerson(int id)
	{
	    DBPerson dBPerson = DBContext.People.Find(id);
	    if (dBPerson != null)
	    {
		DBContext.People.Remove(dBPerson);
		DBContext.SaveChanges();
	    }

	    return RedirectToAction("People");
	}
    }
}
