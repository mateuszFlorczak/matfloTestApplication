namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.countrylanguage")]
    public partial class countrylanguage
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(3)]
        public string countrycode { get; set; }

        [Key]
        [Column(Order = 1)]
        public string language { get; set; }

        public bool isofficial { get; set; }

        public float percentage { get; set; }

        public virtual country country { get; set; }

        public virtual country country1 { get; set; }
    }
}
