namespace CarsharingSystem.Data.Migrations
{
    using CarsharingSystem.Common.GeocodeAPI;
    using CarsharingSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net.Http;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            
            var countries = GoogleApi.GetAllCountries();
            if (context.Countries.Count() == 0)
            {
                foreach (var country in countries)
                {
                    context.Countries.AddOrUpdate(
                        c => c.Name,
                        new Country
                        {
                            Name = country.name,
                            NativeName = country.nativeName
                        }
                        );
                }
            }

            context.SaveChanges();
        }
    }

    
}
