using Arram.Core.DTO;
using System.Collections.Generic;

namespace Arram.Core.API.Models
{
  public class ArticlesModel
  {
    public IEnumerable<ArticleDTO> Items { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }
  }
}
