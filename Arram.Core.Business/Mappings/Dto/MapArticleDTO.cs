using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapArticleDTO
  {
    internal static ArticleDTO MapDataToDTO(Article objet)
    {
      ArticleDTO rtn = new ArticleDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.LicenceId = objet.LicenceId;
        rtn.DateArticle = objet.DateArticle;        
        rtn.Texte = objet.Texte;
        rtn.TypeArticleId = objet.TypeArticleId;
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<ArticleDTO> MapDataToDTO(List<Article> objets)
    {
      List<ArticleDTO> rtn = new List<ArticleDTO>();
      rtn = objets.Select(x => new ArticleDTO()
      {
        Id = x.Id,
        LicenceId = x.LicenceId,
        DateArticle = x.DateArticle,       
        Texte = x.Texte,
        TypeArticleId = x.TypeArticleId,
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted,
      }).ToList();
      return rtn;
    }
  }
}
