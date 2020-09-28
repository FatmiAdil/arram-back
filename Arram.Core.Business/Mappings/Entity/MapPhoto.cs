using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;

namespace Arram.Core.Business.Mappings
{
  internal static class MapPhoto
  {
    internal static Photo ToEntity(PhotoDTO objet, bool creation)
    {
      Photo rtn = new Photo();
      if (null != objet)
      {
        rtn.LicenceId = objet.LicenceId;
        rtn.Description = objet.Description;
        rtn.Url = objet.Url;

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
