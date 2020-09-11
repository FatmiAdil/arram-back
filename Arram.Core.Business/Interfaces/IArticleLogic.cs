using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Arram.Core.Business.Interfaces
{
  public interface IArticleLogic
  {
    Task<ArticleDTO> GetAsync(int id);
    Task<IEnumerable<ArticleDTO>> ListeAsync();
    Task<IEnumerable<ArticleDTO>> ListeActifAsync();
    Task<IEnumerable<ArticleDTO>> SearchAsync(SearchArticle searchParams);


    Task<ArticleDTO> CreateAsync(ArticleDTO trace);
    Task<ArticleDTO> UpdateAsync(ArticleDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
