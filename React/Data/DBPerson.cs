using React.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace React.Data
{
    [Table("People")]
    public class DBPerson
    {
	[Key]
	public int ID { get; set; }

	[Required]
	public string Name { get; set; }

	public string PhoneNumber { get; set; }
	
	public DBCity City { get; set; }
	public int CityId { get; set; }

	public List<PersonLanguage> Languages { get; set; }

	public string LanguagesString
	{
	    get
	    {
		List<PersonLanguage> languageList = Languages;
		string languageString;

		if (languageList != null)
		{
		    languageString = String.Format("{0}: ", languageList.Count);

		    int i = 0;
		    foreach (var item in languageList)
		    {
			if (i > 0)
			{
			    languageString += ",";
			}
			languageString += item.Language.Name;
			i++;
		    }
		} else
		{
		    languageString = "0";
		}
		return languageString;
	    }
	}

	public DBPerson()
	{

	}

	public DBPerson(AddPersonInputModel personData)
	{
	    Name = personData.Name;
	    PhoneNumber = personData.PhoneNumber;
	    CityId = personData.CityId;
	}

	public DBPerson(ReactAddPersonInputModel personData)
	{
	    Name = personData.Name;
	    PhoneNumber = personData.PhoneNumber;
	    CityId = personData.CityId;
	}

	public DBPerson(Person source)
	{
	    Name = source.Name;
	    PhoneNumber = source.PhoneNumber;
//	    City = source.City;
	    ID = source.ID;
	}
    }

}
