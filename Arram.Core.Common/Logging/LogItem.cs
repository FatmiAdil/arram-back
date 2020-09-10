using System;
using System.Collections.Generic;

namespace Arram.Core.Common.Logging
{
  public class LogItem
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="LogItem"/> class.
    /// Constructeur par défaut
    /// </summary>
    public LogItem()
    {
      Timestamp = DateTime.Now;
    }

    /// <summary>
    /// Gets Date/Heure de l'opération
    /// </summary>
    public DateTime Timestamp { get; private set; }

    /// <summary>
    /// Gets or SetsTexte du message à logguer
    /// </summary>
    public string Message { get; set; }

    // LIEU

    /// <summary>
    /// Gets or Sets Application AF
    /// </summary>
    public string NomApplication { get; set; }

    /// <summary>
    /// Gets or Sets Brique logicielle/DLL/WebAPI
    /// </summary>    
    public string Layer { get; set; }

    /// <summary>
    /// Gets or Sets Methode/ NomControleur+methode/
    /// </summary>    
    public string Location { get; set; }

    /// <summary>
    /// Gets or Sets Methode/ NomControleur+methode/
    /// </summary>
    public string MethodName { get; set; }

    /// <summary>
    /// Gets or Sets Nom de la machine cliente
    /// </summary>
    public string Hostname { get; set; }

    // Qui

    /// <summary>
    /// Gets or Sets Identifiant utilisateur courant
    /// </summary>    
    public string UserId { get; set; }

    /// <summary>
    /// Gets or Sets Nom de l'utilisateur courant
    /// </summary>    
    public string UserName { get; set; }

    /// <summary>
    /// Gets or Sets Client Id identifiant du Client AF
    /// </summary>    
    public int? CustomerId { get; set; }

    /// <summary>
    /// Gets or Sets Client Id identifiant du Client AF
    /// Tenir compte de la réglementation RGDP
    /// </summary>    
    public int ClientId { get; set; }

    /// <summary>
    /// Gets or Sets Nom du Client AF
    /// </summary>    
    public string CustomerName { get; set; }

    /// <summary>
    /// Gets or Sets Mesure de performance
    /// </summary>
    public long? ElapsedMilliseconds { get; set; }

    /// <summary>
    /// Gets or Sets Exception intervenue à logguer
    /// </summary>
    public Exception Exception { get; set; }

    /// <summary>
    /// Gets or Sets Permet de retrouver l'information à partir de la communication du Client
    /// </summary>
    public string CorrelationId { get; set; }

    /// <summary>
    /// Gets or Sets Toute autre information à logguer
    /// </summary>
    public Dictionary<string, object> AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets email
    /// </summary>
    public bool AlerteEmail { get; set; }
  }
}
