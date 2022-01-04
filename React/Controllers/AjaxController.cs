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
    public class AjaxController : DBController
    {
        public AjaxController(DatabaseDbContext context) : base(context)
        {
        }

        public IActionResult Index()
	{
            PeopleViewModel peopleViewModel = new PeopleViewModel(this, DBContext);
            return View(peopleViewModel);
	}

	public IActionResult GetPeopleList()
	{
            PeopleViewModel peopleViewModel = new PeopleViewModel(this, DBContext);

            peopleViewModel.PrepareView();

	    return PartialView("_PeopleListPartial", peopleViewModel);
	}

        [HttpPost]
        public IActionResult GetPersonByIndex(int personIndex)
        {
            PeopleViewModel peopleViewModel = new PeopleViewModel(this, DBContext);
            Person person = null;

            if (personIndex >= 0 && personIndex < peopleViewModel.People.Count)
	    {
                DBPerson dBPerson = peopleViewModel.People[personIndex];
                person = new Person(dBPerson);
                person.ItemIndex = personIndex;
            }

            return PartialView("_PersonDetailsPartial", person);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeletePersonByIndex(int personIndex)
        {
            bool success;
            PeopleViewModel peopleViewModel = new PeopleViewModel(this, DBContext);

            success = peopleViewModel.DeletePerson(personIndex);

            return StatusCode(success ? 200 : 404);
        }
    }
}
