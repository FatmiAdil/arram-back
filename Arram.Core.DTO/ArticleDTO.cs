using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DTO
{
  public class ArticleDTO : BaseEntityDTO
  {
    public ArticleDTO()
    {
      Illustrations = new HashSet<IllustrationDTO>();
    }

    [Required]
    public int LicenceId { get; set; }

    [Required]
    public int TypeArticleId { get; set; }

    [Required]
    public string Titre { get; set; }

    [Required]
    public string Texte { get; set; }

    [Required]
    public DateTime DateArticle { get; set; }    


    public virtual LicenceDTO Licence { get; set; }

    public virtual TypeArticleDTO RefTypeArticle { get; set; }

    public virtual ICollection<IllustrationDTO> Illustrations { get; set; }
  }
}
