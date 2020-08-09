namespace SearchEngine.DAL
{
    using System.Data.Entity;
    using SearchEngine.DAL.Helpers;
    using SearchEngine.DAL.Migrations;
    using SearchEngine.Models;

    public partial class SearchEngineContext : DbContext
    {
        public SearchEngineContext()
            : base("name=SearchEngineContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SearchEngineContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Adds configurations for Student from separate class
            modelBuilder.Configurations.Add(new SearchResultConfiguration());

            modelBuilder.Entity<SearchResultModel>()
                .ToTable("search_results");

            modelBuilder.Entity<SearchResultModel>()
                .MapToStoredProcedures();
        }

        public virtual DbSet<SearchResultModel> SearchResult { get; set; }
    }
}
