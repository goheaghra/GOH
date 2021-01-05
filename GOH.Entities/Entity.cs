using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOH.Entities
{
    public abstract class Entity
    {
        public DateTime CreatedOn { get; set; }
        public DateTime ChangedOn { get; set; }
    }
}
