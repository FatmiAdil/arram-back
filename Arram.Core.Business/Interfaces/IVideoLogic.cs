using Arram.Core.DTO;
using Arram.Core.SearchService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface IVideoLogic
  {
    Task<VideoDTO> GetAsync(int id);
    Task<IEnumerable<VideoDTO>> ListeAsync();
    Task<IEnumerable<VideoDTO>> ListeActifAsync();
    Task<IEnumerable<VideoDTO>> SearchAsync(SearchVideo searchParams);


    Task<VideoDTO> CreateAsync(VideoDTO trace);
    Task<VideoDTO> UpdateAsync(VideoDTO trace);
    Task<bool> DeleteAsync(int Id);
  }
}
