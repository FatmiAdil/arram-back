using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface ITypeArticleLogic
  {
    Task<TypeArticleDTO> GetAsync(int id);
    Task<IEnumerable<TypeArticleDTO>> ListeAsync();
    Task<IEnumerable<TypeArticleDTO>> ListeActifAsync();
    Task<IEnumerable<TypeArticleDTO>> SearchAsync(SearchTypeArticle searchParams);


    Task<TypeArticleDTO> CreateAsync(TypeArticleDTO trace);
    Task<TypeArticleDTO> UpdateAsync(TypeArticleDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
