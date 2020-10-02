using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface ILienLogic
  {
    Task<LienDTO> GetAsync(int id);
    Task<IEnumerable<LienDTO>> ListeAsync();
    Task<IEnumerable<LienDTO>> ListeActifAsync();
    Task<IEnumerable<LienDTO>> SearchAsync(SearchLien searchParams);


    Task<LienDTO> CreateAsync(LienDTO trace);
    Task<LienDTO> UpdateAsync(LienDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
