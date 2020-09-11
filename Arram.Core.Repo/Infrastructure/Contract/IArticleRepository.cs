using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface IArticleRepository
  {
    Task<Article> GetAsync(int id);
    Task<List<Article>> GetAllAsync(CancellationToken ct = default);
    Task<List<Article>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<Article>> SearchAsync(SearchArticle searchParams, CancellationToken ct = default);
    Task<Article> CreateAsync(Article objet);
    Task<Article> UpdateAsync(Article objet);
    Task<bool> DeleteAsync(int Id);
  }
}
