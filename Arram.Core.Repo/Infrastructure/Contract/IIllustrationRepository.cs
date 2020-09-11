using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface IIllustrationRepository
  {
    Task<Illustration> GetAsync(int id);
    Task<List<Illustration>> GetAllAsync(CancellationToken ct = default);
    Task<List<Illustration>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<Illustration>> SearchAsync(SearchIllustration searchParams, CancellationToken ct = default);
    Task<Illustration> CreateAsync(Illustration objet);
    Task<Illustration> UpdateAsync(Illustration objet);
    Task<bool> DeleteAsync(int Id);
  }
}
