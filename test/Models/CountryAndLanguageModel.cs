namespace test
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CountryAndLanguageModel : DbContext
    {
        public CountryAndLanguageModel()
            : base("name=CountryAndLanguageModel")
        {
        }

        public virtual DbSet<country> country { get; set; }
        public virtual DbSet<countrylanguage> countrylanguage { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<country>()
                .Property(e => e.code)
                .IsFixedLength();

            modelBuilder.Entity<country>()
                .Property(e => e.gnp)
                .HasPrecision(10, 2);

            modelBuilder.Entity<country>()
                .Property(e => e.gnpold)
                .HasPrecision(10, 2);

            modelBuilder.Entity<country>()
                .Property(e => e.code2)
                .IsFixedLength();

            modelBuilder.Entity<country>()
                .HasMany(e => e.countrylanguage)
                .WithRequired(e => e.country)
                .HasForeignKey(e => e.countrycode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<country>()
                .HasMany(e => e.countrylanguage1)
                .WithRequired(e => e.country1)
                .HasForeignKey(e => e.countrycode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<countrylanguage>()
                .Property(e => e.countrycode)
                .IsFixedLength();
        }
    }
}
