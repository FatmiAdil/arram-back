using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface ICategorieLienRepository
  {
    Task<CategorieLien> GetAsync(int id);
    Task<List<CategorieLien>> GetAllAsync(CancellationToken ct = default);
    Task<List<CategorieLien>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<CategorieLien>> SearchAsync(SearchCategorieLien searchParams, CancellationToken ct = default);
    Task<CategorieLien> CreateAsync(CategorieLien objet);
    Task<CategorieLien> UpdateAsync(CategorieLien objet);
    Task<bool> DeleteAsync(int Id);
  }
}
