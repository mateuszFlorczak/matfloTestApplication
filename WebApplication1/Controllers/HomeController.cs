using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CountryAndLanguageModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private CountryAndLanguageModel.CountryAndLanguageEntities db = new CountryAndLanguageModel.CountryAndLanguageEntities();
        public HomeController() { }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult countries()
        {
            List<Country> Countries = db.Countries.ToList();
            List<Countrylanguage> CountryLanguages = db.Countrylanguages.ToList();

            Models.CountriesAndLanguages CAL = new Models.CountriesAndLanguages();

            CAL.Countries = (from Country in Countries
                             join Countrylanguage in CountryLanguages on Country.Code equals Countrylanguage.Countrycode
                             where Countrylanguage.Isofficial == true
                             orderby Country.Continent ascending,
                                     Country.Name ascending,
                                     Countrylanguage.Percentage descending
                             select Country).ToList();
            CAL.CountryLanguages = (from Country in Countries
                                    join Countrylanguage in CountryLanguages on Country.Code equals Countrylanguage.Countrycode
                                    where Countrylanguage.Isofficial == true
                                    orderby Country.Continent ascending,
                                            Country.Name ascending,
                                            Countrylanguage.Percentage descending
                                    select Countrylanguage).ToList();

            return View(CAL);
        }
        public ActionResult groupedCountries()
        {
            List<Countrylanguage> countryLanguages = db.Countrylanguages.ToList();
            List<Country> Countries = db.Countries.ToList();

            List<Models.WorldLanguages> WL = new List<Models.WorldLanguages>();
            long population = 0;
            foreach(var countr in Countries)
            {
                population += countr.Population;
            }

            WL = (from Countrylanguage in countryLanguages
                  join Country in Countries on Countrylanguage.Countrycode equals Country.Code
                  group new { Countrylanguage, Country } by Countrylanguage.Language into Languages
                  select new Models.WorldLanguages
                  {
                      LanguageName = Languages.Key,
                      Population = Languages.Sum(x => x.Country.Population),
                      Percentage = 1//(float)Math.Round((double)(Languages.Sum(x => x.Country.Population) / population), 2)
                  }).OrderByDescending((x => x.Population)).ThenBy(x => x.LanguageName).ToList();
            
            return View(WL);
        }
        public ActionResult country(string CountryCode)
        {
            if (CountryCode == null)
            {

            }
            else if (CountryCode.Count() != 3)
            {

            }
            Country country = (from Country in db.Countries 
                               where Country.Code == CountryCode 
                               select Country).First();
            ViewBag.CountryName = country.Name;
            List<Countrylanguage> countryLanguages = (from CountryLanguage in db.Countrylanguages 
                                                      where CountryLanguage.Countrycode == CountryCode 
                                                      select CountryLanguage).ToList();
            return View(countryLanguages);
        }
    }
}