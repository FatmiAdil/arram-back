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
  public class CategorieLienRepository : ICategorieLienRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public CategorieLienRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }
    private async Task<bool> CategorieLienExists(int id, CancellationToken ct = default) =>
        await _context.Illustration.AnyAsync(a => a.Id == id, ct);

    public async Task<CategorieLien> GetAsync(int id)
    {
      try
      {
        var retour = await _context.CategorieLien
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
    public async Task<List<CategorieLien>> GetAllAsync(CancellationToken ct = default)
        => await _context.CategorieLien.ToListAsync();

    public async Task<List<CategorieLien>> GetAllActifAsync(CancellationToken ct = default)
        => await _context.CategorieLien
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();

    public async Task<List<CategorieLien>> SearchAsync(SearchCategorieLien searchParams, CancellationToken ct = default)
    {
      List<CategorieLien> retour = await _context.CategorieLien
     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<CategorieLien> CreateAsync(CategorieLien objet)
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
                //Insert RefCategorieLien
                context.CategorieLien.Add(objet);
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

    public async Task<CategorieLien> UpdateAsync(CategorieLien objet)
    {
      _context.Entry(await _context.CategorieLien.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      CategorieLien entite = await _context.CategorieLien.Where(x => x.Id == Id).FirstOrDefaultAsync();
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