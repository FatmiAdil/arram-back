using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Arram.Core.Business.Mappings
{
  internal static class MapPhotoDTO
  {
    internal static PhotoDTO MapDataToDTO(Photo objet)
    {
      PhotoDTO rtn = new PhotoDTO();
      if (null != objet)
      {
        rtn.Id = objet.Id;
        rtn.LicenceId = objet.LicenceId;
        rtn.Description = objet.Description;
        rtn.Url = objet.Url;

        rtn.DateCreation = objet.DateCreation;
        rtn.DateModification = objet.DateModification;
        rtn.IsDeleted = objet.IsDeleted;
      }
      return rtn;
    }

    internal static List<PhotoDTO> MapDataToDTO(List<Photo> objets)
    {
      List<PhotoDTO> rtn = new List<PhotoDTO>();
      rtn = objets.Select(x => new PhotoDTO()
      {
        Id = x.Id,
        LicenceId = x.LicenceId,
        Description = x.Description,
        Url = x.Url,
        DateCreation = x.DateCreation,
        DateModification = x.DateModification,
        IsDeleted = x.IsDeleted,
      }).ToList();
      return rtn;
    }
  }
}
