using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutTracker_v2.Data;
using WorkoutTracker_v2.Repositories;
using WorkoutTracker_v2.Services;

namespace WorkoutTracker_v2
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
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<DapperContext>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IDapperService, DapperService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddControllersWithViews(o => o.Filters.Add(new AuthorizeFilter()));


            services.AddRazorPages().AddMvcOptions(o => o.Filters.Add(new AuthorizeFilter()))
                .AddRazorRuntimeCompilation();

            services.AddAuthentication(o => {
                o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                // o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddCookie(ExternalAuthenticationDefaults.AuthenticationScheme)
                .AddGoogle(o =>
                {
                    o.SignInScheme = ExternalAuthenticationDefaults.AuthenticationScheme;
                    o.ClientId = Configuration["Google:ClientId"]; 
                    o.ClientSecret = Configuration["Google:ClientSecret"]; 
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
