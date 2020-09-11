using Arram.Core.Business.Interfaces;
using Arram.Core.Business.Mappings;
using Arram.Core.Common.Logging;
using Arram.Core.DAL.Entities;
using Arram.Core.DTO;
using Arram.Core.Repo.Infrastructure.Contract;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business
{
  public class IllustrationLogic : IIllustrationLogic
  {
    private readonly IIllustrationRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public IllustrationLogic(ILoggingService Logging, IIllustrationRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL IIllustration Logic", NomApplication = "Arram ", Message = "IIllustration Logic" };
    }

    public async Task<IllustrationDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapIllustrationDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<IllustrationDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapIllustrationDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<IllustrationDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapIllustrationDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<IllustrationDTO>> SearchAsync(SearchIllustration searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapIllustrationDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IllustrationDTO> CreateAsync(IllustrationDTO objet)
    {
      Illustration entite = MapIllustration.ToEntity(objet, true);

      Illustration lRet = await _repo.CreateAsync(entite);
      return MapIllustrationDTO.MapDataToDTO(lRet);
    }

    public async Task<IllustrationDTO> UpdateAsync(IllustrationDTO objet)
    {
      Illustration entite = MapIllustration.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapIllustrationDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}


