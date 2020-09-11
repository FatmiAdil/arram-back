using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arram.Core.Business.Mappings
{
  internal static class MapIllustration
  {
    internal static Illustration ToEntity(IllustrationDTO objet, bool creation)
    {
      Illustration rtn = new Illustration();
      if (null != objet)
      {
        rtn.ArticleId = objet.ArticleId;
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
