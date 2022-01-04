using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class AddCountryInputModel
    {
	[DataType(DataType.Text)]
	[Display(Name = "Name:")]
	[MaxLength(60, ErrorMessage = "Country name is too long")]
	[Required(ErrorMessage = "A name is required")]
	public string Name { get; set; }

	[DataType(DataType.Text)]
	[Display(Name = "Country Code:")]
	[MaxLength(2, ErrorMessage = "Contry Code is max 2 characters")]
	[Required(ErrorMessage = "A country code is required")]
	public string CountryCode { get; set; }

	public AddCountryInputModel()
	{
	    Name = string.Empty;
	    CountryCode = string.Empty;
	}
    }
}
