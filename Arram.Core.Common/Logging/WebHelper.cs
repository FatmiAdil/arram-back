using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace Arram.Core.Common.Logging
{
  public static class WebHelper
  {
    /// <summary>
    /// Logue l'usage d'une méthode dans une page web
    /// </summary>
    /// <param name="product">nom de l'application</param>
    /// <param name="layer">couche logicielle</param>
    /// <param name="activityName">nom methode</param>
    /// <param name="context">contexte http</param>
    /// <param name="logger">Service de logg</param>
    /// <param name="additionalInfo">to be defined</param>
    public static void LogWebUsage(string product, string layer, string activityName, HttpContext context, ILoggingService logger, Dictionary<string, object> additionalInfo = null)
    {
      var details = GetWebLogDetail(product, layer, activityName, context, logger, additionalInfo);
      logger.WriteUsage(details);
    }

    /// <summary>
    /// Logue un diagnostic
    /// </summary>
    /// <param name="product">nom de l'application</param>
    /// <param name="layer">couche logicielle</param>
    /// <param name="message">nom methode</param>
    /// <param name="context">contexte http</param>
    /// <param name="logger">Service de logg</param>
    /// <param name="diagnosticInfo">to be defined</param>
    public static void LogWebDiagnostic(string product, string layer, string message, HttpContext context, ILoggingService logger, Dictionary<string, object> diagnosticInfo = null)
    {
      var details = GetWebLogDetail(product, layer, message, context, logger, diagnosticInfo);
      logger.WriteDiagnostic(details);
    }

    /// <summary>
    /// Logue une erreur
    /// </summary>
    /// <param name="product">nom de l'application</param>
    /// <param name="layer">couche logicielle</param>
    /// <param name="ex">Exception</param>
    /// <param name="context">contexte http</param>
    /// <param name="logger">Service de logg</param>
    public static void LogWebError(string product, string layer, Exception ex, HttpContext context, ILoggingService logger)
    {
      var details = GetWebLogDetail(product, layer, null, context, null);
      details.Exception = ex;

      logger.WriteError(details);
    }

    /// <summary>
    /// Obtient details
    /// </summary>
    /// <param name="product">nom de l'application</param>
    /// <param name="layer">couche logicielle</param>
    /// <param name="activityName">nom methode</param>
    /// <param name="context">contexte http</param>
    /// <param name="logger">Service de logg</param>
    /// <param name="additionalInfo">to be defined</param>
    /// <returns>Element de Log</returns>
    public static LogItem GetWebLogDetail(string product, string layer, string activityName, HttpContext context, ILoggingService logger, Dictionary<string, object> additionalInfo = null)
    {
      var detail = new LogItem
      {
        NomApplication = product,
        Layer = layer,
        Message = activityName,
        Hostname = Environment.MachineName,
        CorrelationId = Activity.Current?.Id ?? context.TraceIdentifier,
        AdditionalInfo = additionalInfo ?? new Dictionary<string, object>(),
      };

      GetUserData(detail, context);
      GetRequestData(detail, context);

      // Session data??

      // Cookie data??
      return detail;
    }

    private static void GetRequestData(LogItem detail, HttpContext context)
    {
      var request = context.Request;
      if (request != null)
      {
        detail.MethodName = request.Path;

        detail.AdditionalInfo.Add("UserAgent", request.Headers["User-Agent"]);

        // non en-US preferences here??
        detail.AdditionalInfo.Add("Languages", request.Headers["Accept-Language"]);

        var qdict = Microsoft.AspNetCore.WebUtilities
            .QueryHelpers.ParseQuery(request.QueryString.ToString());
        foreach (var key in qdict.Keys)
        {
          detail.AdditionalInfo.Add($"QueryString-{key}", qdict[key]);
        }
      }
    }

    private static void GetUserData(LogItem detail, HttpContext context)
    {
      var userId = string.Empty;
      var userName = string.Empty;
      var user = context.User;  // ClaimsPrincipal.Current is not sufficient
      if (user != null)
      {
        var i = 1; // i included in dictionary key to ensure uniqueness
        foreach (var claim in user.Claims)
        {
          if (claim.Type == ClaimTypes.NameIdentifier)
          {
            userId = claim.Value;
          }
          else if (claim.Type == "name")
          {
            userName = claim.Value;
          }
          else
          {
            // example dictionary key: UserClaim-4-role
            detail.AdditionalInfo.Add($"UserClaim-{i++}-{claim.Type}", claim.Value);
          }
        }
      }

      detail.UserId = userId;
      detail.UserName = userName;
    }
  }
}
