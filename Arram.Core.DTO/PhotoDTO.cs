namespace Arram.Core.DTO
{
  public class PhotoDTO : EntityBaseDTO
  {
    public int LicenceId { get; set; }
    public string Description { get; set; }

    public string Url { get; set; }
  }
}
