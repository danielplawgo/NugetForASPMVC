using FizzWare.NBuilder;
using NugetForAspMvc.Models;

namespace NugetForAspMvc.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NugetForAspMvc.Models.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NugetForAspMvc.Models.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.Users.Any() == false)
            {
                var users = Builder<User>.CreateListOfSize(20)
                    .All()
                    .With(u => u.Email = Faker.Internet.Email())
                    .With(u => u.FirstName = Faker.Name.First())
                    .With(u => u.LastName = Faker.Name.Last())
                    .With(u => u.UserName = Faker.Internet.UserName())
                    .Build();

                foreach (var user in users)
                {
                    context.Users.AddOrUpdate(u => u.Email, user);
                }
            }
        }
    }
}
