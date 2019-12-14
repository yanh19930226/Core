using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMigrate.Domain
{
    public class Blog:BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public virtual List<Post> Posts { get; set; }
    }
}
