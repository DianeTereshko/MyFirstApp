using System;
using Blog.Services.Context;
using Blog.Services.DbService;
using Blog.Services.Extensions;
using Blog.Services.Models;
using Blog.Services.ViewModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using SameSiteMode = Microsoft.AspNetCore.Http.SameSiteMode;

namespace Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddSingleton<TriggerModel>();

            string defaultConnection = Configuration
                .GetConnectionString("DefaultConnection");

            string identityConnection = Configuration
                .GetConnectionString("IdentityConnection");

            string snatchConnection = Configuration
                .GetConnectionString("SnatchConnection");

            // * TODO: Внедрить контекст в оперативной памяти
            services.AddDbContext<BlogDbContextInMemory>(options =>
                options.UseInMemoryDatabase("Blog"));

            services.AddDbContext<IdentityUserDbContextInMemory>(options =>
                    options.UseInMemoryDatabase("InMemoryUserDataBase"))
                .AddDefaultIdentity<User>()
                .AddRoles<IdentityRole>()
                //.AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<IdentityUserDbContextInMemory>();


            // * TODO: Внедрить реальный контекст базы данных
            //services.AddDbContext<BlogDbContext>(options =>
            //    options.UseSqlServer(defaultConnection));

            //services.AddDbContext<IdentityUserDbContext>(options =>
            //        options.UseSqlServer(identityConnection))
            //    .AddDefaultIdentity<User>()
            //    .AddRoles<IdentityRole>()
            //    //.AddDefaultUI(UIFramework.Bootstrap4)
            //    .AddEntityFrameworkStores<IdentityUserDbContext>();

            // * TODO: Задать настройки identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;


                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
                options.User.RequireUniqueEmail = false;
            });

            services.AddRouting(options =>
                options.LowercaseUrls = true);

            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                options.HttpsPort = 443;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseRemoveImport(env);
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseHsts();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseSession();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}");
            });
        }
    }
}
