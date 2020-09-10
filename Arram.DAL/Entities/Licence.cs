using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Arram.Core.DAL.Entities
{
  public class Licence : BaseEntity
  {
    public Licence()
    {
      //Article = new HashSet<Article>();
      //Photo = new HashSet<Photo>();
    }

    

    public int? UserId { get; set; }

    [Required]
    [StringLength(10)]
    public string Indicatif { get; set; }

    [StringLength(35)]
    public string Nom { get; set; }

    [StringLength(35)]
    public string Prenom { get; set; }

    [StringLength(80)]
    public string Adresse1 { get; set; }

    [StringLength(80)]
    public string Adresse2 { get; set; }

    [StringLength(6)]
    public string CodePostal { get; set; }

    [StringLength(50)]
    public string Ville { get; set; }

    [StringLength(50)]
    public string Email { get; set; }

    [StringLength(80)]
    public string Website { get; set; }

    [StringLength(10)]
    public string QraLocator { get; set; }


    public int? AnneeLicence { get; set; }

    public bool Actif { get; set; }

    
    //public virtual ICollection<Article> Article { get; set; }

    //public virtual ICollection<Photo> Photo { get; set; }

  }
}
