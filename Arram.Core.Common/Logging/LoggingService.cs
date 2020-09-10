using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.Email;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace Arram.Core.Common.Logging
{
  public class LoggingService : ILoggingService
  {
    private IConfiguration _conf = null;
    private Dictionary<Tuple<string, string>, ILogger> monDico = null;

    #region "Ctor"    
    /// <summary>
    /// Initializes a new instance of the <see cref="AFLoggingService"/> class.
    /// Ctor
    /// </summary>
    /// <param name="conf">objet Configuration</param>
    public LoggingService(IConfiguration conf)
    {
      _conf = conf;
      InitLogger(_conf);
    }
    #endregion

    #region "Public Methods"
    /// <summary>
    /// Loggue un diagnostic
    /// </summary>
    /// <param name="infoToLog">item de log à stocker</param>
    public void WriteDiagnostic(LogItem infoToLog)
    {
      bool DiagnosticActif = false;
      bool result = bool.TryParse(_conf.GetSection("Serilog:DiagnosticActif").Value, out DiagnosticActif);
      if (DiagnosticActif)
      {
        StackFrame frame = new StackFrame(1, true);
        infoToLog.MethodName = calculeNomMethode(frame);

        if (infoToLog.AlerteEmail)
        {
          ILogger loger = monDico.Where(x => x.Key.Item2 == "Alerte").FirstOrDefault().Value;

          if (loger != null)
          {
            loger.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
          }
        }

        foreach (var loger in monDico)
        {
          if (loger.Key.ToString().Contains("Diag"))
          {
            loger.Value.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
          }
        }
      }
    }

    /// <summary>
    /// loggue une performance
    /// </summary>
    /// <param name="infoToLog">item de log à stocker</param>
    public void WritePerf(LogItem infoToLog)
    {
      bool PerformanceActif = false;
      bool result = bool.TryParse(_conf.GetSection("Serilog:PerformanceActif").Value, out PerformanceActif);
      if (PerformanceActif)
      {
        StackFrame frame = new StackFrame(1, true);
        infoToLog.MethodName = calculeNomMethode(frame);

        if (infoToLog.AlerteEmail)
        {
          ILogger loger = monDico.Where(x => x.Key.Item2 == "Alerte").FirstOrDefault().Value;

          if (loger != null)
          {
            loger.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
          }
        }

        foreach (var loger in monDico)
        {
          if (loger.Key.ToString().Contains("Perf"))
          {
            loger.Value.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
          }
        }
      }
    }

    /// <summary>
    /// loggue une utilisation
    /// </summary>
    /// <param name="infoToLog">item de log à stocker</param>
    public void WriteUsage(LogItem infoToLog)
    {
      bool UsageActif = false;
      bool result = bool.TryParse(_conf.GetSection("Serilog:UsageActif").Value, out UsageActif);
      if (UsageActif)
      {
        StackFrame frame = new StackFrame(1, true);
        infoToLog.MethodName = calculeNomMethode(frame);

        if (infoToLog.AlerteEmail)
        {
          ILogger loger = monDico.Where(x => x.Key.Item2 == "Alerte").FirstOrDefault().Value;

          if (loger != null)
          {
            loger.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
          }
        }

        foreach (var loger in monDico)
        {
          if (loger.Key.ToString().Contains("Usage"))
          {
            loger.Value.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
          }
        }
      }
    }

    /// <summary>
    /// Loggue une erreur, une exception
    /// </summary>
    /// <param name="infoToLog">item de log à stocker</param>
    public void WriteError(LogItem infoToLog)
    {
      StackFrame frame = new StackFrame(1, true);
      infoToLog.MethodName = calculeNomMethode(frame);

      ILogger logerEmailAlerte = monDico.Where(x => x.Key.Item2 == "Alerte").FirstOrDefault().Value;

      if (logerEmailAlerte != null)
      {
        logerEmailAlerte.Write(LogEventLevel.Information, "{@AFLogInformationItem}", infoToLog);
      }
      if (infoToLog.Exception != null)
      {
        infoToLog.Message = GetMessageFromException(infoToLog.Exception);
      }
      foreach (var loger in monDico)
      {
        if (loger.Key.ToString().Contains("Error"))
        {
          loger.Value.Write(LogEventLevel.Error, "{@AFLogInformationItem}", infoToLog);
        }
      }
    }
    #endregion

    private void InitLogger(IConfiguration conf)
    {
      string[] destinations = { };
      string nomApplication = conf.GetSection("Serilog:Properties:Application").Value;
      string sectionTypeLog = conf.GetSection("Serilog:TypeLog").Value;
      if (!string.IsNullOrEmpty(sectionTypeLog))
      {
        destinations = sectionTypeLog.Split(",");
      }
      monDico = new Dictionary<Tuple<string, string>, ILogger>();

      int nbDestinations = 0;
      bool fini = false;
      Dictionary<Tuple<int, string>, IConfigurationSection> WriteTo = new Dictionary<Tuple<int, string>, IConfigurationSection>();
      while (!fini)
      {
        if (string.IsNullOrEmpty(conf.GetSection(string.Format("Serilog:WriteTo:{0}:Name", nbDestinations)).Value))
        {
          fini = true;
        }
        else
        {
          IConfigurationSection trouve = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Name", nbDestinations));
          string cle = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Name", nbDestinations)).Value;
          WriteTo.Add(new Tuple<int, string>(nbDestinations, cle), trouve);
          nbDestinations++;
        }
      }

      foreach (var dest in destinations)
      {
        IConfigurationSection sectionWriteTo = WriteTo.Where(x => x.Key.Item2 == dest).FirstOrDefault().Value;
        if (sectionWriteTo != null)
        {
          int cle = WriteTo.Where(x => x.Key.Item2 == dest).FirstOrDefault().Key.Item1;

          switch (sectionWriteTo.Value)
          {
            case "Email":
              ProcessEmailSection(conf, cle, false);
              break;

            case "RollingFile":
              ProcessFileSection(conf, cle, nomApplication);
              break;

            case "Elasticsearch":
              ProcessElasticSection(conf, cle, nomApplication);

              break;
            default:
              break;
          }
        }
      }
      //Traitement section Email pour AlerteMail
      IConfigurationSection sectionEmail = WriteTo.Where(x => x.Key.Item2 == "Email").FirstOrDefault().Value;
      if (sectionEmail != null)
      {
        if (WriteTo.Where(x => x.Key.Item2 == "Email").FirstOrDefault().Value != null)
        {
          int cleEmail = WriteTo.Where(x => x.Key.Item2 == "Email").FirstOrDefault().Key.Item1;
          ProcessEmailSection(conf, cleEmail, true);
        }
      }
    }

    private void ProcessElasticSection(IConfiguration conf, int cle, string nomApplication)
    {
      string destinationElastic = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:nodeUris", cle)).Value;
      if (!string.IsNullOrEmpty(destinationElastic))
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(nomApplication.Length > 8 ? nomApplication.Substring(0, 8) : nomApplication);
        sb.Append("-{0:yyyy.MM.dd}");

        ElasticsearchSinkOptions elasticOption = new ElasticsearchSinkOptions(new Uri(destinationElastic))
        {
          AutoRegisterTemplate = true,
          CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
          AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
          IndexFormat = sb.ToString(),
        };
        ILogger elasticLogger = new LoggerConfiguration().WriteTo.Elasticsearch(elasticOption).Enrich.FromLogContext().CreateLogger();

        if (!monDico.TryAdd(new Tuple<string, string>("Elastic", "Perf"), elasticLogger))
        {
          Debug.Assert(false, "Section Elastic en double!!!!");
        }
        if (!monDico.TryAdd(new Tuple<string, string>("Elastic", "Diag"), elasticLogger))
        {
          Debug.Assert(false, "Section Elastic en double!!!!");
        }
        if (!monDico.TryAdd(new Tuple<string, string>("Elastic", "Error"), elasticLogger))
        {
          Debug.Assert(false, "Section Elastic en double!!!!");
        }
        if (!monDico.TryAdd(new Tuple<string, string>("Elastic", "Usage"), elasticLogger))
        {
          Debug.Assert(false, "Section Elastic en double!!!!");
        }
      }
    }

    private void ProcessFileSection(IConfiguration conf, int cle, string nomApplication)
    {
      string destinationFile = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:pathFormat", cle)).Value;
      if (!string.IsNullOrEmpty(destinationFile))
      {
        string prefixFile = string.Empty;
        string suffixeFile = string.Empty;

        prefixFile = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:pathFormat", cle)).Value;
        suffixeFile = nomApplication + ".log";

        ILogger perfLogger = new LoggerConfiguration()
            .WriteTo.RollingFile(prefixFile + "Perf" + suffixeFile)
            .CreateLogger();

        ILogger usageLogger = new LoggerConfiguration()
            .WriteTo.RollingFile(prefixFile + "Usage" + suffixeFile)
            .CreateLogger();

        ILogger errorLogger = new LoggerConfiguration()
            .WriteTo.RollingFile(prefixFile + "Error" + suffixeFile)
            .CreateLogger();

        ILogger diagnosticLogger = new LoggerConfiguration()
            .WriteTo.RollingFile(prefixFile + "Diag" + suffixeFile)
            .CreateLogger();

        if (!monDico.TryAdd(new Tuple<string, string>("File", "Perf"), perfLogger))
        {
          Debug.Assert(false, "Section File en double!!!!");
        }
        if (!monDico.TryAdd(new Tuple<string, string>("File", "Usage"), usageLogger))
        {
          Debug.Assert(false, "Section File en double!!!!");
        }
        if (!monDico.TryAdd(new Tuple<string, string>("File", "Error"), errorLogger))
        {
          Debug.Assert(false, "Section File en double!!!!");
        }
        if (!monDico.TryAdd(new Tuple<string, string>("File", "Diag"), diagnosticLogger))
        {
          Debug.Assert(false, "Section File en double!!!!");
        }
      }
    }

    private void ProcessEmailSection(IConfiguration conf, int cle, bool AlerteUniquement)
    {
      EmailConnectionInfo eci = null;
      NetworkCredential nc = null;
      nc = new NetworkCredential()
      {
        Password = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:NetworkCredentials:Password", cle)).Value,
        UserName = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:NetworkCredentials:UserName", cle)).Value,
      };
      bool EnableSsl = false;
      bool IsBodyHtml = false;
      int Port = 0;
      bool result = bool.TryParse(conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:EnableSsl", cle)).Value, out EnableSsl);
      result = bool.TryParse(conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:IsBodyHtml", cle)).Value, out IsBodyHtml);
      result = int.TryParse(conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:Port", cle)).Value, out Port);
      eci = new EmailConnectionInfo()
      {
        EmailSubject = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:EmailSubject", cle)).Value,
        FromEmail = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:FromEmail", cle)).Value,
        EnableSsl = EnableSsl,
        IsBodyHtml = IsBodyHtml,
        MailServer = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:MailServer", cle)).Value,
        Port = Port,
        ToEmail = conf.GetSection(string.Format("Serilog:WriteTo:{0}:Args:connectionInfo:ToEmail", cle)).Value,
        NetworkCredentials = nc,
      };

      if (valideEmailConnectionIfo(eci, nc))
      {
        //Construit loggerEmail
        ILogger emailLogger = new LoggerConfiguration()
      .MinimumLevel.Information()
        .WriteTo.Email(eci)
        .CreateLogger();
        if (!AlerteUniquement)
        {
          if (!monDico.TryAdd(new Tuple<string, string>("Email", "Perf"), emailLogger))
          {
            Debug.Assert(false, "Section Email en double!!!!");
          }
          if (!monDico.TryAdd(new Tuple<string, string>("Email", "Diag"), emailLogger))
          {
            Debug.Assert(false, "Section Email en double!!!!");
          }
          if (!monDico.TryAdd(new Tuple<string, string>("Email", "Error"), emailLogger))
          {
            Debug.Assert(false, "Section Email en double!!!!");
          }
          if (!monDico.TryAdd(new Tuple<string, string>("Email", "Usage"), emailLogger))
          {
            Debug.Assert(false, "Section Email en double!!!!");
          }
        }
        else
        {
          if (!monDico.TryAdd(new Tuple<string, string>("Email", "Alerte"), emailLogger))
          {
            Debug.Assert(false, "Alerte emailLogger Deja ajouté");
          }
        }
      }
    }
    private bool ValideEmailAdresse(string email)
    {
      try
      {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
      }
      catch
      {
        return false;
      }
    }
    private bool valideEmailConnectionIfo(EmailConnectionInfo eci, NetworkCredential nc)
    {
      if (string.IsNullOrEmpty(eci.ToEmail))
      {
        return false;
      }
      else
      {
        string[] listedestinataires = eci.ToEmail.Split(",");
        foreach (var adresse in listedestinataires)
        {
          if (!ValideEmailAdresse(adresse))
          {
            Debug.Assert(false, "Adresse destinataire invalide");
            return false;
          }
        }
      }
      if (string.IsNullOrEmpty(eci.MailServer))
      {
        return false;
      }
      if (string.IsNullOrEmpty(eci.FromEmail))
      {
        return false;
      }
      else
      {
        if (!ValideEmailAdresse(eci.FromEmail))
        {
          if (!ValideEmailAdresse(eci.FromEmail))
          {
            Debug.Assert(false, "Adresse expéditeur invalide");
            return false;
          }
        }
      }
      if (string.IsNullOrEmpty(nc.UserName))
      {
        return false;
      }
      if (string.IsNullOrEmpty(nc.Password))
      {
        return false;
      }
      if (eci.Port == 0)
      {
        return false;
      }

      return true;
    }
    private static string GetMessageFromException(Exception ex)
    {
      if (ex.InnerException != null)
      {
        return GetMessageFromException(ex.InnerException);
      }

      return ex.Message;
    }
    private string calculeNomMethode(StackFrame frame)
    {
      string retour = string.Empty;
      if (frame != null)
      {
        retour = string.Format(CultureInfo.CurrentCulture, "Nom de la méthode : {0} Nom du fichier : {1} Numero de ligne : {2}", frame.GetMethod().Name, frame.GetFileName(), frame.GetFileLineNumber().ToString(CultureInfo.CurrentCulture));
      }
      return retour;
    }
  }
}
