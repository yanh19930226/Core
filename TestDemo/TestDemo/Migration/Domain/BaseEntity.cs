using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMigrate.Domain
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime ModifyTime { get; set; } = DateTime.Now;
        public bool IsDel { get; set; } = false;
    }
}
