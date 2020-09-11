using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Arram.Core.Business.Interfaces
{
  public interface IRelaisLogic
  {
    Task<RelaisDTO> GetAsync(int id);
    Task<IEnumerable<RelaisDTO>> ListeAsync();
    Task<IEnumerable<RelaisDTO>> ListeActifAsync();
    Task<IEnumerable<RelaisDTO>> SearchAsync(SearchRelais searchParams);


    Task<RelaisDTO> CreateAsync(RelaisDTO trace);
    Task<RelaisDTO> UpdateAsync(RelaisDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
