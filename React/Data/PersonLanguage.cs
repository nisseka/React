using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace React.Data
{
    [Table("PersonLanguages")]
    public class PersonLanguage
    {
	public int PersonId { get; set; }
	public DBPerson Person { get; set; }

	public int LanguageId { get; set; }
	public DBLanguage Language { get; set; }
    }
}
