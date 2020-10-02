using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapVideoDTO
  {
    internal static VideoDTO MapDataToDTO(Video objet)
    {
      VideoDTO rtn = new VideoDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.Description = objet.Description;
        rtn.Source = objet.Source;
        rtn.Url = objet.Url;
        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<VideoDTO> MapDataToDTO(List<Video> objets)
    {
      List<VideoDTO> rtn = new List<VideoDTO>();
      rtn = objets.Select(x => new VideoDTO()
      {
        Id = x.Id,
        Description = x.Description,
        Source = x.Source,
        Url = x.Url,
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted,
      }).ToList();
      return rtn;
    }
  }
}