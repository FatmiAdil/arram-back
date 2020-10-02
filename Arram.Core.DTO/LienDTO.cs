using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class LienDTO : BaseEntityDTO
  {
    [Required]
    public int CategorieId { get; set; }

    [Required]
    public string Url { get; set; }

    [Required]
    public string Texte { get; set; }

    [Required]
    public string Desc { get; set; }

    public int? Ordre { get; set; }

    public CategorieLienDTO CategorieLien { get; set; }
  }
}
