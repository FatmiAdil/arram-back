using Arram.Core.Common.Logging;
using Arram.Core.DAL;
using Arram.Core.DAL.Entities;
using Arram.Core.Repo.Infrastructure.Contract;
using Arram.Core.SearchService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Repositories
{
  public class RelaisRepository : IRelaisRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public RelaisRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }
    private async Task<bool> RelaisExists(int id, CancellationToken ct = default) =>
        await _context.Licence.AnyAsync(a => a.Id == id, ct);

    public async Task<Relais> GetAsync(int id)
    {
      try
      {
        var retour = await _context.Relais
          .Where(x => x.Id == id).FirstOrDefaultAsync();
        return retour;
      }
      catch (Exception ex)
      {
        logitem.Exception = ex;
        _logger.WriteError(logitem);

      }
      return null;
    }
    public async Task<List<Relais>> GetAllAsync(CancellationToken ct = default)
        => await _context.Relais.ToListAsync();

    public async Task<List<Relais>> GetAllActifAsync(CancellationToken ct = default)
        => await _context.Relais
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();

    public async Task<List<Relais>> SearchAsync(SearchRelais searchParams, CancellationToken ct = default)
    {
      List<Relais> retour = await _context.Relais     
     .Where(x => (searchParams.Bande == null ? 1 == 1 : x.Bande == searchParams.Bande))     
     .Where(x => (string.IsNullOrEmpty(searchParams.Region) ? 1 == 1 : x.Region.Contains(searchParams.Region)))
     .Where(x => (string.IsNullOrEmpty(searchParams.Nom) ? 1 == 1 : x.Nom.Contains(searchParams.Nom)))
     .Where(x => (string.IsNullOrEmpty(searchParams.Site) ? 1 == 1 : x.Nom.Contains(searchParams.Site)))
     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<Relais> CreateAsync(Relais objet)
    {
      using (var db = _context)
      {
        var strategy = db.Database.CreateExecutionStrategy();
        strategy.Execute(() =>
        {
          using (var context = _context)
          {
            //BeginTransaction
            using (var transaction = context.Database.BeginTransaction())
            {
              try
              {
                //Insert Amende
                context.Relais.Add(objet);
                context.SaveChanges();

                //End Transaction 
                transaction.Commit();
              }
              catch (Exception ex)
              {
                _logger.WriteError(new LogItem() { Exception = ex, Message = ex.Message, Layer = "Repository" });
                transaction.Rollback();
              }
            }
          }
        });
      }
      return objet;
    }

    public async Task<Relais> UpdateAsync(Relais objet)
    {
      _context.Entry(await _context.Relais.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      Relais entite = await _context.Relais.Where(x => x.Id == Id).FirstOrDefaultAsync();
      if (null != entite)
      {
        entite.IsDeleted = true;
        entite.DateModification = DateTime.UtcNow;
        return await _context.SaveChangesAsync() > 0;
      }
      return false;
    }
  }
}