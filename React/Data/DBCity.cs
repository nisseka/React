using React.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace React.Data
{
    [Table("Cities")]
    public class DBCity
    {
	[Key]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }

	public List<DBPerson> People { get; set; }

	public Country Country { get; set; }
	public int CountryId { get; set; }

	public string PeopleString
	{
	    get
	    {
		List<DBPerson> peopleList = People;
		string peopleString;

		if (peopleList != null)
		{
		    peopleString = String.Format("{0}: ", peopleList.Count);

		    int i = 0;
		    foreach (var item in peopleList)
		    {
			if (i > 0)
			{
			    peopleString += ",";
			}
			peopleString += item.Name;
			i++;
		    }
		}
		else
		{
		    peopleString = "0";
		}
		return peopleString;
	    }
	}

	public DBCity()
	{

	}

	public DBCity(AddCityInputModel cityData)
	{
	    Name = cityData.Name;
	    CountryId = cityData.CountryId;
	}
    }
}
