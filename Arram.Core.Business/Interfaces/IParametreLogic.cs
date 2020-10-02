using Arram.Core.DTO;
using System.Threading.Tasks;

namespace Arram.Core.Business.Interfaces
{
  public interface IParametreLogic
  {
    Task<ParametreDTO> GetAsync();        
    
  }
}
