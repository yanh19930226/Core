using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiDataMigrate.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Config
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasMany(p => p.PostTags)
                  .WithOne(p => p.Tag)
                  .HasForeignKey(p => p.TagId);
        }
    }
}
