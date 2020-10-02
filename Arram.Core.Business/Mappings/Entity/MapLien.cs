using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;

namespace Arram.Core.Business.Mappings
{
  internal static class MapLien
  {
    internal static Lien ToEntity(LienDTO objet, bool creation)
    {
      Lien rtn = new Lien();
      if (null != objet)
      {
        rtn.Url = objet.Url;
        rtn.Texte = objet.Texte;
        rtn.Desc = objet.Desc;
        rtn.Ordre = objet.Ordre;
        rtn.CategorieLienId = objet.CategorieId;

        if (creation)
        {
          rtn.IsDeleted = false;
          rtn.DateCreation = DateTime.Now;
          rtn.DateModification = DateTime.Now;
        }
        else
        {
          rtn.Id = objet.Id;
          rtn.IsDeleted = objet.IsDeleted;
          rtn.DateCreation = objet.DateCreation;
          rtn.DateModification = objet.DateModification;
        }
      }
      return rtn;
    }
  }
}
