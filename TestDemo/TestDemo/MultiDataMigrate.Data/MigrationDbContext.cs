using Microsoft.EntityFrameworkCore;
using MultiDataMigrate.Data.Config;
using MultiDataMigrate.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiDataMigrate.Data
{
    public class MigrationDbContext : DbContext
    {
        public MigrationDbContext(DbContextOptions<MigrationDbContext> options) : base(options)
        {

        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlogConfig());
            modelBuilder.ApplyConfiguration(new PostConfig());
            modelBuilder.ApplyConfiguration(new TagConfig());
        }
    }
}
