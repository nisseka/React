using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using React.Data;
using Microsoft.AspNetCore.Mvc;

namespace React.Models
{
    public class CitiesViewModel : DBModel
    {

	public CitiesViewModel(Controller aController, DatabaseDbContext dbContext) : base(aController, dbContext)
	{

	}

	public DBCity AddCity(AddCityInputModel cityData)
	{
	    DBCity city = null;

	    if (aController.ModelState.IsValid)
	    {
		city = new DBCity(cityData);

		AddCityToDB(city);
	    }

	    return city;
	}
	public int AddCityToDB(DBCity aCity)
	{
	    aCity.Id = 0;                                 // Set ID to 0 to allow addition to database

	    DBContext.Cities.Add(aCity);
	    DBContext.SaveChanges();
	    return DBContext.Cities.Count();
	}

	public bool RemoveCityFromDB(int ID)
	{
	    bool success = false;

	    DBCity city = DBContext.Cities.Find(ID);
	    if (city != null)
	    {
		Cities.Remove(city);
		DBContext.Cities.Remove(city);
		DBContext.SaveChanges();
		success = true;
	    }
	    return success;
	}

    }
}
