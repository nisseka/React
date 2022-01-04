using React.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public enum Column5Modes { RemoveLink,DisplayID}

    public class Person
    {
	private int itemIndex;
	private Column5Modes column5Mode;
	
	public string Name { get; set; }
	public string PhoneNumber { get; set; }
	public string City { get; set; }

	public int ID { get; set; }
	public int ItemIndex { get => itemIndex; set { itemIndex = value; } }

	public string RowClass { get; set; }

	public string Languages { get; set; }

	public Column5Modes Column5Mode { get => column5Mode; set { column5Mode = value; } }

	public Person()
	{
	    Name = string.Empty;
	    PhoneNumber = string.Empty;
	    City = string.Empty;
	    Languages = string.Empty;
	}

	public Person(Person source)
	{
	    Name = source.Name;
	    PhoneNumber = source.PhoneNumber;
	    Languages = source.Languages;
	    City = source.City;
	    ID = source.ID;
	    itemIndex = source.itemIndex;
	    column5Mode = source.column5Mode;
	}

	public Person(DBPerson source)
	{
	    Name = source.Name;
	    PhoneNumber = source.PhoneNumber;
	    City = source.City != null ? source.City.Name : string.Empty;
	    ID = source.ID;
	    Languages = source.LanguagesString;
	}


	public Person(AddPersonInputModel personData)
	{
	    Name = personData.Name;
	    PhoneNumber = personData.PhoneNumber;
	}

	public Person(string aName, string aCity, string aPhoneNumber)
	{
	    Name = aName;
	    City = aCity;
	    PhoneNumber = aPhoneNumber;
	}
    }
}
