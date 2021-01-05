using GOH.Data.Repositories.Interfaces;
using GOH.Entities.BlogEntities;
using GOH.Data.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace GOH.Data.Repositories
{
    public class BlogRepository : Repository<Blog, BlogContext>, IBlogRepository
    {
        public BlogRepository(BlogContext context)
            : base(context)
        {
        }

        public Blog GetLatest()
        {
            bool isCompatible = BlogContext.Database.CompatibleWithModel(true);

            //return BlogContext.Blogs.OrderBy(blog => blog.CreatedOn).FirstOrDefault();
            return BlogContext.Blogs.FirstOrDefault();
        }

        public IEnumerable<Blog> GetByMonth(int month)
        {
            //return BlogContext.Blogs.Where(blog => blog.CreatedOn.Month == month);
            return BlogContext.Blogs.ToList();
        }

        public BlogContext BlogContext
        {
            get { return Context as BlogContext; }
        }
    }
}