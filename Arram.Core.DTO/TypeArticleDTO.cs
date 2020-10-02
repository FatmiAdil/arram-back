using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class TypeArticleDTO : BaseEntityDTO
  {
    [Required]
    public string Libelle { get; set; }
  }
}
