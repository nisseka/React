using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class EditUserInputModel
    {
	public string Id { get; set; }

        [Required]
        [Display(Name = "Username:")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Birth date:")]
        public string BirthDate { get; set; }

        [Phone]
        [Display(Name = "Phone number:")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Role:")]
        public string RoleId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password: (leave blank to not change it)")]
        public string Password { get; set; }

        public EditUserInputModel()
	{

	}

	public EditUserInputModel(User source)
	{
	    if (source != null)
	    {
                Id = source.Id;
                UserName = source.UserName;
                FirstName = source.FirstName;
                LastName = source.LastName;
                BirthDate = source.BirthDate;
                PhoneNumber = source.PhoneNumber;
                Email = source.Email;
	    }
        }
    }
}
