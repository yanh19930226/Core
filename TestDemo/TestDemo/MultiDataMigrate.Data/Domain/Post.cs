using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Domain
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public virtual List<PostTag> PostTags { get; set; }
    }
}
