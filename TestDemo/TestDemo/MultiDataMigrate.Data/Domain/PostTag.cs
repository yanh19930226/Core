using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Domain
{
    public class PostTag : BaseEntity
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
