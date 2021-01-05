using GOH.Data.Repositories.Interfaces;
using GOH.Data.Contexts;

namespace GOH.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogContext _context;

        public IBlogRepository Blogs { get; private set; }
        public ITagRepository Tags { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public System.Data.Entity.Database Database
        {
            get { return _context.Database; }
        }

        public UnitOfWork(BlogContext context)
        {
            _context = context;
            Blogs = new BlogRepository(_context);
            Tags = new TagRepository(_context);
            Comments = new CommentRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}