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
  public class LicenceLogic : ILicenceLogic
  {
    private readonly ILicenceRepository _repo;
    
    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public LicenceLogic(ILoggingService Logging, ILicenceRepository repo)
    {
      _repo = repo;     
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Licence Logic", NomApplication = "Arram ", Message = "Licence Logic" };
    }

    public async Task<LicenceDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapLicenceDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }

    
    public async Task<IEnumerable<LicenceDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapLicenceDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<LicenceDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapLicenceDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<LicenceDTO>> SearchAsync(SearchLicence searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapLicenceDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<LicenceDTO> CreateAsync(LicenceDTO objet)
    {
      Licence entite = MapLicence.ToEntity(objet, true);

      Licence lRet = await _repo.CreateAsync(entite);
      return MapLicenceDTO.MapDataToDTO(lRet);
    }

    public async Task<LicenceDTO> UpdateAsync(LicenceDTO objet)
    {
      Licence entite = MapLicence.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapLicenceDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }    
  }
}
