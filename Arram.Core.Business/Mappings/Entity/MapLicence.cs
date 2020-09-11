using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;

namespace Arram.Core.Business.Mappings
{
  internal static class MapLicence
  {
    internal static Licence ToEntity(LicenceDTO objet, bool creation)
    {
      Licence rtn = new Licence();
      if (null != objet)
      {        
        rtn.Adresse1 = objet.Adresse1;
        rtn.Adresse2 = objet.Adresse2;
        rtn.AnneeLicence = objet.AnneeLicence;
        rtn.CodePostal = objet.CodePostal;
        
        
        rtn.Email = objet.Email;
        rtn.Indicatif = objet.Indicatif;
        
        rtn.Nom = objet.Nom;
        rtn.Prenom = objet.Prenom;
        rtn.QraLocator = objet.QraLocator;
        rtn.SuppressorId = objet.SuppressorId;
        rtn.UserId = objet.UserId;
        rtn.Ville = objet.Ville;
        rtn.Website = objet.Website;
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
