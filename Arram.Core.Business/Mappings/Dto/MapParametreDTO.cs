using Arram.Core.DAL.Entities;
using Arram.Core.DTO;

namespace Arram.Core.Business.Mappings
{
  internal static class MapParametreDTO
  {
    internal static ParametreDTO MapDataToDTO(Parametre objet)
    {
      ParametreDTO rtn = new ParametreDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.VersionAnnuaire = objet.VersionAnnuaire;
        rtn.VersionLien = objet.VersionLien;
        rtn.VersionNews = objet.VersionNews;
        rtn.VersionPhoto = objet.VersionPhoto;
        rtn.VersionRelais = objet.VersionRelais;
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }
  }
}
