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
  public class LienLogic : ILienLogic
  {
    private readonly ILienRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public LienLogic(ILoggingService Logging, ILienRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Lien Logic", NomApplication = "Arram ", Message = "Lien Logic" };
    }

    public async Task<LienDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapLienDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<LienDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapLienDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<LienDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapLienDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<LienDTO>> SearchAsync(SearchLien searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapLienDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<LienDTO> CreateAsync(LienDTO objet)
    {
      Lien entite = MapLien.ToEntity(objet, false);
      entite = MapLien.ToEntity(objet, true);

      Lien lRet = await _repo.CreateAsync(entite);
      return MapLienDTO.MapDataToDTO(lRet);
    }

    public async Task<LienDTO> UpdateAsync(LienDTO objet)
    {
      Lien entite = MapLien.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapLienDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}
