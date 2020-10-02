using Arram.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arram.Core.DAL.Configurations
{
  public class ArticleConfiguration
  {
    public ArticleConfiguration(EntityTypeBuilder<Article> entity)
    {
      entity.HasKey(e => e.Id);
      entity.Property(p => p.DateArticle).IsRequired().HasColumnType("Date");
      entity.Property(p => p.TypeArticleId).IsRequired();
      entity.Property(p => p.Texte).IsRequired();
      entity.Property(p => p.Titre).IsRequired();
      entity.Property(p => p.LicenceId).IsRequired();
      entity.Property(e => e.IsDeleted).HasColumnName("isDeleted").HasDefaultValueSql("(0)");
      entity.Property(e => e.DateCreation).HasDefaultValueSql("(getutcdate())");
      entity.Property(e => e.DateModification).HasDefaultValueSql("(getutcdate())");
    }
  }
}
