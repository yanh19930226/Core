using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataMigrate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataMigrate.Config
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
