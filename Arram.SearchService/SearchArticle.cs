namespace Arram.Core.SearchService
{
  public class SearchArticle : SearchBase
  {
    public string Titre { get; set; }
    public int? AuteurId { get; set; }
  }
}
