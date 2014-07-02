using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private CountryAndLanguageModel db = new CountryAndLanguageModel();
        // GET: Home
        public HomeController() { }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult countries(int Page = 1, int PageSize = 20)
        {
            List<country> Countries =  db.country.ToList();
            List<countrylanguage> CountryLanguages = db.countrylanguage.ToList();
            
            Models.CountriesAndLanguages CAL = new Models.CountriesAndLanguages();
            
            CAL.Countries = (from Country in Countries
                             join Countrylanguage in CountryLanguages on Country.code equals Countrylanguage.countrycode
                             where Countrylanguage.isofficial == true
                             orderby Country.continent ascending,
                                     Country.name ascending,
                                     Countrylanguage.percentage descending
                             select Country).ToPagedList(Page, PageSize);

            CAL.CountryLanguages = (from Country in Countries
                                    join Countrylanguage in CountryLanguages on Country.code equals Countrylanguage.countrycode
                                    where Countrylanguage.isofficial == true
                                    orderby Country.continent ascending,
                                            Country.name ascending,
                                            Countrylanguage.percentage descending
                                    select Countrylanguage).ToPagedList(Page, PageSize);
            
            //data necessary for pagination
            CAL.Page = Page;
            CAL.Pages = CAL.Countries.PageCount;
            CAL.PageSize = PageSize;

            return View(CAL);
        }
        public ActionResult groupedCountries(int Page = 1, int PageSize = 20)
        {
            List<countrylanguage> countryLanguages = db.countrylanguage.ToList();
            List<country> Countries = db.country.ToList();

            var WL = (from Countrylanguage in countryLanguages
                  join Country in Countries on Countrylanguage.countrycode equals Country.code
                  group new { Countrylanguage, Country } by Countrylanguage.language into Languages
                  select new Models.WorldLanguages
                  {
                      LanguageName = Languages.Key,
                      Population = Languages.Sum(x => x.Country.population),
                      Percentage = 1//temporary value
                  }).OrderByDescending((x => x.Population)).ThenBy(x => x.LanguageName).ToPagedList(Page, PageSize);

            //data necessary for pagination
            ViewBag.Page = Page;
            ViewBag.Pages = WL.PageCount;
            ViewBag.PageSize = PageSize;

            return View(WL);
        }
        public ActionResult country(string CountryCode)
        {
            if (CountryCode == null)
            {
                return RedirectToAction("Index");
            }
            else if (CountryCode.Count() != 3)
            {
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    country country = (from Country in db.country
                                       where Country.code == CountryCode
                                       select Country).First();
                    ViewBag.CountryName = country.name;
                    List<countrylanguage> countryLanguages = (from CountryLanguage in db.countrylanguage
                                                              where CountryLanguage.countrycode == CountryCode
                                                              select CountryLanguage).ToList();
                    return View(countryLanguages);
                }
                catch (Exception) { }
                return RedirectToAction("Index");
            }
        }
    }
}