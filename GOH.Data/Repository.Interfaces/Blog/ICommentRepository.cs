using GOH.Entities.BlogEntities;
using System.Collections.Generic;
using System.Xml;

namespace GOH.Data.Repositories.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        XmlDocument GetCommentTree(int id);
    }
}
