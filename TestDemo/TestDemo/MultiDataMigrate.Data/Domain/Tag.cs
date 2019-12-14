using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Domain
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<PostTag> PostTags { get; set; }
    }
}
