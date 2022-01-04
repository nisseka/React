using React.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class DBModel
    {
	protected Controller aController;
	public readonly DatabaseDbContext DBContext;

	public List<DBPerson> People;
	public List<DBCity> Cities;
	public List<MenuLink> MenuLinks;
	public List<DBLanguage> Languages;
	public List<PersonLanguage> PersonLanguages;
	public List<Country> Countries;

	public readonly List<string> TableRowClasses;

	public DBModel(Controller aController, DatabaseDbContext dbContext)
	{
	    this.aController = aController;
	    DBContext = dbContext;

	    TableRowClasses = new List<string>();
	    TableRowClasses.Add("tableRowOdd");
	    TableRowClasses.Add("tableRowEven");

	    ReadDB();
	}

	public virtual void ReadDB()
	{
	    People = DBContext.People.ToList();
	    Languages = DBContext.Languages.ToList();
	    PersonLanguages = DBContext.PersonLanguages.ToList();
	    Cities = DBContext.Cities.ToList();
	    Countries = DBContext.Countries.ToList();

	    aController.ViewBag.Languages = Languages;          // Make language list available for partial view 'AddPerson'
	    aController.ViewBag.MenuLinks = MenuLinks;          // Make menu links available for the layout page
	    aController.ViewBag.Cities = Cities;                // Make city list available for partial view 'AddPerson'
	    aController.ViewBag.Countries = Countries;          // Make country list available for partial view 'AddCity'
	}

	public bool RemovePersonFromDB(int ID)
	{
	    bool success = false;

	    DBPerson dBPerson = DBContext.People.Find(ID);
	    if (dBPerson != null)
	    {
		People.Remove(dBPerson);
		DBContext.People.Remove(dBPerson);
		DBContext.SaveChanges();
		success = true;
	    }
	    return success;
	}

	public int AddPersonToDB(DBPerson aPerson)
	{
	    aPerson.ID = 0;                                 // Set ID to 0 to allow addition to database

	    DBContext.People.Add(aPerson);
	    DBContext.SaveChanges();
	    return DBContext.People.Count();
	}

	public virtual void PrepareView()
	{

	}
    }
}
