using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOH.Entities.BlogEntities
{
    public class Tag : Entity
    {
        public int TagId { get; set; }
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Value { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public Tag(){}

        public Tag(string tagValue)
        {
            Value = tagValue;
        }
    }

}
