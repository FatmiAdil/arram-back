using Arram.Core.SearchService;
using Arram.Core.DAL.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface ILicenceRepository
  {
      Task<Licence> GetAsync(int id);
      Task<List<Licence>> GetAllAsync(CancellationToken ct = default);
      Task<List<Licence>> GetAllActifAsync(CancellationToken ct = default);
      Task<List<Licence>> SearchAsync(SearchLicence searchParams, CancellationToken ct = default);      
      Task<Licence> CreateAsync(Licence objet);
      Task<Licence> UpdateAsync(Licence objet);
      Task<bool> DeleteAsync(int Id);
  
  }
}
