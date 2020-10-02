using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class VideoDTO : BaseEntityDTO
  {
    [Required]
    public string Url { get; set; }

    [Required]
    public string Description { get; set; }
    [Required]
    public string Source { get; set; }
  }
}
