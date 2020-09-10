using Arram.Core.Common.Logging;

namespace Arram.Core.Common.Logging
{
  public interface ILoggingService
  {
    /// <param name="infoToLog">Item de log à stocker</param>
    void WritePerf(LogItem infoToLog);

    /// <summary>
    /// Loggue une utilisation
    /// </summary>
    /// <param name="infoToLog">Item de log à stocker</param>
    void WriteUsage(LogItem infoToLog);

    /// <summary>
    /// Loggue une erreur ou une exception
    /// </summary>
    /// <param name="infoToLog">Item de log à stocker</param>
    void WriteError(LogItem infoToLog);

    /// <summary>
    /// Loggue un diagnostic
    /// </summary>
    /// <param name="infoToLog">Item de log à stocker</param>
    void WriteDiagnostic(LogItem infoToLog);

  }
}
