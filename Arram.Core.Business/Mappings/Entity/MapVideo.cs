using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;

namespace Arram.Core.Business.Mappings
{
  internal static class MapVideo
  {
    internal static Video ToEntity(VideoDTO objet, bool creation)
    {
      Video rtn = new Video();
      if (null != objet)
      {
        rtn.Description = objet.Description;
        rtn.Source = objet.Source;
        rtn.Url = objet.Url;       

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
