using GOH.Data.Contexts;
using GOH.Data.Repositories.Interfaces;
using GOH.Entities.BlogEntities;

namespace GOH.Data.Repositories
{

    public class TagRepository : Repository<Tag, BlogContext>, ITagRepository
    {
        public TagRepository(BlogContext context)
            : base(context)
        {
        }

        public BlogContext BlogContext
        {
            get { return Context as BlogContext; }
        }
    }
}