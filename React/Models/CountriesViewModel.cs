using React.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class CountriesViewModel : DBModel
    {

	public CountriesViewModel(Controller aController, DatabaseDbContext dbContext) : base(aController, dbContext)
	{

	}

	public Country AddCountry(AddCountryInputModel countryData)
	{
	    Country country = null;

	    if (aController.ModelState.IsValid)
	    {
		country = new Country(countryData);

		AddCountryToDB(country);
	    }

	    return country;
	}

	public int AddCountryToDB(Country aCountry)
	{
	    aCountry.Id = 0;                                 // Set ID to 0 to allow addition to database

	    DBContext.Countries.Add(aCountry);
	    DBContext.SaveChanges();
	    return DBContext.Countries.Count();
	}

	public bool RemoveCountryFromDB(int ID)
	{
	    bool success = false;

	    Country country = DBContext.Countries.Find(ID);
	    if (country != null)
	    {
		Countries.Remove(country);
		DBContext.Countries.Remove(country);
		DBContext.SaveChanges();
		success = true;
	    }
	    return success;
	}
    }
}
