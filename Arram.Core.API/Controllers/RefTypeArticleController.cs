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
  public class RefTypeArticleController : ControllerBase
  {
    private readonly ITypeArticleLogic _logic;
    private readonly ILoggingService loggingService = null;
    private readonly LogItem logItem = new LogItem();

    public RefTypeArticleController(ITypeArticleLogic logic, ILoggingService LoggingService)
    {
      _logic = logic;
      loggingService = LoggingService;
      logItem.NomApplication = "Arram";
      logItem.Layer = "Controleur Article";
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(TypeArticleDTO))]
    public async Task<ActionResult<TypeArticleDTO>> Get(int id)
    {
      TypeArticleDTO resultat = await _logic.GetAsync(id);

      if (resultat == null)
      {
        logItem.Message += "Pas trouvé " + id.ToString();
        loggingService.WriteError(logItem);
        return StatusCode(StatusCodes.Status404NotFound);
      }

      return resultat;
    }



    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces(typeof(List<TypeArticleDTO>))]
    public async Task<ActionResult<List<TypeArticleDTO>>> Get(CancellationToken ct = default)
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
    [Produces(typeof(List<TypeArticleDTO>))]
    public async Task<ActionResult<TypeArticlesModel>> Search([FromQuery] SearchTypeArticle searchParams)
    {

      TypeArticlesModel retour = new TypeArticlesModel();
      IEnumerable<TypeArticleDTO> resultat = await _logic.SearchAsync(searchParams);
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
    public async Task<ActionResult<TypeArticleDTO>> Post([FromBody] TypeArticleDTO objet)
    {
      logItem.Layer = "Controleur";
      logItem.MethodName = "Post RefTypeArticle";
      if (ModelState.IsValid)
      {
        try
        {
          TypeArticleDTO resultat = await _logic.CreateAsync(objet);
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
        return BadRequest("RefTypeArticleDTO invalide");
      }
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TypeArticleDTO>> Put([FromBody] TypeArticleDTO objet)
    {
      logItem.Layer = "Controleur";
      logItem.MethodName = "PUT RefTypeArticleDTO";
      if (ModelState.IsValid)
      {
        try
        {
          TypeArticleDTO resultat = await _logic.UpdateAsync(objet);
          loggingService.WritePerf(logItem);
          return resultat;
        }
        catch (Exception ex)
        {
          logItem.Exception = ex;
          loggingService.WriteError(logItem);
          return BadRequest("RefTypeArticleDTO operation update impossible"); ;
        }
      }
      else
      {
        return BadRequest("RefTypeArticleDTO invalide");
      }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TypeArticleDTO>> Delete(int id)
    {
      bool result = await _logic.DeleteAsync(id);
      if (!result)
      {
        return BadRequest("RefTypeArticleDTO invalide");
      }
      else
      {
        return Ok("Delete fait");
      }
    }
  }
}