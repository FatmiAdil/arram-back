using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface ICategorieLienLogic
  {
    Task<CategorieLienDTO> GetAsync(int id);
    Task<IEnumerable<CategorieLienDTO>> ListeAsync();
    Task<IEnumerable<CategorieLienDTO>> ListeActifAsync();
    Task<IEnumerable<CategorieLienDTO>> SearchAsync(SearchCategorieLien searchParams);    

    Task<CategorieLienDTO> CreateAsync(CategorieLienDTO objet);
    Task<CategorieLienDTO> UpdateAsync(CategorieLienDTO objet);
    Task<bool> DeleteAsync(int Id);
  }
}
