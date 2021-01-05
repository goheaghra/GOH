using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOH.Entities.BlogEntities
{
    public class Comment : Entity
    {
        public int CommentId { get; set; }

        [DisplayName("Commenter")]
        [Required]
        public string CommenterName { get; set; }

        [DisplayName("Comment")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string Value { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public int? ParentId { get; set; }
        public Comment Parent { get; set; }
        public IList<Comment> Children { get; set; }

        public Comment()
        {
            Children = new List<Comment>();
        }

    }
}
