namespace Arram.Core.SearchService
{
  public class SearchRelais : SearchBase
  {
    public string Site { get; set; }

    public string Nom { get; set; }

    public string Region { get; set; }

    public int? Bande { get; set; }
  }
}
