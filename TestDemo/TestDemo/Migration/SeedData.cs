using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DataMigrate;
using System;
using System.Linq;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MigrationDbContext(serviceProvider.GetRequiredService<DbContextOptions<MigrationDbContext>>()))
            {
                if (context.Blogs.Any())
                {
                    return;
                }

                context.Blogs.Add(new DataMigrate.Domain.Blog
                {
                    Id = 1,
                    Url = "blog"
                });
                context.Posts.Add(new DataMigrate.Domain.Post
                {
                    Id = 1,
                    Title = "post"
                });
                context.Tags.Add(new DataMigrate.Domain.Tag
                {
                    Id = 1,
                    Name = "tag"
                });
                context.PostTags.Add(new DataMigrate.Domain.PostTag
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
