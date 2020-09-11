
namespace Arram.Core.DAL.Entities
{
  public class Illustration : BaseEntity
  {
    public int ArticleId { get; set; }


    public string Description { get; set; }


    public string Url { get; set; }

    public virtual Article Article { get; set; }

  }
}
