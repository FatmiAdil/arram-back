using Arram.Core.Business.Interfaces;
using Arram.Core.Business.Mappings;
using Arram.Core.Common.Logging;
using Arram.Core.DTO;
using Arram.Core.Repo.Infrastructure.Contract;
using System.Threading.Tasks;

namespace Arram.Core.Business
{
  public class ParametreLogic : IParametreLogic
  {
    private readonly IParametreRepository _repo;

    private readonly ILoggingService _logging = null;
    private LogItem _logItem = null;

    public ParametreLogic(ILoggingService Logging, IParametreRepository repo)
    {
      _repo = repo;
      _logging = Logging;
      _logItem = new LogItem() { Layer = "BLL Parametre Logic", NomApplication = "Arram ", Message = "Parametre Logic" };
    }

    public async Task<ParametreDTO> GetAsync()
    {
      var objet = await _repo.GetAsync();

      if (null != objet)
      {
        var rtn = MapParametreDTO.MapDataToDTO(objet);
        return rtn;
      }
      else
      {
        return null;
      }
    }    
  }
}
