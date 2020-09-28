using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface IPhotoLogic
  {
    Task<PhotoDTO> GetAsync(int id);
    Task<IEnumerable<PhotoDTO>> ListeAsync();
    Task<IEnumerable<PhotoDTO>> ListeActifAsync();
    Task<IEnumerable<PhotoDTO>> SearchAsync(SearchPhoto searchParams);

    Task<List<PhotoDTO>> GetByLicenceIdAsync(int licenceId);

    Task<PhotoDTO> CreateAsync(PhotoDTO trace);
    Task<PhotoDTO> UpdateAsync(PhotoDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
