using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Arram.Core.Common.Logging
{
  public class PerfTracker
  {
    private readonly Stopwatch sw;
    private readonly LogItem infoToLog;

    /// <summary>
    /// Initializes a new instance of the <see cref="PerfTracker"/> class.
    /// Constructeur par défaut
    /// </summary>
    /// <param name="details">Objet contenant les paramétres nécessaires à Serilog</param>
    public PerfTracker(LogItem details)
    {
      sw = Stopwatch.StartNew();
      infoToLog = details;

      var beginTime = DateTime.Now;
      if (infoToLog.AdditionalInfo == null)
      {
        infoToLog.AdditionalInfo = new Dictionary<string, object>()
                {
                    { "Started", beginTime.ToString(CultureInfo.InvariantCulture) },
                };
      }
      else
      {
        infoToLog.AdditionalInfo.Add(
            "Started", beginTime.ToString(CultureInfo.InvariantCulture));
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerfTracker"/> class.
    /// Log performance d'une méthode
    /// </summary>
    /// <param name="name">Nom de l'application</param>
    /// <param name="userId">Id utilisateur courant</param>
    /// <param name="userName">Nom utilisateur courant</param>
    /// <param name="location">Nom methode</param>
    /// <param name="product">Applicatif</param>
    /// <param name="layer">Couche</param>
    public PerfTracker(string name, string userId, string userName, string location, string product, string layer)
    {
      infoToLog = new LogItem()
      {
        Message = name,
        UserId = userId,
        UserName = userName,
        NomApplication = product,
        Layer = layer,
        MethodName = location,
        Hostname = Environment.MachineName,
      };

      var beginTime = DateTime.Now;
      infoToLog.AdditionalInfo = new Dictionary<string, object>()
            {
                { "Started", beginTime.ToString(CultureInfo.InvariantCulture) },
            };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PerfTracker"/> class.
    /// Autre methode
    /// </summary>
    /// <param name="name">Nom de l'application</param>
    /// <param name="userId">Id utilisateur courant</param>
    /// <param name="userName">Nom utilisateur courant</param>
    /// <param name="location">Nom methode</param>
    /// <param name="product">Applicatif</param>
    /// <param name="layer">Couche</param>
    /// <param name="perfParams">Paramétres de perf</param>
    public PerfTracker(string name, string userId, string userName, string location, string product, string layer, Dictionary<string, object> perfParams)
        : this(name, userId, userName, location, product, layer)
    {
      foreach (var item in perfParams)
      {
        infoToLog.AdditionalInfo.Add("input-" + item.Key, item.Value);
      }
    }

    /// <summary>
    /// Arrête le chrono
    /// </summary>
    /// <param name="logger">Service Loggueur </param>
    public void Stop(ILoggingService logger)
    {
      sw.Stop();
      infoToLog.ElapsedMilliseconds = sw.ElapsedMilliseconds;
      if (logger != null)
      {
        logger.WritePerf(infoToLog);
      }
    }
  }
}
