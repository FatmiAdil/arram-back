using Arram.Core.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arram.Core.DAL.Configurations
{
  public class CategorieLienConfiguration
  {
    public CategorieLienConfiguration(EntityTypeBuilder<CategorieLien> entity)
    {
      entity.HasKey(e => e.Id);
      entity.Property(e => e.Libelle).HasMaxLength(250);      
      entity.Property(e => e.IsDeleted).HasColumnName("isDeleted").HasDefaultValueSql("(0)");
      entity.Property(e => e.DateCreation).HasDefaultValueSql("(getutcdate())");
      entity.Property(e => e.DateModification).HasDefaultValueSql("(getutcdate())");
    }
  }
}
