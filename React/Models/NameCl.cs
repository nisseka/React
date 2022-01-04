using React.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class NameCl
    {
        public int Id { get; set; }
	public string Name { get; set; }

	
	public NameCl(DBCity source)
	{
	    Id = source.Id;
	    Name = source.Name;
	}

	public NameCl(DBLanguage source)
	{
	    Id = source.Id;
	    Name = source.Name;
	}
    }

}
