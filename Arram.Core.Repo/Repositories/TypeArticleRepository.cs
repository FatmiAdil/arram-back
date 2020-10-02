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
  public class TypeArticleRepository : ITypeArticleRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public TypeArticleRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }
    private async Task<bool> RefTypeArticleExists(int id, CancellationToken ct = default) =>
        await _context.Licence.AnyAsync(a => a.Id == id, ct);

    public async Task<TypeArticle> GetAsync(int id)
    {
      try
      {
        var retour = await _context.RefTypeArticle
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
    public async Task<List<TypeArticle>> GetAllAsync(CancellationToken ct = default)
        => await _context.RefTypeArticle.ToListAsync();

    public async Task<List<TypeArticle>> GetAllActifAsync(CancellationToken ct = default)
        => await _context.RefTypeArticle
      .Where(x => !x.IsDeleted)
       .OrderByDescending(on => on.DateCreation)
      .ToListAsync();

    public async Task<List<TypeArticle>> SearchAsync(SearchTypeArticle searchParams, CancellationToken ct = default)
    {
      List<TypeArticle> retour = await _context.RefTypeArticle

     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<TypeArticle> CreateAsync(TypeArticle objet)
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
                //Insert RefTypeArticle
                context.RefTypeArticle.Add(objet);
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

    public async Task<TypeArticle> UpdateAsync(TypeArticle objet)
    {
      _context.Entry(await _context.RefTypeArticle.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      TypeArticle entite = await _context.RefTypeArticle.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
