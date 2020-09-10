using Arram.Core.Common.Logging;
using Arram.Core.Repo.Infrastructure.Contract;
using Arram.Core.DAL;
using Arram.Core.DAL.Entities;
using Arram.Core.SearchService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Arram.Core.Repo.Repositories
{
  public class LicenceRepository : ILicenceRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public LicenceRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Tracking", Layer = "Repository" };
    }
    private async Task<bool> AmendeExists(int id, CancellationToken ct = default) =>
        await _context.Licence.AnyAsync(a => a.Id == id, ct);

    public async Task<Licence> GetAsync(int id)
    {
      try
      {
        var retour = await _context.Licence
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
    public async Task<List<Licence>> GetAllAsync(CancellationToken ct = default)
        => await _context.Licence.ToListAsync();

    public async Task<List<Licence>> GetAllActifAsync(CancellationToken ct = default)
        => await _context.Licence
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();

    public async Task<List<Licence>> SearchAsync(SearchLicence searchParams, CancellationToken ct = default)
    {
      List<Licence> retour = await _context.Licence
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

    public async Task<Licence> CreateAsync(Licence objet)
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
                context.Licence.Add(objet);
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

    public async Task<Licence> UpdateAsync(Licence objet)
    {
      _context.Entry(await _context.Licence.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      Licence entite = await _context.Licence.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
