using Microsoft.EntityFrameworkCore;
using poruchTv.Models.Video;

namespace ChatSample.db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Content> contents { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}