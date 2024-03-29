using Blog.Data.Mappings;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data
{
    public class BlogDataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PostWithTagsCount> PostWithTagsCounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=localhost,1433;Database=Blog;User ID=sa;Password=");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PostMap());

            modelBuilder.Entity<PostWithTagsCount>(x =>
            {
                x.ToSqlQuery(@"
                SELECT [Title], (SELECT COUNT([Id])
                    FROM [Tag]
                    WHERE [Post].[Id] = [Id]) AS [Count]
                FROM [Post]");
            });
        }
    }
}