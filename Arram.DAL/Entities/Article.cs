using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Arram.Core.DAL.Entities
{
  public class Article : BaseEntity
  {
    public Article()
    {
      Illustrations = new HashSet<Illustration>();
    }
    
    [Required]
    [StringLength(200)]
    public string Titre { get; set; }

    [Required]
    public string Texte { get; set; }

    public DateTime DateArticle { get; set; }

    public int LicenceId { get; set; }

    public int TypeArticleId { get; set; }
    

    public virtual Licence Licence { get; set; }

    public virtual TypeArticle RefTypeArticle { get; set; }

    public virtual ICollection<Illustration> Illustrations { get; set; }
  }
}
