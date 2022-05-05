using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using poruchTv.Areas.Identity.Data;
using poruchTv.Data;

[assembly: HostingStartup(typeof(poruchTv.Areas.Identity.IdentityHostingStartup))]
namespace poruchTv.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("UserConnection")));

                services.AddDefaultIdentity<User>(options =>
                    {
                        options.SignIn.RequireConfirmedAccount = true;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireDigit = true;
                        options.Password.RequireNonAlphanumeric = false;
                    })
                    .AddEntityFrameworkStores<UserContext>();
            });
        }
    }
}