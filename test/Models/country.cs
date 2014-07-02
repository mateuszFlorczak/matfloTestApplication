namespace test
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("public.country")]
    public partial class country
    {
        public country()
        {
            countrylanguage = new HashSet<countrylanguage>();
            countrylanguage1 = new HashSet<countrylanguage>();
        }

        [Key]
        [StringLength(3)]
        public string code { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string continent { get; set; }

        [Required]
        public string region { get; set; }

        public float surfacearea { get; set; }

        public short? indepyear { get; set; }

        public int population { get; set; }

        public float? lifeexpectancy { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gnp { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? gnpold { get; set; }

        [Required]
        public string localname { get; set; }

        [Required]
        public string governmentform { get; set; }

        public string headofstate { get; set; }

        public int? capital { get; set; }

        [Required]
        [StringLength(2)]
        public string code2 { get; set; }

        public virtual ICollection<countrylanguage> countrylanguage { get; set; }

        public virtual ICollection<countrylanguage> countrylanguage1 { get; set; }
    }
}
