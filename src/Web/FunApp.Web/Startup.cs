﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FunApp.Web.Models;
using FunApp.Data.Models;
using FunApp.Data;
using FunApp.Data.Common;
using FunApp.Services.DataServices;
using FunApp.Services.Mapping;
using FunApp.Services.Models.Home;
using FunApp.Web.Models.Jokes;
using FunApp.Services.MachineLearning;

namespace FunApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(IndexJokeViewModel).Assembly,
                typeof(CreateJokeInputModel).Assembly
                );

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<FunAppContext>(options =>
                    options.UseSqlServer(
                        this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<FunAppUser>( options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 3;
            })
                .AddEntityFrameworkStores<FunAppContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // My application services
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IJokeService, JokeService>();
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IJokesCategorizer, JokesCategorizer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
