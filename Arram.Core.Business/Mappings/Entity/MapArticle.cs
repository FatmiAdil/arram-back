using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;

namespace Arram.Core.Business.Mappings
{
  internal static class MapArticle
  {
    internal static Article ToEntity(ArticleDTO objet, bool creation)
    {
      Article rtn = new Article();
      if (null != objet)
      {
        rtn.LicenceId = objet.LicenceId;
        rtn.DateArticle = objet.DateArticle;        
        rtn.Texte = objet.Texte;
        rtn.Titre = objet.Titre;
        rtn.TypeArticleId = objet.TypeArticleId;
       
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
