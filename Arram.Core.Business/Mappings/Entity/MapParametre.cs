using Arram.Core.DAL.Entities;
using Arram.Core.DTO;

namespace Arram.Core.Business.Mappings
{
  internal static class MapParametre
  {
    internal static Parametre ToEntity(ParametreDTO objet)
    {
      Parametre rtn = new Parametre();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.VersionAnnuaire = objet.VersionAnnuaire;
        rtn.VersionLien = objet.VersionLien;
        rtn.VersionNews = objet.VersionNews;        
        rtn.VersionPhoto = objet.VersionPhoto;
        rtn.VersionRelais = objet.VersionRelais;
        rtn.SuppressorId = objet.SuppressorId;
        rtn.IsDeleted = objet.IsDeleted;
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;        
      }
      return rtn;
    }
  }
}
