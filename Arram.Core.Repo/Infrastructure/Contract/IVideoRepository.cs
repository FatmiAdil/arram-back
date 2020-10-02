using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface IVideoRepository
  {
    Task<Video> GetAsync(int id);
    Task<List<Video>> GetAllAsync(CancellationToken ct = default);
    Task<List<Video>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<Video>> SearchAsync(SearchVideo searchParams, CancellationToken ct = default);
    Task<Video> CreateAsync(Video objet);
    Task<Video> UpdateAsync(Video objet);
    Task<bool> DeleteAsync(int Id);
  }
}
