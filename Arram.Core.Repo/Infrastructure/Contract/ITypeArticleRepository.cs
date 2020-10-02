using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface ITypeArticleRepository
  {
    Task<TypeArticle> GetAsync(int id);
    Task<List<TypeArticle>> GetAllAsync(CancellationToken ct = default);
    Task<List<TypeArticle>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<TypeArticle>> SearchAsync(SearchTypeArticle searchParams, CancellationToken ct = default);
    Task<TypeArticle> CreateAsync(TypeArticle objet);
    Task<TypeArticle> UpdateAsync(TypeArticle objet);
    Task<bool> DeleteAsync(int Id);
  }
}
