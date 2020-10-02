using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class CategorieLienDTO : BaseEntityDTO
  {
    [Required]
    [StringLength(250)]
    public string Libelle { get; set; }
  }
}
