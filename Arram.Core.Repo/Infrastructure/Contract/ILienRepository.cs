using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface ILienRepository
  {
    Task<Lien> GetAsync(int id);
    Task<List<Lien>> GetAllAsync(CancellationToken ct = default);
    Task<List<Lien>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<Lien>> SearchAsync(SearchLien searchParams, CancellationToken ct = default);
    Task<Lien> CreateAsync(Lien objet);
    Task<Lien> UpdateAsync(Lien objet);
    Task<bool> DeleteAsync(int Id);
  }
}
