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
  public class TypeArticleLogic : ITypeArticleLogic
  {
    private readonly ITypeArticleRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public TypeArticleLogic(ILoggingService Logging, ITypeArticleRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL RefType Article Logic", NomApplication = "Arram ", Message = "RefType Article Logic" };
    }

    public async Task<TypeArticleDTO> GetAsync(int id)
    {
      var objet = await _repo.GetAsync(id);

      if (null != objet)
      {
        var rtn = MapTypeArticleDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }


    public async Task<IEnumerable<TypeArticleDTO>> ListeAsync()
    {
      var liste = await _repo.GetAllAsync();
      var rtn = MapTypeArticleDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<TypeArticleDTO>> ListeActifAsync()
    {
      var liste = await _repo.GetAllActifAsync();
      var rtn = MapTypeArticleDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<IEnumerable<TypeArticleDTO>> SearchAsync(SearchTypeArticle searchParams)
    {
      var liste = await _repo.SearchAsync(searchParams);
      var rtn = MapTypeArticleDTO.MapDataToDTO(liste);
      return rtn;
    }

    public async Task<TypeArticleDTO> CreateAsync(TypeArticleDTO objet)
    {
      TypeArticle entite = MapTypeArticle.ToEntity(objet, true);

      TypeArticle lRet = await _repo.CreateAsync(entite);
      return MapTypeArticleDTO.MapDataToDTO(lRet);
    }

    public async Task<TypeArticleDTO> UpdateAsync(TypeArticleDTO objet)
    {
      TypeArticle entite = MapTypeArticle.ToEntity(objet, false);
      var lRet = await _repo.UpdateAsync(entite);
      return MapTypeArticleDTO.MapDataToDTO(lRet);
    }
    public async Task<bool> DeleteAsync(int Id)
    {
      bool resultat = await _repo.DeleteAsync(Id);
      return resultat;
    }
  }
}
