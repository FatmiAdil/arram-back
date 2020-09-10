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
    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      new LicenceConfiguration(modelBuilder.Entity<Licence>());
    
    }
  }
}
