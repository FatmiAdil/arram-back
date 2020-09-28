using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface IPhotoRepository
  {
    Task<Photo> GetAsync(int id);
    Task<List<Photo>> GetAllAsync(CancellationToken ct = default);
    Task<List<Photo>> GetAllActifAsync(CancellationToken ct = default);
    Task<List<Photo>> SearchAsync(SearchPhoto searchParams, CancellationToken ct = default);

    Task<List<Photo>> GetByLicenceIdAsync(int licenceId);
    Task<Photo> CreateAsync(Photo objet);
    Task<Photo> UpdateAsync(Photo objet);
    Task<bool> DeleteAsync(int Id);
  }
}
