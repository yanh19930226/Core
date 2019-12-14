﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMigrate.Domain
{
    public class Tag:BaseEntity
    {
        public string Name { get; set; }
        public virtual List<PostTag> PostTags { get; set; }
    }
}
