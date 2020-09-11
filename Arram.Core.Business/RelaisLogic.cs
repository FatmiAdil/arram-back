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
  public class RelaisLogic : IRelaisLogic
  {
    private readonly IRelaisRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public RelaisLogic(ILoggingService Logging, IRelaisRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Relais Logic", NomApplication = "Arram ", Message = "Relais Logic" };
    }

    public async Task<RelaisDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapRelaisDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<RelaisDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapRelaisDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<RelaisDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapRelaisDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<RelaisDTO>> SearchAsync(SearchRelais searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapRelaisDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<RelaisDTO> CreateAsync(RelaisDTO objet)
    {
      Relais entite = MapRelais.ToEntity(objet, true);

      Relais lRet = await _repo.CreateAsync(entite);
      return MapRelaisDTO.MapDataToDTO(lRet);
    }

    public async Task<RelaisDTO> UpdateAsync(RelaisDTO objet)
    {
      Relais entite = MapRelais.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapRelaisDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}
