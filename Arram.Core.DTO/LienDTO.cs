using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class LienDTO : BaseEntityDTO
  {
    [Required]
    public string Libelle { get; set; }
  }
}
