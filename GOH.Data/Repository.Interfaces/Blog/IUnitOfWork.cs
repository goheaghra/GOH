using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOH.Data.Repositories.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBlogRepository Blogs { get; }
        ITagRepository Tags { get; }
        ICommentRepository Comments { get; }
        System.Data.Entity.Database Database { get; }
        int Complete();

    }
}
