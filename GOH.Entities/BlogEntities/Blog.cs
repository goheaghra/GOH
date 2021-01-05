using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace GOH.Entities.BlogEntities
{
    public class Blog : Entity
    {
        public int BlogId { get; set; }
        [Required]
        public string Title { get; set; }
        [AllowHtml]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Comment> Comments { get; private set; }

        [NotMapped]
        public string TagsText
        {
            get
            {
                List<string> tagsTextList = new List<string>();

                foreach (Tag tag in Tags)
                {
                    tagsTextList.Add(tag.Value);
                }

                string result = string.Join(" ", tagsTextList.ToArray());

                return result;
            }
            set
            {
                string[] tagValues = value.ToLower().Split(' ');
                foreach (string tagValue in tagValues)
                {
                    this.Tags.Add(new Tag(tagValue));
                }
            }
        }

        public Blog()
        {
            Comments = new List<Comment>();
            Tags = new List<Tag>();
        }

        public void AddComment(Comment comment)
        {
            this.Comments.Add(comment);
        }
    }
}
