using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface IRelaisRepository
  {
    Task<Relais> GetAsync(int id);
    Task<List<Relais>> GetAllAsync(CancellationToken ct = default);
    Task<List<Relais>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<Relais>> SearchAsync(SearchRelais searchParams, CancellationToken ct = default);
    Task<Relais> CreateAsync(Relais objet);
    Task<Relais> UpdateAsync(Relais objet);
    Task<bool> DeleteAsync(int Id);
  }
}
