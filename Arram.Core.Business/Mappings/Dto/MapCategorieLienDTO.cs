using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapCategorieLienDTO
  {
    internal static CategorieLienDTO MapDataToDTO(CategorieLien objet)
    {
      CategorieLienDTO rtn = new CategorieLienDTO();
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

    internal static List<CategorieLienDTO> MapDataToDTO(List<CategorieLien> objets)
    {
      List<CategorieLienDTO> rtn = new List<CategorieLienDTO>();
      rtn = objets.Select(x => new CategorieLienDTO()
      {
        Id = x.Id,
        Libelle = x.Libelle,        
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted
      }).ToList();
      return rtn;
    }
  }
}
