using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOH.Entities.BlogEntities
{
    public class CommentHierarchical : Comment
    {
        public string Path { get; set; }
        public int Level { get; set; }

    }
}
