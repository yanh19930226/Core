using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiDataMigrate.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Config
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasMany(p => p.PostTags)
                  .WithOne(p => p.Post)
                  .HasForeignKey(p => p.PostId);
        }
    }
}
