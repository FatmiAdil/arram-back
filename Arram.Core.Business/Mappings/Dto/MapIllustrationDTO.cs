using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapIllustrationDTO
  {
    internal static IllustrationDTO MapDataToDTO(Illustration objet)
    {
      IllustrationDTO rtn = new IllustrationDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.ArticleId = objet.ArticleId;
        rtn.Description = objet.Description;
        rtn.Url = objet.Url;
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<IllustrationDTO> MapDataToDTO(List<Illustration> objets)
    {
      List<IllustrationDTO> rtn = new List<IllustrationDTO>();
      rtn = objets.Select(x => new IllustrationDTO()
      {
        Id = x.Id,
        ArticleId = x.ArticleId,
        Description = x.Description,
        Url = x.Url,
        DateCreation = x.DateCreation,
        DateModification = x.DateModification, 
        IsDeleted = x.IsDeleted
      }).ToList();
      return rtn;
    }
  }
}
