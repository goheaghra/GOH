namespace GOH.Data.Migrations
{
        using System;
        using System.Data.Entity.Migrations;

        public partial class GetComments : DbMigration
        {
            public override void Up()
            {
                Sql(@"Create Procedure GetComments(
                    @BlogId int
                    )
                    As
                    Begin
                    WITH CommentsForBlog AS
                    (
                        SELECT CommentId, CommenterName, Value, BlogId, ParentId, CreatedOn, ChangedOn, CAST(CommentId AS varchar(max)) AS [Path], 1 AS [level]
                        FROM dbo.Comments
                        WHERE BlogId = @BlogId AND ParentId IS NULL
                        UNION ALL

                        SELECT c.CommentId, c.CommenterName, c.Value, c.BlogId, c.ParentId, c.CreatedOn, c.ChangedOn, CAST(cte.[Path] + '/' + CAST(c.CommentId AS varchar(max)) AS varchar(max)) AS [Path], cte.[level] + 1
                        FROM dbo.Comments c
                        INNER JOIN CommentsForBlog cte ON c.ParentId = cte.CommentId
                    )
                    SELECT * From CommentsForBlog order by Path
                    End");
            }

            public override void Down()
            {
                Sql(@"Drop Procedure GetComments");
            }
        }
}
