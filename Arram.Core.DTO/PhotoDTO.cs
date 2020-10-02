using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class PhotoDTO : BaseEntityDTO
  {
    [Required]
    public int LicenceId { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Url { get; set; }
  }
}
