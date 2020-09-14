using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapRelaisDTO
  {
    internal static RelaisDTO MapDataToDTO(Relais objet)
    {
      RelaisDTO rtn = new RelaisDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.Altitude = objet.Altitude;
        rtn.Bande = objet.Bande;
        rtn.strBande = rtn.Bande == 1 ? "VHF" : "UHF";
        rtn.FreqEntree = objet.FreqEntree;
        rtn.FreqSortie = objet.FreqSortie;
        rtn.Latitude = objet.Latitude;
        rtn.Longitude = objet.Longitude;
        rtn.Nom = objet.Nom;
        // rtn.Position = objet.Position;
        rtn.Puissance = objet.Puissance;
        rtn.QraLocator = objet.QraLocator;

        rtn.Region = objet.Region;
        rtn.Shift = objet.Shift;
        rtn.Site = objet.Site;
        rtn.SuppressorId = objet.SuppressorId;
        rtn.Tone = objet.Tone;

        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<RelaisDTO> MapDataToDTO(List<Relais> objets)
    {
      List<RelaisDTO> rtn = new List<RelaisDTO>();
      rtn = objets.Select(x => new RelaisDTO()
      {
        Id = x.Id,
        Altitude = x.Altitude,
        Bande = x.Bande,
        strBande = x.Bande == 1 ? "VHF" : "UHF",
        FreqEntree = x.FreqEntree,
        FreqSortie = x.FreqSortie,
        Latitude = x.Latitude,
        Longitude = x.Longitude,
        Nom = x.Nom,
        //Position = x.Position,
        Puissance = x.Puissance,
        QraLocator = x.QraLocator,

        Region = x.Region,
        Shift = x.Shift,
        Site = x.Site,
        SuppressorId = x.SuppressorId,
        Tone = x.Tone,
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted,
      }).ToList();
      return rtn;
    }
  }
}
