using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class AddLanguageInputModel
    {
	[DataType(DataType.Text)]
	[Display(Name = "Name:")]
	[Required(ErrorMessage = "A name is required")]
	public string Name { get; set; }

	public AddLanguageInputModel()
	{
	    Name = string.Empty;
	}
    }
}
