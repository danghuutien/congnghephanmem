namespace gioithieudaihocvinh.Migrations
{
    using gioithieudaihocvinh.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using gioithieudaihocvinh.Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<Gioithieudaihocvinh.Models.GioithieuContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Gioithieudaihocvinh.Models.GioithieuContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Users.AddOrUpdate(x => x.Id,
               new User()
               {
                   Fullname = "admin",
                   Gmail = "danghuutien@gmail.com",
                   Phone = 0375071575,
                   Password = Functions.MD5Password("12082001")
               });
        }
    }
}
