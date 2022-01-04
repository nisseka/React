using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class ReactAddPersonInputModel
    {
	[DataType(DataType.Text)]
	[Display(Name = "Name:")]
	[Required(ErrorMessage = "A name is required")]
	public string Name { get; set; }

	[DataType(DataType.PhoneNumber)]
	[Display(Name = "Phone Number:")]
	public string PhoneNumber { get; set; }

	[Display(Name = "City:")]
	public int CityId { get; set; }

	[Display(Name = "Languages:")]
	public string Languages { get; set; }

	public ReactAddPersonInputModel()
	{
	    Name = string.Empty;
	    PhoneNumber = string.Empty;
	    CityId = 0;
	}
    }
}
