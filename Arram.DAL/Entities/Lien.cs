﻿namespace Arram.Core.DAL.Entities
{
  public class Lien : BaseEntity
  {
    public int CategorieId { get; set; }
    public string Url { get; set; }
    public string Texte { get; set; }
    public string Desc { get; set; }
    public int Ordre { get; set; }

    public CategorieLien Categorie { get; set; }

  }
}
