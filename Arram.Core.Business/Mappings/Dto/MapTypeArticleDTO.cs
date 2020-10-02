using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  
  internal static class MapTypeArticleDTO
  {
    internal static TypeArticleDTO MapDataToDTO(TypeArticle objet)
    {
      TypeArticleDTO rtn = new TypeArticleDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.Libelle = objet.Libelle;        
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<TypeArticleDTO> MapDataToDTO(List<TypeArticle> objets)
    {
      List<TypeArticleDTO> rtn = new List<TypeArticleDTO>();
      rtn = objets.Select(x => new TypeArticleDTO()
      {
        Id = x.Id,
        Libelle = x.Libelle,        
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted,
      }).ToList();
      return rtn;
    }
  }
}

