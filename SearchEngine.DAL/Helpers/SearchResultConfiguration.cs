using SearchEngine.Models;
using System.Data.Entity.ModelConfiguration;

namespace SearchEngine.DAL.Helpers
{
    public class SearchResultConfiguration : EntityTypeConfiguration<SearchResultModel>
    {
        public SearchResultConfiguration()
        {
            Property(s => s.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(s => s.Title).IsOptional();
            Property(s => s.Headline).IsOptional().IsMaxLength();
            Property(s => s.ModifiedDate).IsOptional();
            Property(s => s.Url).IsOptional();
            Property(s => s.ServiceName).IsOptional();
        }
    }
}
