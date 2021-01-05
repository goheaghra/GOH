using GOH.Entities.BlogEntities;
using System.Collections.Generic;

namespace GOH.Data.Repositories.Interfaces
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Blog GetLatest();
        IEnumerable<Blog> GetByMonth(int month);
    }
}
