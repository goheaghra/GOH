using GOH.Data.Repositories.Interfaces;
using GOH.Data.Repositories;
using GOH.Entities.BlogEntities;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Linq;

namespace GOH.Data.Contexts
{

    public class CommentRepository : Repository<Comment, BlogContext>, ICommentRepository
    {
        public CommentRepository(BlogContext context)
            : base(context)
        {
        }


        public XmlDocument GetCommentTree(int id)
        {

            CommentHierarchical[] comments = this.BlogContext.GetCommentsHierarchical(id);

            XmlDocument commentTree = new XmlDocument();
            XmlDeclaration xmlDeclaration = commentTree.CreateXmlDeclaration("1.0", "UTF-8", null);
            commentTree.AppendChild(xmlDeclaration);

            Stack<XmlElement> stack = new Stack<XmlElement>();
            stack.Push(commentTree.CreateElement("CommentThread"));

            commentTree.AppendChild(stack.Peek());

            int currentLevel = 1;

            // if the level's increased then we nned to nest under another CommenThread, 
            // if the level's decreased then we need to move back out to the correct level
            foreach (CommentHierarchical comment in comments)
            {
                if (comment.Level > currentLevel)
                {
                    XmlElement itemsElem = commentTree.CreateElement("CommentThread");
                    stack.Peek().AppendChild(itemsElem);
                    stack.Push(itemsElem);

                }
                else if (comment.Level < currentLevel)
                {
                    int i = currentLevel;
                    while (i > comment.Level)
                    {
                        stack.Pop();
                        i--;
                    };
                }

                XmlElement commentElem = commentTree.CreateElement("Comment");

                XmlElement commentIdElem = commentTree.CreateElement("CommentId");
                commentIdElem.AppendChild(commentTree.CreateTextNode(comment.CommentId.ToString()));
                commentElem.AppendChild(commentIdElem);

                XmlElement commenterNameElem = commentTree.CreateElement("CommenterName");
                commenterNameElem.AppendChild(commentTree.CreateTextNode(comment.CommenterName));
                commentElem.AppendChild(commenterNameElem);

                XmlElement valueElem = commentTree.CreateElement("Value");
                valueElem.AppendChild(commentTree.CreateTextNode(comment.Value));
                commentElem.AppendChild(valueElem);

                XmlElement blogIdElem = commentTree.CreateElement("BlogId");
                blogIdElem.AppendChild(commentTree.CreateTextNode(comment.BlogId.ToString()));
                commentElem.AppendChild(blogIdElem);

                XmlElement parentIdElem = commentTree.CreateElement("ParentId");
                parentIdElem.AppendChild(commentTree.CreateTextNode(comment.ParentId.ToString()));
                commentElem.AppendChild(parentIdElem);

                stack.Peek().AppendChild(commentElem);
                currentLevel = comment.Level;

            }

            return commentTree;
        }


        private XElement GetComment(DataRow row)
        {
            XElement comment = new XElement("Comment");

            comment.Add(new XElement("CommentId", row["CommentId"]));
            comment.Add(new XElement("CommenterName", row["CommenterName"]));
            comment.Add(new XElement("Value", row["Value"]));
            comment.Add(new XElement("BlogId", row["BlogId"]));
            comment.Add(new XElement("ParentId", row["ParentId"]));
            comment.Add(new XElement("CreatedOn", row["CreatedOn"]));
            comment.Add(new XElement("ChangedOn", row["ChangedOn"]));
            comment.Add(new XElement("BlogId", row["BlogId"]));
            comment.Add(new XElement("ParentId", row["ParentId"]));

            return comment;
        }


        //public XDocument GetCommentTree(int id)
        //{

        //    XDocument commentTree = new XDocument(new XElement("Comments"));

        //    DataSet commentsDS = this.BlogContext.GetCommentsData(id);

        //    foreach (DataRow row in commentsDS.Tables["Comments"].Rows)
        //    {


        //        XElement comment
        //            = new XElement("Comment");

        //        comment.Add(new XElement("CommentId", row["CommentId"]));
        //        comment.Add(new XElement("CommenterName", row["CommenterName"]));
        //        comment.Add(new XElement("Value", row["Value"]));
        //        comment.Add(new XElement("BlogId", row["BlogId"]));
        //        comment.Add(new XElement("ParentId", row["ParentId"]));
        //        comment.Add(new XElement("CreatedOn", row["CreatedOn"]));
        //        comment.Add(new XElement("ChangedOn", row["ChangedOn"]));
        //        comment.Add(new XElement("BlogId", row["BlogId"]));
        //        comment.Add(new XElement("ParentId", row["ParentId"]));



        //        //CommentId, CommenterName, Value, BlogId, ParentId, CreatedOn, ChangedOn, CAST(CommentId AS varchar(max)) AS [Path], 1 AS [level]


        //        commentTree.Root.Add(comment);

        //    }



        //XDocument commentTree = new XDocument(
        //                            new XElement("Parent",
        //                                new XElement("Child1", "Child1 data"),
        //                                new XElement("Child2", "Child2 data")
        //                            )
        //                        ); 


        //    return commentTree;

        //}



        public BlogContext BlogContext
        {
            get { return Context as BlogContext; }
        }
    }
}