using Arram.Core.Common.Logging;
using Arram.Core.DAL;
using Arram.Core.DAL.Entities;
using Arram.Core.Repo.Infrastructure.Contract;
using Arram.Core.SearchService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Repositories
{
  public class ArticleRepository : IArticleRepository
  {
    private readonly ArramContext _context;
    private readonly ILoggingService _logger;
    private readonly LogItem logitem;
    public ArticleRepository(ArramContext context, ILoggingService logger)
    {
      _context = context;
      _logger = logger;
      logitem = new LogItem() { NomApplication = "Arram", Layer = "Repository" };
    }
    private async Task<bool> ArticleExists(int id, CancellationToken ct = default) =>
        await _context.Licence.AnyAsync(a => a.Id == id, ct);

    public async Task<Article> GetAsync(int id)
    {
      try
      {
        var retour = await _context.Article
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
    public async Task<List<Article>> GetAllAsync(CancellationToken ct = default)
    {
      try
      {
        List<Article> retour = await _context.Article.ToListAsync();
        return retour;
      }
      catch(Exception ex)
      {
        logitem.Exception = ex;
        _logger.WriteError(logitem);
      }
        return null;
    }
    public async Task<List<Article>> GetAllActifAsync(CancellationToken ct = default)
    {
      try
      {
        List<Article> retour = await _context.Article
          .Where(x => !x.IsDeleted)
          .OrderByDescending(on => on.DateCreation)
          .ToListAsync();
        return retour;
      }
      catch (Exception ex)
      {
        logitem.Exception = ex;
        _logger.WriteError(logitem);
      }
      return null;
    }
    public async Task<List<Article>> SearchAsync(SearchArticle searchParams, CancellationToken ct = default)
    {
      List<Article> retour = await _context.Article
     .Where(x => !x.IsDeleted)
     .OrderByDescending(on => on.DateCreation)
     .ToListAsync();
      return retour;
    }

    public async Task<Article> CreateAsync(Article objet)
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
                //Insert Article
                context.Article.Add(objet);
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

    public async Task<Article> UpdateAsync(Article objet)
    {
      _context.Entry(await _context.Article.FirstOrDefaultAsync(x => x.Id == objet.Id)).CurrentValues.SetValues(objet);
      await _context.SaveChangesAsync();
      return objet;
    }

    public async Task<bool> DeleteAsync(int Id)
    {
      Article entite = await _context.Article.Where(x => x.Id == Id).FirstOrDefaultAsync();
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
