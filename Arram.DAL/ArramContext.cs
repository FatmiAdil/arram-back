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
    
    public virtual DbSet<Article> Article { get; set; }
    public virtual DbSet<Illustration> Illustration { get; set; }
    public virtual DbSet<Licence> Licence { get; set; }
    public virtual DbSet<Lien> Lien { get; set; }
    public virtual DbSet<Parametre> Parametre { get; set; }
    public virtual DbSet<Photo> Photo { get; set; }
    public virtual DbSet<CategorieLien> RefCategorieLien { get; set; }
    public virtual DbSet<TypeArticle> RefTypeArticle { get; set; }
    public virtual DbSet<Relais> Relais { get; set; }
    public virtual DbSet<Video> Video { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      new ArticleConfiguration(modelBuilder.Entity<Article>());
      new IllustrationConfiguration(modelBuilder.Entity<Illustration>());      
      new LicenceConfiguration(modelBuilder.Entity<Licence>());      
      new LienConfiguration(modelBuilder.Entity<Lien>());
      new ParametreConfiguration(modelBuilder.Entity<Parametre>());
      new PhotoConfiguration(modelBuilder.Entity<Photo>());
      new CategorieLienConfiguration(modelBuilder.Entity<CategorieLien>());
      new TypeArticleConfiguration(modelBuilder.Entity<TypeArticle>());
      new RelaisConfiguration(modelBuilder.Entity<Relais>());
      new VideoConfiguration(modelBuilder.Entity<Video>());
    }
  }
}
