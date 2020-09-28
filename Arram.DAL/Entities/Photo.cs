namespace Arram.Core.DAL.Entities
{
  public class Photo : BaseEntity
  {
    public int LicenceId { get; set; }
    public string Description { get; set; }

    public string Url { get; set; }
  }
}
