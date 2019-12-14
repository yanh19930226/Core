using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiDataMigrate.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Config
{
    public class PostTagConfig : IEntityTypeConfiguration<PostTag>
    {
        public void Configure(EntityTypeBuilder<PostTag> builder)
        {
        }
    }
}
