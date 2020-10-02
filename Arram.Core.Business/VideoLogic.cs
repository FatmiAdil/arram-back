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
  public class VideoLogic : IVideoLogic
  {
    private readonly IVideoRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public VideoLogic(ILoggingService Logging, IVideoRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Video Logic", NomApplication = "Arram ", Message = "Video Logic" };
    }

    public async Task<VideoDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapVideoDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<VideoDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapVideoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<VideoDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapVideoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<VideoDTO>> SearchAsync(SearchVideo searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapVideoDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<VideoDTO> CreateAsync(VideoDTO objet)
    {
      Video entite = MapVideo.ToEntity(objet, true);

      Video lRet = await _repo.CreateAsync(entite);
      return MapVideoDTO.MapDataToDTO(lRet);
    }

    public async Task<VideoDTO> UpdateAsync(VideoDTO objet)
    {
      Video entite = MapVideo.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapVideoDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}