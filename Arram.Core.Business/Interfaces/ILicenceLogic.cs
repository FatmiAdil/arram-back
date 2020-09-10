using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface ILicenceLogic
  {
    Task<LicenceDTO> GetAsync(int id);
    Task<IEnumerable<LicenceDTO>> ListeAsync();
    Task<IEnumerable<LicenceDTO>> ListeActifAsync();
    Task<IEnumerable<LicenceDTO>> SearchAsync(SearchLicence searchParams);

   
    Task<LicenceDTO> CreateAsync(LicenceDTO trace);
    Task<LicenceDTO> UpdateAsync(LicenceDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
