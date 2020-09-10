using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapLicenceDTO
  {
    internal static LicenceDTO MapDataToDTO(Licence objet)
    {
      LicenceDTO rtn = new LicenceDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.Adresse1 = objet.Adresse1;
        rtn.Adresse2 = objet.Adresse2;
        rtn.AnneeLicence = objet.AnneeLicence;
        rtn.CodePostal = objet.CodePostal;
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.Email = objet.Email;
        rtn.Indicatif = objet.Indicatif;
        rtn.IsDeleted = objet.IsDeleted;
        rtn.Nom = objet.Nom;
        rtn.Prenom = objet.Prenom;
        rtn.QraLocator = objet.QraLocator;
        rtn.SuppressorId = objet.SuppressorId;
        rtn.UserId = objet.UserId;
        rtn.Ville = objet.Ville;
        rtn.Website = objet.Website;        
      }
      return rtn;
    }

    internal static List<LicenceDTO> MapDataToDTO(List<Licence> objets)
    {
      List<LicenceDTO> rtn = new List<LicenceDTO>();
      rtn = objets.Select(x => new LicenceDTO()
      {
        Id = x.Id,
       Adresse1 = x.Adresse1,
       Adresse2 = x.Adresse2,
       AnneeLicence = x.AnneeLicence,
       CodePostal = x.CodePostal,
       DateCreation = x.DateCreation,
       DateModification = x.DateModification,
       Email = x.Email,
       Indicatif = x.Indicatif,
       IsDeleted = x.IsDeleted,
       Nom = x.Nom,
       Prenom = x.Prenom,
       QraLocator = x.QraLocator,
       SuppressorId = x.SuppressorId,
       UserId = x.UserId,
       Ville = x.Ville,
       Website = x.Website,

    }).ToList();
      return rtn;
    }
  }
}
