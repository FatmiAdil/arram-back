using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Arram.Core.API.Models;
using Arram.Core.Business.Interfaces;
using Arram.Core.Common.Logging;
using Arram.Core.DTO;
using Arram.Core.SearchService;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arram.Core.API.Controllers
{
  //[Authorize]
  [ServiceFilter(typeof(TrackPerformanceFilter))]
  [Produces("application/json")]
  [EnableCors("CorsPolicy")]
  [Route("api/[controller]")]
  [ApiController]
  public class PhotoController : ControllerBase
  {
    private readonly IPhotoLogic _logic;
    private readonly ILoggingService loggingService = null;
    private readonly LogItem logItem = new LogItem();

    public PhotoController(IPhotoLogic logic, ILoggingService LoggingService)
    {
      _logic = logic;
      loggingService = LoggingService;
      logItem.NomApplication = "Arram";
      logItem.Layer = "Controleur Photo";
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(PhotoDTO))]
    public async Task<ActionResult<PhotoDTO>> Get(int id)
    {
      PhotoDTO resultat = await _logic.GetAsync(id);

      if (resultat == null)
      {
        logItem.Message += "Pas trouvé " + id.ToString();
        loggingService.WriteError(logItem);
        return StatusCode(StatusCodes.Status404NotFound);
      }

      return resultat;
    }

    [HttpGet("licence/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(PhotoDTO))]
    public async Task<ActionResult<List<PhotoDTO>>> GetByLicenceId(int licenceId)
    {
      List<PhotoDTO> resultat = await _logic.GetByLicenceIdAsync(licenceId);

      if (resultat == null)
      {
        logItem.Message += "Pas trouvé " + licenceId.ToString();
        loggingService.WriteError(logItem);
        return StatusCode(StatusCodes.Status404NotFound);
      }

      return resultat;
    }



    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(List<PhotoDTO>))]
    public async Task<ActionResult<List<PhotoDTO>>> Get(CancellationToken ct = default)
    {
      try
      {
        return new ObjectResult(await _logic.ListeActifAsync());
      }
      catch (Exception ex)
      {
        logItem.Exception = ex;
        loggingService.WriteError(logItem);
        return StatusCode(500, ex);
      }
    }


    [HttpGet("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(List<PhotoDTO>))]
    public async Task<ActionResult<PhotosModel>> Search([FromQuery] SearchPhoto searchParams)
    {

      PhotosModel retour = new PhotosModel();
      IEnumerable<PhotoDTO> resultat = await _logic.SearchAsync(searchParams);
      retour.TotalCount = resultat.ToList().Count;
      resultat = resultat
        .Skip(searchParams.PageIndex * searchParams.PageSize)
        .Take(searchParams.PageSize)
        .ToList();

      retour.Items = resultat;

      if (resultat == null)
      {
        logItem.Message += "Search Pas trouvé " + searchParams.ToString();
        loggingService.WriteError(logItem);
        return StatusCode(StatusCodes.Status404NotFound);
      }
      return retour;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PhotoDTO>> Post([FromBody] PhotoDTO objet)
    {
      logItem.Layer = "Controleur";
      logItem.MethodName = "Post Photo";
      if (ModelState.IsValid)
      {
        try
        {
          PhotoDTO resultat = await _logic.CreateAsync(objet);
          loggingService.WritePerf(logItem);
          return resultat;
        }
        catch (Exception ex)
        {
          logItem.Exception = ex;
          loggingService.WriteError(logItem);
          return BadRequest("Exception occurs");
        }
      }
      else
      {
        return BadRequest("PhotoDTO invalide");
      }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PhotoDTO>> Put([FromBody] PhotoDTO objet)
    {
      logItem.Layer = "Controleur";
      logItem.MethodName = "PUT Photo";
      if (ModelState.IsValid)
      {
        try
        {
          PhotoDTO resultat = await _logic.UpdateAsync(objet);
          loggingService.WritePerf(logItem);
          return resultat;
        }
        catch (Exception ex)
        {
          logItem.Exception = ex;
          loggingService.WriteError(logItem);
          return BadRequest("Photo operation update impossible"); ;
        }
      }
      else
      {
        return BadRequest("Photo invalide");
      }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PhotoDTO>> Delete(int id)
    {
      bool result = await _logic.DeleteAsync(id);
      if (!result)
      {
        return BadRequest("Photo invalide");
      }
      else
      {
        return Ok("Delete fait");
      }
    }
  }
}
