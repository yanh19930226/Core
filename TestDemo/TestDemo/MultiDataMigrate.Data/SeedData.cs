using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiDataMigrate.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiDataMigrate.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MigrationDbContext(serviceProvider.GetRequiredService<DbContextOptions<MigrationDbContext>>()))
            {
                if (context.Blogs.Any())
                {
                    return;
                }

                context.Blogs.Add(new Blog
                {
                    Id = 1,
                    Url = "blog"
                });
                context.Posts.Add(new Post
                {
                    Id = 1,
                    Title = "post"
                });
                context.Tags.Add(new Tag
                {
                    Id = 1,
                    Name = "tag"
                });
                context.PostTags.Add(new PostTag
                {
                    Id = 1,
                    PostId = 1,
                    TagId = 1
                });
                context.SaveChanges();
            }
        }
    }
}
