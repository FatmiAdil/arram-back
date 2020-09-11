using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;

namespace Arram.Core.Business.Mappings
{
  internal static class MapRelais
  {
    internal static Relais ToEntity(RelaisDTO objet, bool creation)
    {
      Relais rtn = new Relais();
      if (null != objet)
      {
        rtn.Region = objet.Region;
        rtn.Site = objet.Site;
        rtn.Nom = objet.Nom;
        rtn.Altitude = objet.Altitude;
        rtn.FreqEntree = objet.FreqEntree;
        rtn.FreqSortie = objet.FreqSortie;
        rtn.Latitude = objet.Latitude;
        rtn.Longitude = objet.Longitude;
        //rtn.Position = objet.Position;
        rtn.Puissance = objet.Puissance;
        rtn.QraLocator = objet.QraLocator;
        rtn.Shift = objet.Shift;
        rtn.Tone = objet.Tone;
        rtn.Bande = objet.Bande;       

        if (creation)
        {
          rtn.IsDeleted = false;
          rtn.DateCreation = DateTime.Now;
          rtn.DateModification = DateTime.Now;
        }
        else
        {
          rtn.SuppressorId = objet.SuppressorId;
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
