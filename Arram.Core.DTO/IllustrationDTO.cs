namespace Arram.Core.DTO
{
  public class IllustrationDTO : EntityBaseDTO 
  {
    public int ArticleId { get; set; }


    public string Description { get; set; }


    public string Url { get; set; }

    public virtual ArticleDTO Article { get; set; }
  }
}
