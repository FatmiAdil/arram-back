using Arram.Core.DAL.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Arram.Core.Repo.Infrastructure.Contract
{
  public interface IParametreRepository
  {    
    Task<Parametre> GetAsync(CancellationToken ct = default);
    
    Task<Parametre> UpdateAsync(Parametre objet);    
  }
}
