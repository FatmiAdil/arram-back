using System;

namespace Arram.Core.DTO
{
  public class ArticleDTO : EntityBaseDTO
  {
    public ArticleDTO()
    {
      // Illustration = new HashSet<Illustration>();
    }

    public string Titre { get; set; }

    public string Texte { get; set; }

    public DateTime DateArticle { get; set; }

    public int LicenceId { get; set; }

    public int TypeArticleId { get; set; }
    

    public virtual LicenceDTO Licence { get; set; }

    //public virtual RefTypeArticleDTO RefTypeArticle { get; set; }

    //public virtual ICollection<IllustrationDTO> Illustration { get; set; }
  }
}
