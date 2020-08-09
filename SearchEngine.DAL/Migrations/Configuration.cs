using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace SearchEngine.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SearchEngine.DAL.SearchEngineContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "SearchEngine.DAL.SearchEngineContext";
        }

        protected override void Seed(SearchEngine.DAL.SearchEngineContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
