using Arram.Core.Common.Logging;
using Arram.Core.DAL;
using Arram.Core.DAL.Entities;
using Arram.Core.Repo.Infrastructure.Contract;
using Arram.Core.SearchService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Repositories
{
  public class LienRepository : ILienRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public LienRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }
    private async Task<bool> LienExists(int id, CancellationToken ct = default) =>
        await _context.Lien.AnyAsync(a => a.Id == id, ct);

    public async Task<Lien> GetAsync(int id)
    {
      try
      {
        var retour = await _context.Lien
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
    public async Task<List<Lien>> GetAllAsync(CancellationToken ct = default)
        => await _context.Lien.ToListAsync();

    public async Task<List<Lien>> GetAllActifAsync(CancellationToken ct = default)
        => await _context.Lien
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();

    public async Task<List<Lien>> SearchAsync(SearchLien searchParams, CancellationToken ct = default)
    {
      List<Lien> retour = await _context.Lien     
     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<Lien> CreateAsync(Lien objet)
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
                //Insert Lien
                context.Lien.Add(objet);
                context.SaveChanges();

                //Update Parametre.
                context.Parametre.FromSqlRaw("UPDATE Parametre SET VersionLien = Parametre.VersionLien + 1 WHere parametre.Id =1;");

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

    public async Task<Lien> UpdateAsync(Lien objet)
    {
      _context.Entry(await _context.Lien.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      Lien entite = await _context.Lien.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
