using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TaxCalculator.Dal.Models
{
    public class TaxCalculatorDBContext : DbContext
    {
        public TaxCalculatorDBContext()
        {
        }

        public TaxCalculatorDBContext(DbContextOptions<TaxCalculatorDBContext> options)
            : base(options)
        {
        }

        public DbSet<TaxTypes> TaxTypes { get; set; }
        public DbSet<TaxRules> TaxRules { get; set; }
        public DbSet<Municipalities> Municipalities { get; set; }
        public DbSet<TaxPeriod> TaxPeriod { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxTypes>().Property(x => x.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TaxTypes>().Property(x => x.UpdatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TaxRules>().Property(x => x.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TaxRules>().Property(x => x.UpdatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TaxPeriod>().Property(x => x.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<TaxPeriod>().Property(x => x.UpdatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Municipalities>().Property(x => x.CreatedOn).HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Municipalities>().Property(x => x.UpdatedOn).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<TaxTypes>().HasData(
               new TaxTypes() { TaxTypeId = 1, TaxType = "Daily", Order = 1 },
               new TaxTypes() { TaxTypeId = 2, TaxType = "Weekly", Order = 2 },
               new TaxTypes() { TaxTypeId = 3, TaxType = "Weekly", Order = 3 },
               new TaxTypes() { TaxTypeId = 4, TaxType = "Yearly", Order = 4 }
             );

            modelBuilder.Entity<TaxRules>().HasData(
               new TaxRules() { TaxRuleId = 1, TaxRule = 1, Description = "Add the taxes" },
               new TaxRules() { TaxRuleId = 2, TaxRule = 2, Description = "Choose the smallest period tax" }
             );

            modelBuilder.Entity<TaxPeriod>().HasData(
               new TaxPeriod() { TaxPeriodId = 1, MunicipalityId = 1, From = DateTime.Parse("2020.01.01"), To = DateTime.Parse("2020.12.31"), TaxTypeId = 4, Tax = 0.3 },
               new TaxPeriod() { TaxPeriodId = 2, MunicipalityId = 1, From = DateTime.Parse("2020.01.01"), To = DateTime.Parse("2020.01.31"), TaxTypeId = 3, Tax = 0.2 },
               new TaxPeriod() { TaxPeriodId = 3, MunicipalityId = 1, From = DateTime.Parse("2020.01.06"), To = DateTime.Parse("2020.01.12"), TaxTypeId = 2, Tax = 0.1 },
               new TaxPeriod() { TaxPeriodId = 4, MunicipalityId = 2, From = DateTime.Parse("2020.01.01"), To = DateTime.Parse("2020.12.31"), TaxTypeId = 4, Tax = 0.2 },
               new TaxPeriod() { TaxPeriodId = 5, MunicipalityId = 2, From = DateTime.Parse("2020.05.01"), To = DateTime.Parse("2020.05.31"), TaxTypeId = 3, Tax = 0.4 },
               new TaxPeriod() { TaxPeriodId = 7, MunicipalityId = 2, From = DateTime.Parse("2020.01.01"), TaxTypeId = 1, Tax = 0.1 },
               new TaxPeriod() { TaxPeriodId = 8, MunicipalityId = 2, From = DateTime.Parse("2020.12.25"), TaxTypeId = 1, Tax = 0.1 }
             );

            modelBuilder.Entity<Municipalities>().HasData(
               new Municipalities() { MunicipalityId = 1, MunicipalityName = "Kaunas", TaxRuleId = 1 },
               new Municipalities() { MunicipalityId = 2, MunicipalityName = "Vilnius", TaxRuleId = 2 }
             );
        }
    }
}
