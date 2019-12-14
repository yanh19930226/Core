using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MultiDataMigrate.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data.Config
{
    public class BlogConfig : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasMany(p => p.Posts)
                   .WithOne(p => p.Blog)
                   .HasForeignKey(p => p.BlogId);
        }
    }
}
