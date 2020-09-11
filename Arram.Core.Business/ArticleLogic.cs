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
  public class ArticleLogic : IArticleLogic
  {
    private readonly IArticleRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public ArticleLogic(ILoggingService Logging, IArticleRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Article Logic", NomApplication = "Arram ", Message = "Article Logic" };
    }

    public async Task<ArticleDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapArticleDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<ArticleDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapArticleDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<ArticleDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapArticleDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<ArticleDTO>> SearchAsync(SearchArticle searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapArticleDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<ArticleDTO> CreateAsync(ArticleDTO objet)
    {
      Article entite = MapArticle.ToEntity(objet, true);

      Article lRet = await _repo.CreateAsync(entite);
      return MapArticleDTO.MapDataToDTO(lRet);
    }

    public async Task<ArticleDTO> UpdateAsync(ArticleDTO objet)
    {
      Article entite = MapArticle.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapArticleDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}
