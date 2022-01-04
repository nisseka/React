using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class User : ApplicationUser
    {
	public string RolesString { get; set; }

	public string Name
	{
	    get
	    {
		return String.Format("{0} {1}", FirstName, LastName);
	    }
	}

	public User()
	{

	}

	public User(ApplicationUser source)
	{
	    Id = source.Id;
	    FirstName = source.FirstName;
	    LastName = source.LastName;
	    BirthDate = source.BirthDate;
	    UserName = source.UserName;
	    Email = source.Email;
	    PhoneNumber = source.PhoneNumber;
	}
    }
}
