using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime ModifyTime { get; set; } = DateTime.Now;
        public bool IsDel { get; set; } = false;
    }
}
