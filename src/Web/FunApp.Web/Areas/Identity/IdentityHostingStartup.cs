using System;
using FunApp.Web.Areas.Identity.Data;
using FunApp.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FunApp.Web.Areas.Identity.IdentityHostingStartup))]
namespace FunApp.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<FunAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("FunAppContextConnection")));

                services.AddDefaultIdentity<FunAppUser>()
                    .AddEntityFrameworkStores<FunAppContext>();
            });
        }
    }
}