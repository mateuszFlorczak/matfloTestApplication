using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class WorldLanguages : Comparer<WorldLanguages>
    {
        public string LanguageName { get; set; }
        public int Population { get; set; }
        public float Percentage { get; set; }

        public override int Compare(WorldLanguages x, WorldLanguages y)
        {
            //throw new NotImplementedException();
            if (x.LanguageName == y.LanguageName)
                return 0;
            else
                return 1;
        }
    }
}