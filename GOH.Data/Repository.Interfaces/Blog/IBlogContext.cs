using GOH.Entities.BlogEntities;
using System.Data.Entity;

namespace GOH.Data.Repositories.Interfaces
{

    public interface IBlogContext : IDbContext
    {
        IDbSet<Blog> Blogs { get; }
    }
}
