using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapLienDTO
  {
    internal static LienDTO MapDataToDTO(Lien objet)
    {
      LienDTO rtn = new LienDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.CategorieId = objet.CategorieLienId;
        rtn.Desc = objet.Desc;
        rtn.Ordre = objet.Ordre;
        rtn.Texte = objet.Texte;
        rtn.Url = objet.Url;
        rtn.CategorieLien = MapCategorieLienDTO.MapDataToDTO(objet.CategorieLien);
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<LienDTO> MapDataToDTO(List<Lien> objets)
    {
      List<LienDTO> rtn = new List<LienDTO>();
      rtn = objets.Select(x => new LienDTO()
      {
        Id = x.Id,
        CategorieId = x.CategorieLienId,
        Desc = x.Desc,
        Ordre = x.Ordre,
        Texte = x.Texte,
        Url = x.Url,
        CategorieLien = MapCategorieLienDTO.MapDataToDTO(x.CategorieLien),
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted,
      }).ToList();
      return rtn;
    }
  }
}