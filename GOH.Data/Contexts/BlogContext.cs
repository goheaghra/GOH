using GOH.Entities;
using GOH.Entities.BlogEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;


namespace GOH.Data.Contexts
{



    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public BlogContext() : base("name=GOHConnectionString")
        {
            Database.SetInitializer<DbContext>(null);
            //Database.SetInitializer(new DbInitializer());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<BlogContext, Data.Migrations.Configuration>());
        }

        public CommentHierarchical[] GetCommentsHierarchical(int id)
        {

            SqlParameter blogIdParam = new SqlParameter("@BlogId", id);
            var parameters = new object[] { blogIdParam };
            var comments = this.Database.SqlQuery<CommentHierarchical>("GetComments @BlogId", new SqlParameter("@BlogId", id));
            return comments.ToArray();
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Blog>().HasKey(b => b.BlogId).Property(b => b.BlogId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Comment>().HasOptional(c => c.Parent).WithMany(c => c.Children).HasForeignKey(c => c.ParentId);
            modelBuilder.Ignore<CommentHierarchical>();


            
            base.OnModelCreating(modelBuilder);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                DateTime now = DateTime.UtcNow;
                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).CreatedOn = now;
                }

                ((Entity)entity.Entity).ChangedOn = now;
            }
        }
    }
}