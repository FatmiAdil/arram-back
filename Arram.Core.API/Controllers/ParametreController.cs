using Arram.Core.Business.Interfaces;
using Arram.Core.Common.Logging;
using Arram.Core.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Arram.Core.API.Controllers
{

  //[Authorize]
  [ServiceFilter(typeof(TrackPerformanceFilter))]
  [Produces("application/json")]
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  [ApiController]
  public class ParametreController : ControllerBase
  {
    private readonly IParametreLogic _logic;
    private readonly ILoggingService loggingService = null;
    private readonly LogItem logItem = new LogItem();

    public ParametreController(IParametreLogic logic, ILoggingService LoggingService)
    {
      _logic = logic;
      loggingService = LoggingService;
      logItem.NomApplication = "Arram";
      logItem.Layer = "Controleur Parametre";
    }


    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(ArticleDTO))]
    public async Task<ActionResult<ParametreDTO>> Get()
    {
      ParametreDTO resultat = await _logic.GetAsync();

      if (resultat == null)
      {
        logItem.Message += "Pas trouvé ";
        loggingService.WriteError(logItem);
        return StatusCode(StatusCodes.Status404NotFound);
      }

      return resultat;
    }

  }
}