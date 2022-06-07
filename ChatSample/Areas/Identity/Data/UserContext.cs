using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using poruchTv.Areas.Identity.Data;
using poruchTv.Models;
using poruchTv.Models.Rooms;
using poruchTv.Models.Video;

namespace poruchTv.Data
{
    public class UserContext : IdentityDbContext<User>
    {
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Content> contents { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<UserSession> UserSessions { get; set; }
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
