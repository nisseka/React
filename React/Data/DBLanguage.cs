using React.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace React.Data
{
    [Table("Languages")]
    public class DBLanguage
    {
	[Key]
	public int Id { get; set; }

	[Required]
	public string Name { get; set; }

	public List<PersonLanguage> People { get; set; }

	public string PeopleString 
	{
	    get
	    {
		List<PersonLanguage> peopleList = People;
		string peopleString;

		if (peopleList != null)
		{
		    peopleString = String.Format("{0}: ",peopleList.Count);

		    int i = 0;
		    foreach (var item in peopleList)
		    {
			if (i > 0)
			{
			    peopleString += ",";
			}
			peopleString += item.Person.Name;
			i++;
		    }
		} else
		{
		    peopleString = "0";
		}
		return peopleString;
	    }
	}

	public DBLanguage()
	{

	}

	public DBLanguage(AddLanguageInputModel languageData)
	{
	    Name = languageData.Name;
	}
    }
}
