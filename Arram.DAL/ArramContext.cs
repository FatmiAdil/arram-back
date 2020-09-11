using Microsoft.EntityFrameworkCore;
using Arram.Core.DAL.Configurations;
using Arram.Core.DAL.Entities;

namespace Arram.Core.DAL
{
  public class ArramContext : DbContext
  {
    public ArramContext()
    {

    }

    public ArramContext(DbContextOptions<ArramContext> options) : base(options)
    { }

    public virtual DbSet<Licence> Licence { get; set; }
    public virtual DbSet<Article> Article { get; set; }
    public virtual DbSet<Illustration> Illustration { get; set; }
    public virtual DbSet<Relais> Relais { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      new LicenceConfiguration(modelBuilder.Entity<Licence>());
      new ArticleConfiguration(modelBuilder.Entity<Article>());
      new IllustrationConfiguration(modelBuilder.Entity<Illustration>());
      new RelaisConfiguration(modelBuilder.Entity<Relais>());

    }
  }
}
