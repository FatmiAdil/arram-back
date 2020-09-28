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
  public class PhotoRepository : IPhotoRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public PhotoRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }

    private async Task<bool> PhotoExists(int id, CancellationToken ct = default) =>
        await _context.Photo.AnyAsync(a => a.Id == id, ct);    

    public async Task<List<Photo>> GetAllActifAsync(CancellationToken ct = default)
    {
      List <Photo> retour = await _context.Photo
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();
      return retour;
    }

    public async Task<List<Photo>> GetAllAsync(CancellationToken ct = default)
    {
      List<Photo> retour = await _context.Photo      
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();
      return retour;
    }

    public async Task<Photo> GetAsync(int id)
    {
      try
      {
        var retour = await _context.Photo
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

    public async Task<List<Photo>> SearchAsync(SearchPhoto searchParams, CancellationToken ct = default)
    {
      List<Photo> retour = await _context.Photo     
     
     .Where(x => (searchParams.LicenceId == null ? 1 == 1 : x.LicenceId == searchParams.LicenceId))     
     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<List<Photo>> GetByLicenceIdAsync(int licenceId)
    {
      List<Photo> retour = await _context.Photo     
     .Where(x => !x.IsDeleted && x.LicenceId == licenceId)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<Photo> CreateAsync(Photo objet)
    {
      _context.Photo.Add(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<Photo> UpdateAsync(Photo objet)
    {
      _context.Entry(await _context.Photo.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      Photo entite = await _context.Photo.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
