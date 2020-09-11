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
  public class IllustrationRepository : IIllustrationRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public IllustrationRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }
    private async Task<bool> IllustrationExists(int id, CancellationToken ct = default) =>
        await _context.Illustration.AnyAsync(a => a.Id == id, ct);

    public async Task<Illustration> GetAsync(int id)
    {
      try
      {
        var retour = await _context.Illustration
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
    public async Task<List<Illustration>> GetAllAsync(CancellationToken ct = default)
        => await _context.Illustration.ToListAsync();

    public async Task<List<Illustration>> GetAllActifAsync(CancellationToken ct = default)
        => await _context.Illustration
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();

    public async Task<List<Illustration>> SearchAsync(SearchIllustration searchParams, CancellationToken ct = default)
    {
      List<Illustration> retour = await _context.Illustration
     //.Include(v => v.Vehicule)
     //.Include(c => c.Conducteur)
     //.Include(s => s.StatutAmende)
     //.Include(t => t.TypeAmende)
     //.Where(x => (searchParams.VehiculeId == null ? 1 == 1 : x.VehiculeId == searchParams.VehiculeId))
     //.Where(x => (searchParams.ConducteurId == null ? 1 == 1 : x.ConducteurId == searchParams.ConducteurId))
     //.Where(x => (searchParams.TypeAmendeId == null ? 1 == 1 : x.TypeAmendeId == searchParams.TypeAmendeId))
     //.Where(x => (searchParams.StatutAmendeId == null ? 1 == 1 : x.StatutAmendeId == searchParams.StatutAmendeId))
     //.Where(x => (string.IsNullOrEmpty(searchParams.NumeroAmende) ? 1 == 1 : x.NumeroAmende.Contains(searchParams.NumeroAmende)))
     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<Illustration> CreateAsync(Illustration objet)
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
                context.Illustration.Add(objet);
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

    public async Task<Illustration> UpdateAsync(Illustration objet)
    {
      _context.Entry(await _context.Illustration.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      Illustration entite = await _context.Illustration.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
