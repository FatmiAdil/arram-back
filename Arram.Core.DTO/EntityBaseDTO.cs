using System;

namespace Arram.Core.DTO
{
  public abstract class EntityBaseDTO
  {   
    public int? Id { get; set; }
    public bool IsDeleted { get; set; } = false;

    public int? SuppressorId { get; set; }
    public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    public DateTime DateModification { get; set; } = DateTime.UtcNow;
  }
}
