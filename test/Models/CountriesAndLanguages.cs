using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class CountriesAndLanguages
    {
        public PagedList.IPagedList<country> Countries { get; set; }
        public PagedList.IPagedList<countrylanguage> CountryLanguages { get; set; }
        public int Page;
        public int Pages;
        public int PageSize;
    }
}