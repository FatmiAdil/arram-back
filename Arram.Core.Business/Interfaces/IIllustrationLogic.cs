using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface IIllustrationLogic
  {
    Task<IllustrationDTO> GetAsync(int id);
    Task<IEnumerable<IllustrationDTO>> ListeAsync();
    Task<IEnumerable<IllustrationDTO>> ListeActifAsync();
    Task<IEnumerable<IllustrationDTO>> SearchAsync(SearchIllustration searchParams);


    Task<IllustrationDTO> CreateAsync(IllustrationDTO trace);
    Task<IllustrationDTO> UpdateAsync(IllustrationDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
