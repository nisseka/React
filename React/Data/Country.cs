using React.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace React.Data
{
    [Table("Countries")]
    public class Country
    {
	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(60, ErrorMessage = "Country name is too long")]
	public string Name { get; set; }

	[Required]
	[MaxLength(2,ErrorMessage = "Contry Code is max 2 characters")]
	public string CountryCode { get; set; }

	public List<DBCity> Cities { get; set; }

	public string CitiesString
	{
	    get
	    {
		List<DBCity> cityList = Cities;
		string citiesString;

		if (cityList != null)
		{
		    citiesString = String.Format("{0}: ", cityList.Count);

		    int i = 0;
		    foreach (var item in cityList)
		    {
			if (i > 0)
			{
			    citiesString += ",";
			}
			citiesString += item.Name;
			i++;
		    }
		}
		else
		{
		    citiesString = "0";
		}
		return citiesString;
	    }
	}

	public Country()
	{

	}

	public Country(AddCountryInputModel countryData)
	{
	    Name = countryData.Name;
	    CountryCode = countryData.CountryCode.ToUpper();
	}
    }
}
