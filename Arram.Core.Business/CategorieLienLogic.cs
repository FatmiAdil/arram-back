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
  public class CategorieLienLogic : ICategorieLienLogic
  {
    private readonly ICategorieLienRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public CategorieLienLogic(ILoggingService Logging, ICategorieLienRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL RefCategorieLien Logic", NomApplication = "Arram ", Message = "RefCategorieLien Logic" };
    }   

    

    public async Task<CategorieLienDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapCategorieLienDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }

    public async Task<IEnumerable<CategorieLienDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapCategorieLienDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<CategorieLienDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapCategorieLienDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<CategorieLienDTO>> SearchAsync(SearchCategorieLien searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapCategorieLienDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<CategorieLienDTO> CreateAsync(CategorieLienDTO objet)
    {
      CategorieLien entite = MapCategorieLien.ToEntity(objet, true);

      CategorieLien lRet = await _repo.CreateAsync(entite);
      return MapCategorieLienDTO.MapDataToDTO(lRet);
    }

    public async Task<CategorieLienDTO> UpdateAsync(CategorieLienDTO objet)
    {
      CategorieLien entite = MapCategorieLien.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapCategorieLienDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}
