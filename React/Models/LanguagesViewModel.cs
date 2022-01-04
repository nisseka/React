using React.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace React.Models
{
    public class LanguagesViewModel : DBModel
    {
	public LanguagesViewModel(Controller aController, DatabaseDbContext dbContext) : base(aController, dbContext)
	{

	}

	public DBLanguage AddLanguage(AddLanguageInputModel languageData)
	{
	    DBLanguage language = null;

	    if (aController.ModelState.IsValid)
	    {
		language = new DBLanguage(languageData);

		AddLanguageToDB(language);
	    }

	    return language;
	}
	public int AddLanguageToDB(DBLanguage aLanguage)
	{
	    aLanguage.Id = 0;                                 // Set ID to 0 to allow addition to database

	    DBContext.Languages.Add(aLanguage);
	    DBContext.SaveChanges();
	    return DBContext.Languages.Count();
	}

	public bool RemoveLanguageFromDB(int ID)
	{
	    bool success = false;

	    DBLanguage language = DBContext.Languages.Find(ID);
	    if (language != null)
	    {
		Languages.Remove(language);
		DBContext.Languages.Remove(language);
		DBContext.SaveChanges();
		success = true;
	    }
	    return success;
	}
    }
}
