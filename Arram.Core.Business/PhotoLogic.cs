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
  public class PhotoLogic : IPhotoLogic
  {
    private readonly IPhotoRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public PhotoLogic(ILoggingService Logging, IPhotoRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Photo Logic", NomApplication = "Arram ", Message = "Photo Logic" };
    }

    public async Task<PhotoDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapPhotoDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<PhotoDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapPhotoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<PhotoDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapPhotoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<PhotoDTO>> SearchAsync(SearchPhoto searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapPhotoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<List<PhotoDTO>> GetByLicenceIdAsync(int licenceId)
    {
      var liste = await _repo.GetByLicenceIdAsync(licenceId);
      var rtn = MapPhotoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<PhotoDTO> CreateAsync(PhotoDTO objet)
    {
      Photo entite = MapPhoto.ToEntity(objet, true);

      Photo lRet = await _repo.CreateAsync(entite);
      return MapPhotoDTO.MapDataToDTO(lRet);
    }

    public async Task<PhotoDTO> UpdateAsync(PhotoDTO objet)
    {
      Photo entite = MapPhoto.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapPhotoDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}
