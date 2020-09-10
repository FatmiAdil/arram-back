using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace Arram.Core.Common.Logging
{
  public class TrackPerformanceFilter : IActionFilter
  {
    private PerfTracker tracker;
    private string _product;
    private string _layer;
    private ILoggingService _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TrackPerformanceFilter"/> class.
    /// Objet de mesure de perf
    /// </summary>
    /// <param name="product">Nom de l'application</param>
    /// <param name="layer">couche logicielle</param>
    /// /// <param name="logguerService">Service de log</param>
    public TrackPerformanceFilter(string product, string layer, ILoggingService logguerService)
    {
      _product = product;
      _layer = layer;
      _logger = logguerService;
    }

    /// <summary>
    /// S'execute quand l'action du controleur s'execute
    /// </summary>
    /// <param name="context">Context d'execution</param>
    public void OnActionExecuting(ActionExecutingContext context)
    {
      var request = context.HttpContext.Request;
      var activity = $"{request.Path}-{request.Method}";

      var dict = new Dictionary<string, object>();
      foreach (var key in context.RouteData.Values?.Keys)
      {
        dict.Add($"RouteData-{key}", (string)context.RouteData.Values[key]);
      }

      var details = WebHelper.GetWebLogDetail(_product, _layer, activity, context.HttpContext, _logger, dict);

      tracker = new PerfTracker(details);
    }

    /// <summary>
    /// Intervient apres l'execution de l'action
    /// </summary>
    /// <param name="context">contexte d'execution</param>
    public void OnActionExecuted(ActionExecutedContext context)
    {
      if (tracker != null)
      {
        tracker.Stop(_logger);
      }
    }
  }
}
