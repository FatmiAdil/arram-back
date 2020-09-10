using System;
using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DAL.Entities
{
  public abstract class BaseEntity
  {
    [Key]
    public int? Id { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int? SuppressorId { get; set; }
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime DateModification { get; set; } = DateTime.UtcNow;
  }
}
